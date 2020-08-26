using BD.Domain.Entity;
using BD.Domain.Repository;
using BD.Domain.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Linq;
using BD.DI;
using BD.Domain.Data;

namespace BD.Infra.Repository
{
    public class AssinaturaRepository : NHibernateRepository<Assinatura>, IAssinaturaRepository
    {
        public AssinaturaRepository(IUnitOfWork unitOfWork, IDependencyInjectionContainer container) : base(unitOfWork, container)
        {
        }

        public Assinatura ObterAssinaturaMaisRecentePeloEmailDoUsuario(string email)
        {
            return Session.Query<Assinatura>().Where(w => w.Usuario.Email.ToUpper() == email.ToUpper()).OrderByDescending(x => x.DataFim).FirstOrDefault();
        }

        public string ObterDataFimAssinaturaMaisRecentePeloEmailDoUsuario(string email, bool somenteData = false)
        {
            var data = String.Empty;
            var assinatura = Session.Query<Assinatura>().Where(w => w.Usuario.Email.ToUpper() == email.ToUpper()).OrderByDescending(x => x.DataFim).FirstOrDefault();
            if (assinatura != null)
            {
                String formato = somenteData ? "{0:dd/MM/yyyy}" : "{0:dd/MM/yyyy HH:mm:ss}";
                data = String.Format(formato, assinatura.DataFim);
            }
            return data;
        }

        public List<Assinatura> ObterTodasAssinaturasPorEmail(string email)
        {
            return Session.Query<Assinatura>().Where(w => w.Usuario.Email.ToUpper() == email.ToUpper()).OrderByDescending(x => x.Id).ToList();
        }

        public List<AssinaturaVO> ObterTodasAssinaturasVOPorEmail(string email)
        {
            var q = from p in Session.Query<Assinatura>().Where(w => w.Usuario.Email.ToUpper() == email.ToUpper())
                    orderby p.Id descending
                    select new AssinaturaVO() { NumeroPedido = p.Pedido != null ? p.Pedido.Id.ToString() : String.Empty, DataPedido = p.Pedido.Data.ToString("dd/MM/yyyy HH:mm:ss"), DescricaoCurtaServico = p.Pedido.Servico.DescricaoCurta, ValorServico = String.Format("{0:0.00}", p.Pedido.Servico.Valor), DataInicio = p.DataInicio.ToString("dd/MM/yyyy HH:mm:ss"), DataFim = p.DataFim.ToString("dd/MM/yyyy HH:mm:ss") };
            return q.ToList();
        }
    }
}
