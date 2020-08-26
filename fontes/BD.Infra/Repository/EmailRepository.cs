using BD.DI;
using BD.Domain.Data;
using BD.Domain.Entity;
using BD.Domain.Repository;

namespace BD.Infra.Repository
{
    public class EmailRepository : NHibernateRepository<Email>, IEmailRepository
    {
        public EmailRepository(IUnitOfWork unitOfWork, IDependencyInjectionContainer container) : base(unitOfWork, container)
        {
        }
    }
}
