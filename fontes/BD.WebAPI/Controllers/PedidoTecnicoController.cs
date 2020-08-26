using BD.DI;
using BD.Domain.Entity;
using BD.Domain.Enum;
using BD.Domain.Repository;
using BD.Domain.Service;
using BD.Domain.ValueObject;
using System;
using System.Web.Http;

namespace BuscaDadosAPI.Web.Controllers
{
    public class PedidoTecnicoController : ApiController
    {
        private static IDependencyInjectionContainer container;
        public PedidoTecnicoController(IDependencyInjectionContainer _container)
        {
            container = _container;
        }

        [HttpGet]
        public IHttpActionResult GravarPedidoTecnico(String emailUsuario, String senhaUsuario, Int16 motivo, String descricao, String cpf)
        {
            try
            {
                var tipo = (TipoPedidoTecnico)motivo;

                var usuario = container.Resolve<IUsuarioRepository>().ObterPeloEmailESenha(emailUsuario, senhaUsuario);

                if (usuario == null)
                {
                    return Json(new { erro = "S", mensagem = "Usuário não possui acesso a este serviço." });
                }
                else
                {
                    var repository = container.Resolve<IPedidoTecnicoRepository>();

                    if (tipo == TipoPedidoTecnico.DadosCPFDesatualizados || tipo == TipoPedidoTecnico.ErroConsultarCPF)
                    {
                        var pedidos = repository.ObterPedidosTecnicosUsuarioPeloCPF(usuario.Id, cpf);

                        if (pedidos != null && pedidos.Count > 0)
                        {
                            var pedido = pedidos[0];
                            return Json(new { erro = "S", mensagem = String.Format("Já existe o chamado técnico {0} para verificação deste CPF.", pedido.Id.ToString()) });
                        }
                        else
                        {
                            PedidoTecnico pedidoTecnico = new PedidoTecnico();
                            pedidoTecnico.CPF = cpf;
                            pedidoTecnico.DataCadastro = DateTime.Now;
                            pedidoTecnico.Descricao = descricao;
                            pedidoTecnico.Status = (Int16)StatusPedidoTecnico.Pendente;
                            pedidoTecnico.Usuario = usuario;
                            pedidoTecnico.Motivo = (Int16)motivo;

                            repository.Save(pedidoTecnico);

                            return Json(new { erro = "N", mensagem = "Gravado com sucesso." });
                        }
                    }
                    else
                    {
                        PedidoTecnico pedidoTecnico = new PedidoTecnico();
                        pedidoTecnico.CPF = cpf;
                        pedidoTecnico.DataCadastro = DateTime.Now;
                        pedidoTecnico.Descricao = descricao;
                        pedidoTecnico.Status = (Int16)StatusPedidoTecnico.Pendente;
                        pedidoTecnico.Usuario = usuario;
                        pedidoTecnico.Motivo = (Int16)motivo;

                        repository.Save(pedidoTecnico);

                        return Json(new { erro = "N", mensagem = "Gravado com sucesso." });
                    }

                }
            }
            catch (Exception)
            {
                return Json(new { erro = "S", mensagem = "Erro ao gravar o chamado técnico, envie um e-mail para o desenvolvedor informando o ocorrido." });
            }
        }

        [HttpGet]
        public IHttpActionResult ObterChamadosUsuario(RequisicaoConsultaVO requisicao)
        {
            var erro = new ErroConsulta();
            erro.Cpfcnpj = requisicao.cpf;
            erro.Email = requisicao.email;
            erro.Aplicativo = requisicao.aplicativo;
            erro.Data = DateTime.Now;
            erro.Versao = requisicao.versao;
            erro.Ip = requisicao.ip;

            var consultaVO = new ConsultaCPFCNPJVO();
            consultaVO.CPF = requisicao.cpf;
            consultaVO.Email = requisicao.email;
            consultaVO.Versao = requisicao.versao;
            consultaVO.IP = requisicao.ip;
            consultaVO.Hash = requisicao.hash;

            try
            {
                var usuario = container.Resolve<IUsuarioRepository>().ObterPeloEmailESenha(requisicao.email, requisicao.senha);

                if (usuario == null)
                {
                    return Json(new { erro = "S", mensagem = "Usuário não possui acesso a este serviço." });
                }
                else
                {
                    var atividadeAtual = new Atividade() { Usuario = usuario, IP = requisicao.ip, Data = DateTime.Now, TipoAtividade = Convert.ToInt32(TipoAtividade2.ConsultaPedido), Origem = Convert.ToInt32(OrigemAcesso.Web), Descricao = "O usuário com o e-mail " + requisicao.email + " fez uma consulta de pedido técnico. " };

                    container.Resolve<AtividadeCRUDService>().Salvar(atividadeAtual);

                    var chamados = container.Resolve<IPedidoTecnicoRepository>().ObterPedidosTecnicosUsuario(usuario.Id);

                    return Json(new { erro = "N", pedidosTecnicos = chamados });
                }
            }
            catch (Exception)
            {
                return Json(new { erro = "S", mensagem = "Erro ao consultar os chamados técnicos do usuários." });
            }
        }
    }
}