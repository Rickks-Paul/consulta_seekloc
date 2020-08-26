using BD.DI;
using BD.Domain.Entity;
using BD.Domain.Repository;
using BD.Domain.ValueObject;
using BuscaDadosAPI.Domain;
using System;

namespace BD.Domain.Service
{
    public class ValidacaoConsultaCPFService : ValidacaoConsultaBaseService
    {
        private static IDependencyInjectionContainer container;

        public ValidacaoConsultaCPFService(IDependencyInjectionContainer _container) : base(_container)
        {
            container = _container;
        }

        public override void Validate(ConsultaCPFCNPJVO vo)
        {
            base.Validate(vo);

            ErroConsulta = new ErroConsulta();
            ErroConsulta.Cpfcnpj = vo.CPF;
            ErroConsulta.Email = vo.Email;
            ErroConsulta.Aplicativo = !String.IsNullOrEmpty(vo.Aplicativo) ? Convert.ToInt32(vo.Aplicativo) : (Int32?)null;
            ErroConsulta.Data = DateTime.Now;
            ErroConsulta.IMEI = vo.IMEI;
            ErroConsulta.Numero = vo.NumeroCelular;
            ErroConsulta.Versao = vo.Versao;
            ErroConsulta.Ip = vo.IP;

            ValidaCPF(vo, ErroConsulta);

            if (!TemErro)
            {
                ValidaEmail(vo.Email, ErroConsulta);
            }

            if (!TemErro)
            {
                ValidaAssinatura(vo.Email, ErroConsulta);
            }

        }

        protected void ValidaCPF(ConsultaCPFCNPJVO vo, ErroConsulta erro)
        {
            if (String.IsNullOrEmpty(vo.CPF))
            {
                TemErro = true;
                erroVO.erro = "S";
                erroVO.mensagem = "O CPF não foi informado.";

                SalvarErro(erro, erroVO.mensagem);
            }
            else
            {
                if (!DomainUtil.CPFEhValido(vo.CPF))
                {
                    TemErro = true;
                    erroVO.erro = "S";
                    erroVO.mensagem = "O CPF informado não é válido.";

                    SalvarErro(erro, erroVO.mensagem);
                }
                else
                {
                    var CPFRepo = container.Resolve<ICPFBloqueadoRepository>();
                    if (CPFRepo.VerificaCPFBloqueado(vo.CPF))
                    {
                        TemErro = true;
                        erroVO.erro = "S";
                        erroVO.mensagem = "O CPF informado não pode ser consultado devido a solicitação do portador.";

                        SalvarErro(erro, erroVO.mensagem);
                    }
                }
            }
        }
    }
}
