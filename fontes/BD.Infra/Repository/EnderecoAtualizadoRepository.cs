using BD.DI;
using BD.Domain.Data;
using BD.Domain.Entity;
using BD.Domain.Repository;
using NHibernate.Linq;
using System.Linq;

namespace BD.Infra.Repository
{
    public class EnderecoAtualizadoRepository : NHibernateRepository<EnderecoAtualizado>, IEnderecoAtualizadoRepository
    {
        public EnderecoAtualizadoRepository(IUnitOfWork unitOfWork, IDependencyInjectionContainer container) : base(unitOfWork, container)
        {
        }

        public EnderecoAtualizado ObterEndereco(string cpf)
        {
            return Session.Query<EnderecoAtualizado>().Where(w => w.CPF == cpf).FirstOrDefault();
        }
    }
}
