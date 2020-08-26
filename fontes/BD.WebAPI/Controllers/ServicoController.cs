using BD.DI;
using BD.Domain.Entity;
using BD.Domain.Enum;
using BD.Domain.Repository;
using BD.Domain.Service;
using BD.Domain.ValueObject;
using BuscaDadosAPI.Domain;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace BuscaDados.Web.Controllers
{
    public class ServicoController : ApiController
    {
        private static IDependencyInjectionContainer container;

        public ServicoController(IDependencyInjectionContainer _container)
        {
            container = _container;
        }

        [HttpGet]
        public IHttpActionResult ObterTodos(String email, String ip, String hash, String tipo = "0")
        {
            var hashGerado = DomainUtil.GerarHashMD5(String.Concat(email, email, ip, ip));

            if (tipo == "0")
            {
                if (hashGerado != hash)
                {
                    var atividadeAtual = new Atividade() { Usuario = null, IP = ip, Data = DateTime.Now, TipoAtividade = Convert.ToInt32(TipoAtividade.PossivelTentativaDeHack), Origem = Convert.ToInt32(OrigemAcesso.Web), Descricao = "O usuário com o e-mail " + email + " tentou acessar a consulta de serviço com a Hash errada." };

                    container.Resolve<AtividadeCRUDService>().Salvar(atividadeAtual);

                    return Json(new { erro = "S", mensagem = "Hash incorreto, esta tentativa de consulta foi logada e enviada ao Administrador." });
                }
            }

            List<ServicoVO> servicosVO = null;

            servicosVO = container.Resolve<IServicoRepository>().ObterTodosAtivos();

            if (servicosVO != null)
            {
                var atividadeAtual = new Atividade() { Usuario = null, IP = ip, Data = DateTime.Now, TipoAtividade = Convert.ToInt32(TipoAtividade.ConsultaServico), Origem = Convert.ToInt32(OrigemAcesso.Web), Descricao = "O usuário com o e-mail " + email + " solicitou consulta de serviços." };

                container.Resolve<AtividadeCRUDService>().Salvar(atividadeAtual);
            }

            return Json(new { erro = "N", itens = servicosVO != null ? servicosVO : new List<ServicoVO>() });
        }
    }
}
