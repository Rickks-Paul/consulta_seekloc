using BD.DI;
using BD.Domain;
using BD.Domain.Entity;
using BD.Domain.Enum;
using BD.Domain.Repository;
using BD.Domain.Service;
using BD.Domain.ValueObject;
using BuscaDadosAPI.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Http;

namespace BuscaDados.Web.Controllers
{
    public class UsuarioController : ApiController
    {
        private static IDependencyInjectionContainer container;
        public UsuarioController(IDependencyInjectionContainer _container)
        {
            container = _container;
        }

        private String ObterProvedor(String isp, String org)
        {
            String valor = String.Empty;

            if (!String.IsNullOrEmpty(org))
            {
                valor = org;
            }

            if (!String.IsNullOrEmpty(isp))
            {
                valor += String.IsNullOrEmpty(valor) ? isp : "|" + isp;
            }

            return valor;
        }


        [HttpGet]
        public IHttpActionResult LogarUsuarioMobile(String email, String senha, String city, String countryCode, String isp, String org, String ip, String region, String modeloCel, String numeroIMEI, String operadora)
        {
            String novacity = String.Empty;
            String novocountryCode = String.Empty;
            String novoisp = String.Empty;
            String novoorg = String.Empty;
            String novoip = String.Empty;
            String novaregion = String.Empty;
            String novomodeloCel = String.Empty;
            String novonumeroIMEI = String.Empty;
            String novaoperadora = String.Empty;

            novacity = !String.IsNullOrEmpty(city) ? city.Substring(0, city.Length > 50 ? 50 : city.Length) : String.Empty;
            novocountryCode = !String.IsNullOrEmpty(countryCode) ? countryCode.Substring(0, countryCode.Length > 50 ? 50 : countryCode.Length) : String.Empty;
            novoisp = !String.IsNullOrEmpty(isp) ? isp.Substring(0, isp.Length > 50 ? 50 : isp.Length) : String.Empty;
            novoorg = !String.IsNullOrEmpty(org) ? org.Substring(0, org.Length > 50 ? 50 : org.Length) : String.Empty;
            novoip = !String.IsNullOrEmpty(ip) ? ip.Substring(0, ip.Length > 50 ? 50 : ip.Length) : String.Empty;
            novaregion = !String.IsNullOrEmpty(region) ? region.Substring(0, region.Length > 50 ? 50 : region.Length) : String.Empty;
            novomodeloCel = !String.IsNullOrEmpty(modeloCel) ? modeloCel.Substring(0, modeloCel.Length > 50 ? 50 : modeloCel.Length) : String.Empty;
            novonumeroIMEI = !String.IsNullOrEmpty(numeroIMEI) ? numeroIMEI.Substring(0, numeroIMEI.Length > 20 ? 20 : numeroIMEI.Length) : String.Empty;
            novaoperadora = !String.IsNullOrEmpty(operadora) ? operadora.Substring(0, operadora.Length > 20 ? 20 : operadora.Length) : String.Empty;

            var resultado = ValidarLogon(email, senha);
            if (resultado != null)
            {
                return resultado;
            }

            try
            {
                IErroLoginRepository repositoryErro = container.Resolve<IErroLoginRepository>();

                var bloqueado = repositoryErro.CometeuMuitosErrosLogin(email);

                if (bloqueado)
                {
                    return NotFound();
                }

                IUsuarioRepository repository = container.Resolve<IUsuarioRepository>();

                var ultimaAssinatura = String.Empty;
                var ultimoLogon = String.Empty;
                var usuarioExistente = repository.ObterPeloEmailESenha(email, senha);
                if (usuarioExistente != null)
                {
                    if (!String.IsNullOrEmpty(usuarioExistente.Ativo) && usuarioExistente.Ativo == "N")
                    {
                        return Json(new { erro = "S", mensagem = String.Format("Seu cadastro está inativado pelo seguinte motivo: {0}", usuarioExistente.MotivoInativo) });
                    }

                    IAssinaturaRepository assinaturaRepo = container.Resolve<IAssinaturaRepository>();

                    var dataFim = assinaturaRepo.ObterDataFimAssinaturaMaisRecentePeloEmailDoUsuario(email);

                    if (!String.IsNullOrEmpty(dataFim))
                    {
                        ultimaAssinatura = dataFim;
                    }

                    IAtividadeRepository atividadeRepo = container.Resolve<IAtividadeRepository>();

                    var ultimoLogonBD = atividadeRepo.ObterUltimoLogonPeloEmail(email);

                    if (!String.IsNullOrEmpty(ultimoLogonBD))
                    {
                        ultimoLogon = ultimoLogonBD;
                    }

                    var atividadeAtual = new Atividade()
                    {
                        Usuario = usuarioExistente,
                        IP = ip,
                        Data = DateTime.Now,
                        TipoAtividade = Convert.ToInt32(TipoAtividade.LogonUsuario),
                        Origem = Convert.ToInt32(OrigemAcesso.Android),
                        Descricao = "O usuário com o e-mail " + email + " logou no sistema.",
                        IMEI = novonumeroIMEI,
                        Cidade = novacity,
                        ModeloCelular = novomodeloCel,
                        Operadora = novaoperadora,
                        Pais = novocountryCode,
                        Provedor = ObterProvedor(org, isp)
                    };

                    Task.Run(() =>
                    {
                        //SalvarAtividadeHelper.SalvarAtividade(atividadeAtual);
                    });

                    return Json(new { erro = "N", mensagem = "OK", nome = usuarioExistente.Nome, email = usuarioExistente.Email, cadastrado = String.Format("{0:dd/MM/yyyy}", usuarioExistente.DataCadastro), dataassinatura = ultimaAssinatura, logon = ultimoLogon, codigoIndicacao = usuarioExistente.Id + 1000 });
                }
                else
                {
                    ErroLogin erro = new ErroLogin();
                    erro.Email = email;
                    erro.IMEI = numeroIMEI;
                    erro.Data = DateTime.Now;
                    erro.Operadora = operadora;
                    erro.IP = ip;
                    erro.Senha = senha;

                    //SalvarErroLoginHelper.SalvarErro(erro);
                    return Json(new { erro = "S", mensagem = "E-mail não cadastrado ou senha incorreta." });
                }


            }
            catch (Exception ex)
            {
                //SalvarErroSistemaHelper.SalvarErro(ex);
                String mensagem = !String.IsNullOrEmpty(ex.Message) ? ex.Message : String.Empty;
                String mensagemInterna = ex.InnerException != null && String.IsNullOrEmpty(ex.InnerException.Message) ? ex.InnerException.Message : String.Empty;
                String assunto = "Erro na API de Login de usuário ";
                String mensagem2 = String.Format("Exceção: {0} \n Exceção interna: {1}, \n Data/Hora: {2} \n Email: {3}: ", mensagem, mensagemInterna, DateTime.Now, email);

                //EmailHelper.EnviarEmail(assunto, "vando.lg@gmail.com", mensagem2);

                return Json(new { erro = "S", mensagem = "Ocorreu um erro inesperado ao tentar realizar o seu login, não se preocupe, o suporte já foi acionado e vai resolver o problema e te informar assim que for verificado." });
            }
        }


        [HttpGet]
        public IHttpActionResult ObterEmailPelaChaveDeRecuperacao(String chave)
        {
            if (!String.IsNullOrEmpty(chave))
            {


                var repository = container.Resolve<IRecuperacaoSenhaRepository>();

                var recupera = repository.ObterPeloChave(chave);

                if (recupera != null)
                {
                    var usuario = container.Resolve<IUsuarioRepository>().ObterPeloEmail(recupera.Email);
                    var atividadeAtual = new Atividade() { Usuario = usuario, Data = DateTime.Now, TipoAtividade = Convert.ToInt32(TipoAtividade.RecuperaSenhaUsuario), Origem = Convert.ToInt32(OrigemAcesso.Web), Descricao = "O usuário com o e-mail " + recupera.Email + " solicitou recuperar a senha pela chave." };

                    container.Resolve<AtividadeCRUDService>().Salvar(atividadeAtual);

                    return Json(new { erro = "N", email = recupera.Email });
                }
                else
                {
                    return Json(new { erro = "S", mensagem = "A chave informada não existe." });
                }
            }

            return Json(new { erro = "N", email = String.Empty });
        }

        [HttpGet]
        public IHttpActionResult ObterHistoricoConsultas(String email, String ip, String hash)
        {
            return null;
        }

        [HttpGet]
        public IHttpActionResult ObterHistoricoPedidos(String email, String ip, String hash, String historicoPedidos)
        {
            List<PedidoVO> pedidosVO = null;

            try
            {
                if (!String.IsNullOrEmpty(email) && !String.IsNullOrEmpty(hash))
                {
                    if (hash != DomainUtil.GerarHashMD5(String.Concat(email, email, ip)))
                    {
                        return Json(new { erro = "S", mensagem = "Erro na hash de autenticação." });
                    }

                    pedidosVO = container.Resolve<IPedidoRepository>().ObterTodosPedidosPorEmail(email);
                    var usuario = container.Resolve<IUsuarioRepository>().ObterPeloEmail(email);
                    var atividadeAtual = new Atividade() { Usuario = usuario, IP = ip, Data = DateTime.Now, TipoAtividade = Convert.ToInt32(TipoAtividade.HistoricoCompra), Origem = Convert.ToInt32(OrigemAcesso.Web), Descricao = "O usuário com o e-mail " + email + " solicitou histórico de pedidos." };

                    container.Resolve<AtividadeCRUDService>().Salvar(atividadeAtual);
                }
            }
            catch (Exception ex)
            {
                container.Resolve<ErroSistemaCRUDService>().Salvar(ex);
                String mensagem = !String.IsNullOrEmpty(ex.Message) ? ex.Message : String.Empty;
                String mensagemInterna = ex.InnerException != null && String.IsNullOrEmpty(ex.InnerException.Message) ? ex.InnerException.Message : String.Empty;
                String assunto = "Erro em ObterHistoricoPedidos na API do ";
                String mensagem2 = String.Format("Exceção: {0} \n Exceção interna: {1}, \n Data/Hora: {2} \n Email: {3}: ", mensagem, mensagemInterna, DateTime.Now, email);

                container.Resolve<IEmailService>().EnviarEmail(Constantes.EMAIL_DESTINATARIO, Constantes.EMAIL_REMETENTE, "Suporte ", assunto, mensagem2, false);

                return Json(new { erro = "S", mensagem = "Ocorreu um erro inesperado ao obter o histórico de pedidos, não se preocupe, o suporte já foi acionado e vai resolver o problema e te informar assim que for verificado." });
            }

            return Json(pedidosVO != null ? pedidosVO : new List<PedidoVO>());
        }

        [HttpGet]
        public IHttpActionResult ObterHistoricoAssinaturas(String email, String ip, String hash, String valor)
        {
            List<AssinaturaVO> assinaturas = null;

            try
            {
                if (!String.IsNullOrEmpty(email) && !String.IsNullOrEmpty(hash))
                {
                    if (hash != DomainUtil.GerarHashMD5(String.Concat(email, email, ip)))
                    {
                        return null;
                    }

                    assinaturas = container.Resolve<IAssinaturaRepository>().ObterTodasAssinaturasVOPorEmail(email);
                    var usuario = container.Resolve<IUsuarioRepository>().ObterPeloEmail(email);
                    var atividadeAtual = new Atividade() { Usuario = usuario, IP = ip, Data = DateTime.Now, TipoAtividade = Convert.ToInt32(TipoAtividade.HistoricoAssinatura), Origem = Convert.ToInt32(OrigemAcesso.Web), Descricao = "O usuário com o e-mail " + email + " solicitou histórico de assinaturas." };

                    container.Resolve<AtividadeCRUDService>().Salvar(atividadeAtual);
                }
            }

            catch (Exception ex)
            {
                container.Resolve<ErroSistemaCRUDService>().Salvar(ex);
                String mensagem = !String.IsNullOrEmpty(ex.Message) ? ex.Message : String.Empty;
                String mensagemInterna = ex.InnerException != null && String.IsNullOrEmpty(ex.InnerException.Message) ? ex.InnerException.Message : String.Empty;
                String assunto = "Erro em ObterHistoricoAssinaturas na API da Subcode 23";
                String mensagem2 = String.Format("Exceção: {0} \n Exceção interna: {1}, \n Data/Hora: {2} \n Email: {3}: ", mensagem, mensagemInterna, DateTime.Now, email);

                container.Resolve<IEmailService>().EnviarEmail(Constantes.EMAIL_DESTINATARIO, Constantes.EMAIL_REMETENTE, "Suporte ", assunto, mensagem2, false);

                return Json(new { erro = "S", mensagem = "Ocorreu um erro inesperado ao obter o histórico de assinaturas, não se preocupe, o suporte já foi acionado e vai resolver o problema e te informar assim que for verificado." });
            }

            return Json(assinaturas != null ? assinaturas : new List<AssinaturaVO>());
        }



        private IHttpActionResult ValidarLogon(String email, String senha)
        {
            if (String.IsNullOrEmpty(email))
            {
                return Json(new { erro = "S", mensagem = "O e-mail é um campo obrigatório." });
            }
            else
            {
                if (!DomainUtil.EmailEhValido(email) || (email.IndexOf(".") == -1))
                {
                    return Json(new { erro = "S", mensagem = "O e-mail informado é inválido." });
                }
            }

            if (String.IsNullOrEmpty(senha))
            {
                return Json(new { erro = "S", mensagem = "A senha é um campo obrigatório." });
            }

            return null;
        }


        [HttpGet]
        public IHttpActionResult Logar(String email, String senha, String ip)
        {
            var resultado = ValidarLogon(email, senha);
            if (resultado != null)
            {
                return resultado;
            }

            try
            {
                IUsuarioRepository repository = container.Resolve<IUsuarioRepository>();

                var ultimaAssinatura = String.Empty;
                var ultimoLogon = String.Empty;
                var usuarioExistente = repository.ObterPeloEmailESenha(email, senha);
                if (usuarioExistente != null)
                {
                    if (!String.IsNullOrEmpty(usuarioExistente.Ativo) && usuarioExistente.Ativo == "N")
                    {
                        return Json(new { erro = "S", mensagem = String.Format("Seu cadastro está inativado pelo seguinte motivo: {0}", usuarioExistente.MotivoInativo) });
                    }

                    IAssinaturaRepository assinaturaRepo = container.Resolve<IAssinaturaRepository>();

                    var dataFim = assinaturaRepo.ObterDataFimAssinaturaMaisRecentePeloEmailDoUsuario(email);

                    if (!String.IsNullOrEmpty(dataFim))
                    {
                        ultimaAssinatura = dataFim;
                    }

                    IAtividadeRepository atividadeRepo = container.Resolve<IAtividadeRepository>();

                    var ultimoLogonBD = atividadeRepo.ObterUltimoLogonPeloEmail(email);

                    if (!String.IsNullOrEmpty(ultimoLogonBD))
                    {
                        ultimoLogon = ultimoLogonBD;
                    }

                    var atividadeAtual = new Atividade() { Usuario = usuarioExistente, IP = ip, Data = DateTime.Now, TipoAtividade = Convert.ToInt32(TipoAtividade.LogonUsuario), Origem = Convert.ToInt32(OrigemAcesso.Web), Descricao = "O usuário com o e-mail " + email + " logou no sistema." };

                    container.Resolve<AtividadeCRUDService>().Salvar(atividadeAtual);

                    return Json(new { erro = "N", mensagem = "OK", nome = usuarioExistente.Nome, email = usuarioExistente.Email, cadastrado = String.Format("{0:dd/MM/yyyy}", usuarioExistente.DataCadastro), dataassinatura = ultimaAssinatura, logon = ultimoLogon, codigoIndicacao = usuarioExistente.Id + 1000 });
                }
                else
                {
                    return Json(new { erro = "S", mensagem = "E-mail não cadastrado ou senha incorreta." });
                }

            }
            catch (Exception ex)
            {
                container.Resolve<ErroSistemaCRUDService>().Salvar(ex);
                String mensagem = !String.IsNullOrEmpty(ex.Message) ? ex.Message : String.Empty;
                String mensagemInterna = ex.InnerException != null && String.IsNullOrEmpty(ex.InnerException.Message) ? ex.InnerException.Message : String.Empty;
                String assunto = "Erro na API de Login de usuário ";
                String mensagem2 = String.Format("Exceção: {0} \n Exceção interna: {1}, \n Data/Hora: {2} \n Email: {3}: ", mensagem, mensagemInterna, DateTime.Now, email);

                container.Resolve<IEmailService>().EnviarEmail(Constantes.EMAIL_DESTINATARIO, Constantes.EMAIL_REMETENTE, "Suporte ", assunto, mensagem2, false);

                return Json(new { erro = "S", mensagem = "Ocorreu um erro inesperado ao tentar realizar o seu login, não se preocupe, o suporte já foi acionado e vai resolver o problema e te informar assim que for verificado." });
            }
        }

        [HttpGet]
        public IHttpActionResult AlterarSenha(String usaChave, String email, String chave, String senha, String confirmacaoSenha, String ip)
        {
            try
            {
                if (String.IsNullOrEmpty(senha))
                {
                    return Json(new { erro = "S", mensagem = "A senha é um campo obrigatório." });
                }

                if (String.IsNullOrEmpty(confirmacaoSenha))
                {
                    return Json(new { erro = "S", mensagem = "A confirmação da senha é um campo obrigatório." });
                }

                if (senha != confirmacaoSenha)
                {
                    return Json(new { erro = "S", mensagem = "A senha e a confirmação da senha devem ser iguais." });
                }


                if (usaChave == "S")
                {
                    var recuperacaoRepository = container.Resolve<IRecuperacaoSenhaRepository>();

                    var recupera = recuperacaoRepository.ObterPeloChave(chave);

                    if (recupera != null)
                    {
                        if (recupera.Status == 1)
                        {
                            return Json(new { erro = "S", mensagem = "A chave informada não existe." });
                        }
                        else
                        {

                            var usuarioRepository = container.Resolve<IUsuarioRepository>();
                            var usuario = usuarioRepository.ObterPeloEmail(recupera.Email);

                            if (usuario != null)
                            {
                                if (!String.IsNullOrEmpty(usuario.Ativo) && usuario.Ativo == "N")
                                {
                                    return Json(new { erro = "S", mensagem = "Usuário que gerou essa chave está bloqueado." });
                                }

                                if (usuario.Senha == senha)
                                {
                                    return Json(new { erro = "S", mensagem = "Sua senha não foi alterada pois a nova senha é igual a senha anterior." });
                                }

                                //Altera efetivamente a senha do usuário
                                usuario.Senha = senha;
                                usuarioRepository.Save(usuario);

                                //Altera o registro de alteração de senha para 1 = confirmado
                                recupera.Status = 1;
                                recuperacaoRepository.Save(recupera);

                                var atividadeAtual = new Atividade() { Usuario = usuario, IP = ip, Data = DateTime.Now, TipoAtividade = Convert.ToInt32(TipoAtividade.AlteraSenhaUsuario), Origem = Convert.ToInt32(OrigemAcesso.Web), Descricao = "O usuário com o e-mail " + usuario.Email + " solicitou alterar a senha." };
                                container.Resolve<AtividadeCRUDService>().Salvar(atividadeAtual);
                            }
                            else
                            {
                                return Json(new { erro = "S", mensagem = "Usuário que gerou essa chave não existe ou está bloqueado." });
                            }

                        }
                    }
                    else
                    {
                        return Json(new { erro = "S", mensagem = "A chave informada não existe." });
                    }
                }
                else
                {
                    var usuarioRepository = container.Resolve<IUsuarioRepository>();
                    var usuario = usuarioRepository.ObterPeloEmail(email);
                    if (usuario != null)
                    {
                        if (!String.IsNullOrEmpty(usuario.Ativo) && usuario.Ativo == "N")
                        {
                            return Json(new { erro = "S", mensagem = "Usuário que gerou essa chave está bloqueado." });
                        }

                        if (usuario.Senha == senha)
                        {
                            return Json(new { erro = "S", mensagem = "Sua senha não foi alterada pois a nova senha é igual a senha anterior." });
                        }

                        //Altera efetivamente a senha do usuário
                        usuario.Senha = senha;
                        usuarioRepository.Save(usuario);

                        var atividadeAtual = new Atividade() { Usuario = usuario, IP = ip, Data = DateTime.Now, TipoAtividade = Convert.ToInt32(TipoAtividade.AlteraSenhaUsuario), Origem = Convert.ToInt32(OrigemAcesso.Web), Descricao = "O usuário com o e-mail " + usuario.Email + " solicitou alterar a senha." };
                        container.Resolve<AtividadeCRUDService>().Salvar(atividadeAtual);
                    }
                    else
                    {
                        return Json(new { erro = "S", mensagem = "Usuário que gerou essa chave não existe ou está bloqueado." });
                    }

                }

                return Json(new { erro = "S", mensagem = "A senha foi alterada com sucesso." });
            }
            catch (Exception ex)
            {
                container.Resolve<ErroSistemaCRUDService>().Salvar(ex);
                String mensagem = !String.IsNullOrEmpty(ex.Message) ? ex.Message : String.Empty;
                String mensagemInterna = ex.InnerException != null && String.IsNullOrEmpty(ex.InnerException.Message) ? ex.InnerException.Message : String.Empty;
                String assunto = "Erro na API de Recuperação de senha de usuário";
                String mensagem2 = String.Format("Exceção: {0} \n Exceção interna: {1}, \n Data/Hora: {2} \n Key: {3}: ", mensagem, mensagemInterna, DateTime.Now, chave);

                container.Resolve<IEmailService>().EnviarEmail(Constantes.EMAIL_DESTINATARIO, Constantes.EMAIL_REMETENTE, "Suporte ", assunto, mensagem2, false);

                return Json(new { erro = "S", mensagem = "Ocorreu um erro inesperado ao tentar alterar sua senha , não se preocupe, o suporte já foi acionado e vai resolver o problema e te informar assim que for verificado." });
            }
        }

        [HttpGet]
        public IHttpActionResult RecuperarSenha(String email, String ip)
        {
            try
            {
                if (String.IsNullOrEmpty(email))
                {
                    return Json(new { erro = "S", mensagem = "O e-mail é um campo obrigatório para recuperação de senha." });
                }

                IUsuarioRepository repository = container.Resolve<IUsuarioRepository>();

                var usuario = repository.ObterPeloEmail(email);

                if (usuario != null)
                {
                    if (!String.IsNullOrEmpty(usuario.Ativo) && usuario.Ativo == "N")
                    {
                        return Json(new { erro = "S", mensagem = "Este usuário está bloqueado e não pode alterar sua senha, para mais detalhes entre em contato com o suporte." });
                    }

                    IRecuperacaoSenhaRepository recuperaRepository = container.Resolve<IRecuperacaoSenhaRepository>();
                    var recupera = new RecuperacaoSenha();
                    recupera.Email = email;
                    recupera.Data = DateTime.Now;
                    recupera.Chave = Guid.NewGuid().ToString();
                    recupera.IP = ip;
                    recupera.Status = 0;
                    recuperaRepository.Save(recupera);

                    StringBuilder mensagem = new StringBuilder();

                    mensagem.Append("Nós recebemos uma solicitação de recuperação de senha do seu e-mail no  <br><br>");
                    mensagem.Append(String.Format("IP: {0} <br><br>", ip));
                    mensagem.Append(String.Format(@"Para criar uma nova senha clique <a href=""https://xpto.com/recuperarsenha.php?key={0}"" target=""_blank"">aqui</a>", recupera.Chave));

                    //Envia e-mail em uma nova task
                    Task.Run(() =>
                    {
                        container.Resolve<IEmailService>().EnviarEmail(email, Constantes.EMAIL_REMETENTE, "Suporte ", "Recuperação de senha - Super x", mensagem.ToString(), true);
                    });                    
                }

                return Json(new { erro = "N", mensagem = "Processo realizado com sucesso! Caso este e-mail esteja cadastrado vocé irá receber um link para mudar a senha em seu e-mail." });
            }
            catch (Exception ex)
            {
                container.Resolve<ErroSistemaCRUDService>().Salvar(ex);
                String mensagem = !String.IsNullOrEmpty(ex.Message) ? ex.Message : String.Empty;
                String mensagemInterna = ex.InnerException != null && String.IsNullOrEmpty(ex.InnerException.Message) ? ex.InnerException.Message : String.Empty;
                String assunto = "Erro na API de Recuperação de senha de usuário";
                String mensagem2 = String.Format("Exceção: {0} \n Exceção interna: {1}, \n Data/Hora: {2} \n Email: {3}: ", mensagem, mensagemInterna, DateTime.Now, email);

                container.Resolve<IEmailService>().EnviarEmail(Constantes.EMAIL_DESTINATARIO, Constantes.EMAIL_REMETENTE, "Suporte ", assunto, mensagem2, false);

                return Json(new { erro = "S", mensagem = "Ocorreu um erro inesperado ao tentar recuperar sua senha , não se preocupe, o suporte já foi acionado e vai resolver o problema e te informar assim que for verificado." });
            }
        }

        [HttpGet]
        public IHttpActionResult Cadastrar(String nome, String motivo, String email, String emailConfirmacao, String senha, String senhaConfirmacao, String ip, String codigoIndicacao)
        {
            var nomeSemCaracteresEspeciais = Regex.Replace(nome, @"[^\w\d]", " ");

            if (String.IsNullOrEmpty(nome))
            {
                return Json(new { erro = "S", mensagem = "Nome é um campo obrigatório e não deve possuir caracteres especiais" });
            }

            if (String.IsNullOrEmpty(motivo))
            {
                return Json(new { erro = "S", mensagem = "Motivo é um campo obrigatório." });
            }

            if (String.IsNullOrEmpty(email))
            {
                return Json(new { erro = "S", mensagem = "E-mail é um campo obrigatório." });
            }

            if (String.IsNullOrEmpty(emailConfirmacao))
            {
                return Json(new { erro = "S", mensagem = "Confirmação de e-mail é um campo obrigatório." });
            }

            if (email != emailConfirmacao)
            {
                return Json(new { erro = "S", mensagem = "E-mail e a confirmação de e-mail devem ser iguais." });
            }

            if (String.IsNullOrEmpty(senha))
            {
                return Json(new { erro = "S", mensagem = "Senha é um campo obrigatório." });
            }

            if (String.IsNullOrEmpty(senhaConfirmacao))
            {
                return Json(new { erro = "S", mensagem = "Confirmação de senha é um campo obrigatório." });
            }

            if (senha != senhaConfirmacao)
            {
                return Json(new { erro = "S", mensagem = "Senha e a confirmação de senha devem ser iguais." });
            }

            try
            {
                var codigoMontado = 0;

                IUsuarioRepository repository = container.Resolve<IUsuarioRepository>();

                var usuarioExistente = repository.ObterPeloEmail(email);
                if (usuarioExistente != null)
                {
                    return Json(new { codigo = "S", mensagem = "O e-mail informado já está cadastrado." });
                }

                if (!String.IsNullOrEmpty(codigoIndicacao))
                {
                    var identificadorUsuarioIndicou = 0;
                    Boolean converteu = Int32.TryParse(codigoIndicacao, out identificadorUsuarioIndicou);

                    if (converteu)
                    {
                        codigoMontado = identificadorUsuarioIndicou - 1000;

                        var indicador = repository.GetById(codigoMontado);
                        if (indicador == null)
                        {
                            return Json(new { codigo = "S", mensagem = "O código de indicação informado não existe." });
                        }
                    }
                }

                Usuario usuario = new Usuario();
                usuario.Nome = nomeSemCaracteresEspeciais;
                usuario.Senha = senha;
                usuario.MotivoCadastro = motivo;
                usuario.Email = email;
                usuario.IPCadastro = ip;
                usuario.IndicadorPor = codigoMontado != 0 ? codigoMontado : (Int32?)null;
                usuario.DataCadastro = DateTime.Now;

                repository.Save(usuario);

                var atividadeAtual = new Atividade() { Usuario = usuario, IP = ip, Data = DateTime.Now, TipoAtividade = Convert.ToInt32(TipoAtividade.CadastroUsuario), Origem = Convert.ToInt32(OrigemAcesso.Web), Descricao = "O usuário com o e-mail " + email + " se cadastrou." };
                container.Resolve<AtividadeCRUDService>().Salvar(atividadeAtual);
                //Envia e-mail em uma nova task
                Task.Run(() =>
                {
                    container.Resolve<IEmailService>().EnviarEmail(Constantes.EMAIL_DESTINATARIO, Constantes.EMAIL_REMETENTE, "Suporte ", "Usuário cadastrado no Busca Dados", "O usuário " + nome + " - Email: " + email + " se cadastrou no Busca Dados", false);
                });
            }
            catch (Exception ex)
            {
                container.Resolve<ErroSistemaCRUDService>().Salvar(ex);
                String mensagem = !String.IsNullOrEmpty(ex.Message) ? ex.Message : String.Empty;
                String mensagemInterna = ex.InnerException != null && String.IsNullOrEmpty(ex.InnerException.Message) ? ex.InnerException.Message : String.Empty;
                String assunto = "Erro na API de cadastro de usuário";
                String mensagem2 = String.Format("Exceção: {0} \n Exceção interna: {1}, \n Data/Hora: {2}", mensagem, mensagemInterna, DateTime.Now);

                container.Resolve<IEmailService>().EnviarEmail(Constantes.EMAIL_DESTINATARIO, Constantes.EMAIL_REMETENTE, "Suporte ", assunto, mensagem2, false);

                return Json(new { erro = "S", mensagem = "Ocorreu um erro inesperado ao tentar realizar o seu cadastro, não se preocupe, o suporte já foi acionado e vai resolver o problema e te informar assim que for verificado." });
            }

            return Json(new { erro = "N", mensagem = "Cadastro realizado com sucesso." });
        }
    }
}
