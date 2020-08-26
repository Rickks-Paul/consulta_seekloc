using BD.DI;
using BD.Domain.Data;
using BD.Domain.Entity;
using BD.Domain.Repository;

namespace BD.Infra.Repository
{
    public class LogConsultaRepository : NHibernateRepository<LogConsulta>, ILogConsultaRepository
    {
        public LogConsultaRepository(IUnitOfWork unitOfWork, IDependencyInjectionContainer container) : base(unitOfWork, container)
        {
        }
    }
}
