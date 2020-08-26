using BD.DI;
using BD.Domain.Data;
using BD.Domain.Entity;
using BD.Domain.Repository;

namespace BD.Domain.Service
{
    public class AssinaturaCRUDService
    {
        private static IDependencyInjectionContainer container;

        public AssinaturaCRUDService(IDependencyInjectionContainer _container)
        {
            container = _container;
        }

        public void Salvar(Assinatura assinatura)
        {
            var uow = container.Resolve<IUnitOfWork>();
            var repository = container.Resolve<IAssinaturaRepository>();
            uow.BeginTransaction();
            repository.Save(assinatura);
            uow.Commit();
        }
    }
}
