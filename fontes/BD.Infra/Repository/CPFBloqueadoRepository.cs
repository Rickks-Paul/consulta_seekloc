using BD.DI;
using BD.Domain.Data;
using BD.Domain.Entity;
using BD.Domain.Repository;
using NHibernate.Linq;
using System.Linq;

namespace BD.Infra.Repository
{
    public class CPFBloqueadoRepository : NHibernateRepository<CPFBloqueado>, ICPFBloqueadoRepository
    {
        public CPFBloqueadoRepository(IUnitOfWork unitOfWork, IDependencyInjectionContainer container) : base(unitOfWork, container)
        {
        }

        public bool VerificaCPFBloqueado(string cpf)
        {
            return Session.Query<CPFBloqueado>().Where(w => w.CPF == cpf).Any();
        }
    }
}
