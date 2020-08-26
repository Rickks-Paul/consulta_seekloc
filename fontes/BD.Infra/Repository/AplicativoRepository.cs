using BD.DI;
using BD.Domain.Data;
using BD.Domain.Entity;
using BD.Domain.Repository;

namespace BD.Infra.Repository
{
    public class AplicativoRepository : NHibernateRepository<Aplicativo>, IAplicativoRepository
    {
        public AplicativoRepository(IUnitOfWork unitOfWork, IDependencyInjectionContainer container) : base(unitOfWork, container)
        {
        }
    }
}
