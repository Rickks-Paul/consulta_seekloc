using BD.DI;
using BD.Domain.Data;
using BD.Domain.Entity;
using BD.Domain.Repository;

namespace BD.Infra.Repository
{
    public class ErroConsultaRepository : NHibernateRepository<ErroConsulta>, IErroConsultaRepository
    {
        public ErroConsultaRepository(IUnitOfWork unitOfWork, IDependencyInjectionContainer container) : base(unitOfWork, container)
        {
        }
    }
}
