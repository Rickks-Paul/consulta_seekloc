using BD.DI;
using BD.Domain;
using BD.Domain.Entity;
using BD.Domain.Enum;
using BD.Domain.Repository;
using BD.Domain.Service;
using BD.Domain.ValueObject;
using BuscaDadosAPI.Domain;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Web.Http;
using Uol.PagSeguro.Constants;
using Uol.PagSeguro.Domain;
using Uol.PagSeguro.Exception;
using Uol.PagSeguro.Resources;
using Uol.PagSeguro.Service;

namespace BD.WebAPI.Controllers
{
    public class PagSeguroController : ApiController
    {
        private static IDependencyInjectionContainer container;
        public PagSeguroController(IDependencyInjectionContainer _container)
        {
            container = _container;
        }

        [HttpPost]
        public IHttpActionResult ObterNotificacao([FromBody] NotificacaoPagSeguroVO notificacao)
        {
            Boolean sandbox = true;
            String ArquivoConfiguracaoPagSeguro = @"C:\temp\PagSeguroConfig.xml";

            if (notificacao == null)
            {
                Task.Run(() =>
                {
                    String assunto = "Erro ao ObterNotificacao - ";
                    String mensagem2 = String.Format("Ocorreu um problema ao receber uma notificação (objeto nulo) do pagseguro: Data: {0}", DateTime.Now.ToString("dd/MM/yyyy - HH:mm:ss"));

                    container.Resolve<IEmailService>().EnviarEmail(Constantes.EMAIL_DESTINATARIO, Constantes.EMAIL_REMETENTE, "Suporte ", assunto, mensagem2, false);
                });

                return BadRequest();
            }

            try
            {
                var configuracaoRepo = container.Resolve<IConfiguracaoRepository>();
                var configuracao = configuracaoRepo.GetById(1);

                if (configuracao != null)
                {
                    ArquivoConfiguracaoPagSeguro = configuracao.ArquivoConfiguracaoPagSeguro;
                    sandbox = configuracao.AmbienteProducao == "N";
                }

                // PagSeguroConfiguration.UrlXmlConfiguration = @"C:\temp\PagSeguroConfig.xml";
                EnvironmentConfiguration.ChangeEnvironment(sandbox);

                AccountCredentials credentials = PagSeguroConfiguration.Credentials(sandbox);
                Transaction transaction = NotificationService.CheckTransaction(credentials, notificacao.notificationCode);

                if (transaction != null)
                {
                    if (!String.IsNullOrEmpty(transaction.Reference))
                    {
                        var pedidoRepo = container.Resolve<IPedidoRepository>();
                        var pedido = pedidoRepo.GetById(Convert.ToInt32(transaction.Reference));
                        pedidoRepo.Evict(pedido);

                        try
                        {
                            if (pedido != null)
                            {
                                if (transaction.TransactionStatus == TransactionStatus.Cancelled)
                                {
                                    pedido.Status = (Int32)StatusPedido.Cancelado;
                                    pedido.Motivo = "Cancelado pelo PagSeguro";
                                    pedido.CodigoAutorizacao = transaction.Code;
                                    pedido.DataAlteracaoStatus = DateTime.Now;
                                    pedidoRepo.Save(pedido);

                                    var atividadeAtual = new Atividade() { Usuario = pedido.Usuario, Data = DateTime.Now, TipoAtividade = Convert.ToInt32(TipoAtividade.MudancaStatusPedido), Origem = Convert.ToInt32(OrigemAcesso.Web), Descricao = String.Format("O pagseguro mudou o status do pedido {0} para cancelado.", pedido.Id) };

                                    container.Resolve<AtividadeCRUDService>().Salvar(atividadeAtual);
                                }
                                else if (transaction.TransactionStatus == TransactionStatus.InAnalysis)
                                {
                                    pedido.Status = (Int32)StatusPedido.Pendente;
                                    pedido.Motivo = "Em análise pelo PagSeguro";
                                    pedido.CodigoAutorizacao = transaction.Code;
                                    pedido.DataAlteracaoStatus = DateTime.Now;
                                    pedidoRepo.Save(pedido);

                                    var atividadeAtual = new Atividade() { Usuario = pedido.Usuario, Data = DateTime.Now, TipoAtividade = Convert.ToInt32(TipoAtividade.MudancaStatusPedido), Origem = Convert.ToInt32(OrigemAcesso.Web), Descricao = String.Format("O pagseguro mudou o status do pedido {0} para em análise.", pedido.Id) };

                                    container.Resolve<AtividadeCRUDService>().Salvar(atividadeAtual);
                                }
                                else if (transaction.TransactionStatus == TransactionStatus.Initiated)
                                {
                                    pedido.Status = (Int32)StatusPedido.Pendente;
                                    pedido.Motivo = "Iniciado pelo PagSeguro";
                                    pedido.CodigoAutorizacao = transaction.Code;
                                    pedido.DataAlteracaoStatus = DateTime.Now;
                                    pedidoRepo.Save(pedido);

                                    var atividadeAtual = new Atividade() { Usuario = pedido.Usuario, Data = DateTime.Now, TipoAtividade = Convert.ToInt32(TipoAtividade.MudancaStatusPedido), Origem = Convert.ToInt32(OrigemAcesso.Web), Descricao = String.Format("O pagseguro mudou o status do pedido {0} para iniciado.", pedido.Id) };

                                    container.Resolve<AtividadeCRUDService>().Salvar(atividadeAtual);
                                }
                                else if (transaction.TransactionStatus == TransactionStatus.WaitingPayment)
                                {
                                    pedido.Status = (Int32)StatusPedido.Pendente;
                                    pedido.Motivo = "Aguardando pagamento pelo PagSeguro";
                                    pedido.CodigoAutorizacao = transaction.Code;
                                    pedido.DataAlteracaoStatus = DateTime.Now;
                                    pedidoRepo.Save(pedido);

                                    var atividadeAtual = new Atividade() { Usuario = pedido.Usuario, Data = DateTime.Now, TipoAtividade = Convert.ToInt32(TipoAtividade.MudancaStatusPedido), Origem = Convert.ToInt32(OrigemAcesso.Web), Descricao = String.Format("O pagseguro mudou o status do pedido {0} para aguardando pagamento.", pedido.Id) };

                                    container.Resolve<AtividadeCRUDService>().Salvar(atividadeAtual);
                                }
                                else if (transaction.TransactionStatus == TransactionStatus.Available)
                                {
                                    pedido.Status = (Int32)StatusPedido.Pago;
                                    pedido.Motivo = "Saldo disponível no PagSeguro";
                                    pedido.CodigoAutorizacao = transaction.Code;
                                    pedido.DataAlteracaoStatus = DateTime.Now;
                                    pedidoRepo.Save(pedido);

                                    var atividadeAtual = new Atividade() { Usuario = pedido.Usuario, Data = DateTime.Now, TipoAtividade = Convert.ToInt32(TipoAtividade.MudancaStatusPedido), Origem = Convert.ToInt32(OrigemAcesso.Web), Descricao = String.Format("O pagseguro mudou o status do pedido {0} para disponível.", pedido.Id) };

                                    container.Resolve<AtividadeCRUDService>().Salvar(atividadeAtual);
                                }
                                else if (transaction.TransactionStatus == TransactionStatus.Paid)
                                {
                                    if (pedido.Status == (Int32)StatusPedido.Pago || pedido.Status == (Int32)StatusPedido.Fechado || pedido.Status == (Int32)StatusPedido.Cancelado)
                                    {
                                        var atividadeAtual1 = new Atividade() { Usuario = pedido.Usuario, Data = DateTime.Now, TipoAtividade = Convert.ToInt32(TipoAtividade.AtividadeIncomumPagSeguro), Origem = Convert.ToInt32(OrigemAcesso.Web), Descricao = String.Format("O pagseguro aparentemente tentou mudar o status do pedido {0} que está pago/fechado/cancelado ", pedido.Id) };

                                        container.Resolve<AtividadeCRUDService>().Salvar(atividadeAtual1);

                                        return Ok();
                                    }

                                    pedido.Status = (Int32)StatusPedido.Pago;
                                    pedido.Motivo = "Pagamento confirmado pelo PagSeguro";
                                    pedido.CodigoAutorizacao = transaction.Code;
                                    pedido.DataAlteracaoStatus = DateTime.Now;
                                    pedidoRepo.Save(pedido);

                                    if (pedido.Servico.GeraMeses == "S")
                                    {
                                        //verifica se o usuário tem alguma assinatura vigente
                                        var assinaturaRepo = container.Resolve<IAssinaturaRepository>();
                                        var assinatura = assinaturaRepo.ObterAssinaturaMaisRecentePeloEmailDoUsuario(pedido.Usuario.Email);

                                        DateTime novaData = (assinatura != null && assinatura.DataFim > DateTime.Now) ? assinatura.DataFim : DateTime.Now;

                                        var novaAssinatura = new Assinatura();
                                        novaAssinatura.Pedido = pedido;
                                        novaAssinatura.DataCadastro = DateTime.Now;
                                        novaAssinatura.DataInicio = novaData;
                                        Int32 Quantidade = Convert.ToInt32(pedido.Servico.QuantidadeMeses);
                                        novaAssinatura.DataFim = novaData.AddMonths(Quantidade);
                                        novaAssinatura.Usuario = pedido.Usuario;

                                        container.Resolve<AssinaturaCRUDService>().Salvar(novaAssinatura);

                                        Task.Run(() =>
                                        {
                                            String assunto = " - Pagamento de pedido confirmado";
                                            String mensagem2 = String.Format("Olá {0} <br><br> O pagamento do seu pedido {1} foi confirmado pelo pagseguro.<br><br>A vigência da sua nova assinatura do serviço de consulta é de {2} até {3}. <br><br> Atenciosamente, Equipe Subcode23", pedido.Usuario.Nome, pedido.Id, novaAssinatura.DataInicio.ToString("dd/MM/yyyy - HH:mm:ss"), novaAssinatura.DataFim.ToString("dd/MM/yyyy - HH:mm:ss"));

                                            container.Resolve<IEmailService>().EnviarEmail(Constantes.EMAIL_DESTINATARIO, Constantes.EMAIL_REMETENTE, "Suporte ", assunto, mensagem2, true);
                                        });
                                    }
                                    else if (pedido.Servico.GeraCredito == "S")
                                    {
                                        var creditoRepo = container.Resolve<ICreditoRepository>();
                                        var novoCredito = new Credito();
                                        novoCredito.Pedido = pedido;
                                        novoCredito.DataCadastro = DateTime.Now;
                                        novoCredito.DataCadastro = DateTime.Now;
                                        Int32 Quantidade = Convert.ToInt32(pedido.Servico.QuantidadeCredito);
                                        novoCredito.Usuario = pedido.Usuario;

                                        creditoRepo.Save(novoCredito);
                                    }

                                    var atividadeAtual = new Atividade() { Usuario = pedido.Usuario, Data = DateTime.Now, TipoAtividade = Convert.ToInt32(TipoAtividade.MudancaStatusPedido), Origem = Convert.ToInt32(OrigemAcesso.Web), Descricao = String.Format("O pagseguro mudou o status do pedido {0} para pago.", pedido.Id) };

                                    container.Resolve<AtividadeCRUDService>().Salvar(atividadeAtual);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Task.Run(() =>
                            {
                                String assunto = "Erro no método ObterNotificacao - ";
                                String mensagem2 = String.Format("Ocorreu um erro -  Exceção: {0} - Exceção interna: {1} - Data: {2}", ex.Message, (ex.InnerException != null && ex.InnerException.Message != null) ? ex.InnerException.Message : "Exceção interna não definida", DateTime.Now.ToString("dd/MM/yyyy - HH:mm:ss"));

                                container.Resolve<IEmailService>().EnviarEmail(Constantes.EMAIL_DESTINATARIO, Constantes.EMAIL_REMETENTE, "Suporte ", assunto, mensagem2, false);

                                //SalvarErroSistemaHelper.SalvarErro(ex);
                            });
                            return BadRequest();
                        }
                    }
                    else
                    {
                        return Ok();
                    }

                    return Ok();
                }
            }

            catch (PagSeguroServiceException ex)
            {
                String mensagem = String.Empty;
                Task.Run(() =>
                {
                    String assunto = "Erro de PagSeguroServiceException no método ObterNotificacao - Subcode 23";
                    if (ex.Errors.Count > 0)
                    {
                        mensagem = ex.Errors[0].Code + " - " + ex.Errors[0].Message;
                    }
                    String mensagem2 = String.Format("Ocorreu um erro - Erro do pagseguro {0} - Data: {1}", mensagem, DateTime.Now.ToString("dd/MM/yyyy - HH:mm:ss"));

                    container.Resolve<IEmailService>().EnviarEmail(Constantes.EMAIL_DESTINATARIO, Constantes.EMAIL_REMETENTE, "Suporte ", assunto, mensagem2, false);
                });
                return BadRequest();
            }

            return Ok();
        }

        [HttpGet]
        public IHttpActionResult ObterURLPagamento(String email, String servico, String ip, String hash, String emitenota, String cpfnota)
        {
            var hashGerado = DomainUtil.GerarHashMD5(String.Concat(email, ip, servico));
            String pedidoGerado = String.Empty;
            String nomeComprador = String.Empty;
            String codigoServico = String.Empty;
            String descricaoServico = String.Empty;
            String valorServico = String.Empty;
            Usuario usuarioAtual = null;
            Boolean sandbox = true;
            String ArquivoConfiguracaoPagSeguro = @"C:\temp\PagSeguroConfig.xml";

            if (!File.Exists(ArquivoConfiguracaoPagSeguro))
            {
                return Json(new { erro = "S", mensagem = "Arquivo de configuração do pagamento com pagseguro não foi encontrado." });
            }


            try
            {
                var configuracaoRepo = container.Resolve<IConfiguracaoRepository>();
                var usuarioRepo = container.Resolve<IUsuarioRepository>();
                var servicoRepo = container.Resolve<IServicoRepository>();
                var usuario = usuarioRepo.ObterPeloEmail(email);
                var servicoBD = servicoRepo.ObterPeloCodigo(servico);
                var configuracao = configuracaoRepo.GetById(1);
                if (configuracao != null)
                {
                    sandbox = configuracao.AmbienteProducao == "N";
                    ArquivoConfiguracaoPagSeguro = configuracao.ArquivoConfiguracaoPagSeguro;
                }

                if (usuario != null)
                {
                    nomeComprador = usuario.Nome;
                    usuarioAtual = usuario;
                    var pedidoRepo = container.Resolve<IPedidoRepository>();

                    var pedido = new Pedido();
                    pedido.Usuario = usuario;
                    pedido.IP = ip;
                    pedido.Status = (Int32)StatusPedido.Pendente;
                    pedido.Data = DateTime.Now;
                    pedido.CPFNF = cpfnota;
                    pedido.EmiteNF = emitenota;
                    pedido.FormaPagamento = (Int32)FormaPagamento.PagSeguro;

                    if (servicoBD != null)
                    {
                        codigoServico = servicoBD.Codigo;
                        descricaoServico = servicoBD.DescricaoCurta;
                        valorServico = String.Format("{0:0.00}", servicoBD.Valor);
                        pedido.Servico = servicoBD;
                        pedidoRepo.Save(pedido);
                        pedidoGerado = pedido.Id.ToString();
                    }
                    else
                    {
                        return Json(new { erro = "S", mensagem = "Código do serviço não cadastrado." });
                    }

                }
                else
                {
                    return Json(new { erro = "S", mensagem = "Usuário não cadastrado." });
                }
            }
            catch (Exception ex)
            {
                String debug = ex.Message;
                return Json(new { erro = "S", mensagem = "Ocorreu um erro inesperado e não foi possível gerar o seu pedido, por gentileza informe ao desenvolvedor." });
            }

            PagSeguroConfiguration.UrlXmlConfiguration = ArquivoConfiguracaoPagSeguro;
            EnvironmentConfiguration.ChangeEnvironment(sandbox);

            PaymentRequest payment = new PaymentRequest();

            payment.Items.Add(new Item(codigoServico, descricaoServico, 1, Convert.ToDecimal(valorServico)));

            payment.Sender = new Sender(nomeComprador, email, null);

            payment.Shipping = new Shipping();
            payment.Shipping.ShippingType = ShippingType.NotSpecified;
            payment.Currency = Currency.Brl;
            payment.Reference = pedidoGerado;

            try
            {
                AccountCredentials credentials = PagSeguroConfiguration.Credentials(sandbox);
                Uri paymentRedirectUri = payment.Register(credentials);

                var atividadeAtual = new Atividade() { Usuario = usuarioAtual, Data = DateTime.Now, TipoAtividade = Convert.ToInt32(TipoAtividade.NovaCompra), Origem = Convert.ToInt32(OrigemAcesso.Web), Descricao = String.Format("O usuário com o e-mail {0} fez o pedido {1} e gerou a url {2} para checkout no pagseguro", email, pedidoGerado, paymentRedirectUri.AbsoluteUri) };

                container.Resolve<AtividadeCRUDService>().Salvar(atividadeAtual);

                return Json(new { erro = "N", url = paymentRedirectUri.AbsoluteUri });
            }
            catch (PagSeguroServiceException ex)
            {
                String mensagem = String.Empty;

                String assunto = "Erro de PagSeguroServiceException no método ObterURLPagamento -  " + email;
                if (ex.Errors.Count > 0)
                {
                    mensagem = ex.Errors[0].Code + " - " + ex.Errors[0].Message;
                }
                String mensagem2 = String.Format("Ocorreu um erro - Erro do pagseguro {0} - Data: {1}", mensagem, DateTime.Now.ToString("dd/MM/yyyy - HH:mm:ss"));

                container.Resolve<IEmailService>().EnviarEmail(Constantes.EMAIL_DESTINATARIO, Constantes.EMAIL_REMETENTE, "Suporte ", assunto, mensagem2, false);

                String texto = ex.Message;
                return Json(new { erro = "S", mensagem = texto }); ;
            }
        }
    }
}