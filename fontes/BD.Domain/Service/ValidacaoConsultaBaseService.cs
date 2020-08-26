using BD.DI;
using BD.Domain.Entity;
using BD.Domain.Repository;
using BD.Domain.ValueObject;
using System;
using System.Threading.Tasks;

namespace BD.Domain.Service
{
    public class ValidacaoConsultaBaseService
    {
        public Boolean TemErro { get; set; }
        public ErroVO erroVO { get; set; }
        public ErroConsulta ErroConsulta { get; set; }
        public Usuario Usuario { get; set; }

        private static IDependencyInjectionContainer container;

        public ValidacaoConsultaBaseService(IDependencyInjectionContainer _container)
        {
            container = _container;
            erroVO = new ErroVO();
        }

        public virtual void Validate(ConsultaCPFCNPJVO vo)
        {

        }

        protected void ValidaEmail(String email, ErroConsulta erro)
        {
            if (String.IsNullOrEmpty(email))
            {
                TemErro = true;
                erroVO.erro = "S";
                erroVO.mensagem = "O e-mail deve ser informado.";

                SalvarErro(erro, erroVO.mensagem);
            }
            else
            {
                var usuarioRepo = container.Resolve<IUsuarioRepository>();
                if (!usuarioRepo.VerificarSeUsuarioExiste(email))
                {
                    TemErro = true;
                    erroVO.erro = "S";
                    erroVO.mensagem = "O e-mail informado não está cadastrado.";

                    SalvarErro(erro, erroVO.mensagem);
                }
            }
        }

        protected void ValidaAssinatura(String email, ErroConsulta erro)
        {
            var assinatura = container.Resolve<IAssinaturaRepository>().ObterDataFimAssinaturaMaisRecentePeloEmailDoUsuario(email, true);
            ICreditoRepository creditoRepo = container.Resolve<ICreditoRepository>();
            Credito credito = null;
            var usuario = container.Resolve<IUsuarioRepository>().ObterPeloEmail(email);
            if (usuario != null)
            {
                credito = creditoRepo.ObterCreditoPeloUsuario(Convert.ToInt32(usuario.Id));
            }

            if (String.IsNullOrEmpty(assinatura))
            {
                if (credito == null)
                {
                    TemErro = true;
                    erroVO.erro = "S";
                    erroVO.mensagem = "Não foi encontrada assinatura do serviço de consulta para seu e-mail, adquira um plano de consulta ou entre em contato pelo e-mail subcode23@gmail.com";

                    SalvarErro(erro, erroVO.mensagem);
                }
            }
            else
            {
                var data = Convert.ToDateTime(assinatura);

                if (data < DateTime.Now.Date.AddDays(+1) && credito == null)
                {
                    TemErro = true;
                    erroVO.erro = "S";
                    erroVO.mensagem = "Assinatura do serviço de consulta vencida, por gentileza renove sua assinatura.";

                    SalvarErro(erro, erroVO.mensagem);
                }
            }
        }

        protected void SalvarErro(ErroConsulta erro, String mensagem)
        {
            //Task.Run(() =>
            //{
            //    erro.Mensagem = mensagem;
            //    SalvarErroConsultaHelper.SalvarErro(erro);
            //});
        }
    }
}
