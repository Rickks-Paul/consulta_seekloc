using BD.DI;
using BD.Domain.Data;
using BD.Domain.Entity;
using BD.Domain.Repository;

namespace BD.Infra.Repository
{
    public class PessoaFisica2Repository : NHibernateRepository<PessoaFisica2>, IPessoaFisica2Repository
    {
        public PessoaFisica2Repository(IUnitOfWork unitOfWork, IDependencyInjectionContainer container) : base(unitOfWork, container)
        {
        }
    }
}
