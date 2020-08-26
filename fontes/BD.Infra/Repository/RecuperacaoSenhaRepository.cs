using BD.DI;
using BD.Domain.Data;
using BD.Domain.Entity;
using BD.Domain.Repository;
using System;

namespace BD.Infra.Repository
{
    public class RecuperacaoSenhaRepository : NHibernateRepository<RecuperacaoSenha>, IRecuperacaoSenhaRepository
    {
        public RecuperacaoSenhaRepository(IUnitOfWork unitOfWork, IDependencyInjectionContainer container) : base(unitOfWork, container)
        {
        }

        public RecuperacaoSenha ObterPeloChave(string chave)
        {
            throw new NotImplementedException();
        }
    }
}
