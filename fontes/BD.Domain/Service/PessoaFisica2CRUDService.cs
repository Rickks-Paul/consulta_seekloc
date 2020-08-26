using BD.DI;
using BD.Domain.Data;
using BD.Domain.Entity;
using BD.Domain.Repository;

namespace BD.Domain.Service
{
    public class PessoaFisica2CRUDService
    {
        private static IDependencyInjectionContainer container;

        public PessoaFisica2CRUDService(IDependencyInjectionContainer _container)
        {
            container = _container;
        }

        public void Salvar(PessoaFisica2 pessoa)
        {
            var uow = container.Resolve<IUnitOfWork>();
            var repository = container.Resolve<IPessoaFisica2Repository>();
            uow.BeginTransaction();
            repository.Save(pessoa);
            uow.Commit();
        }
    }
}
