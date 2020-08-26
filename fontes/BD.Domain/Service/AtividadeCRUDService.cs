using BD.DI;
using BD.Domain.Data;
using BD.Domain.Entity;
using BD.Domain.Repository;

namespace BD.Domain.Service
{
    public class AtividadeCRUDService : CRUDBase<Atividade>
    {
        private static IDependencyInjectionContainer container;

        public AtividadeCRUDService(IDependencyInjectionContainer _container)
        {
            container = _container;
        }

        public void Salvar(Atividade atividade)
        {
            var uow = container.Resolve<IUnitOfWork>();
            var repository = container.Resolve<IAtividadeRepository>();
            uow.BeginTransaction();
            repository.Save(atividade);
            uow.Commit();
        }
    }
}
