using BD.DI;
using BD.Domain.Data;
using BD.Domain.Entity;
using BD.Domain.Repository;
using NHibernate.Linq;
using System.Linq;

namespace BD.Infra.Repository
{
    public class CreditoRepository : NHibernateRepository<Credito>, ICreditoRepository
    {
        public CreditoRepository(IUnitOfWork unitOfWork, IDependencyInjectionContainer container) : base(unitOfWork, container)
        {
        }

        public Credito ObterCreditoPeloUsuario(int IdUsuario)
        {
            return Session.Query<Credito>().Where(x => x.Usuario.Id == IdUsuario && x.Quantidade > 0).FirstOrDefault();
        }
    }
}
