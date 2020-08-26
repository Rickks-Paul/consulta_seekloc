using BD.DI;
using BD.Domain.Data;
using BD.Domain.Entity;
using BD.Domain.Repository;

namespace BD.Infra.Repository
{
    public class ConfiguracaoRepository : NHibernateRepository<Configuracao>, IConfiguracaoRepository
    {
        public ConfiguracaoRepository(IUnitOfWork unitOfWork, IDependencyInjectionContainer container) : base(unitOfWork, container)
        {
        }
    }
}
