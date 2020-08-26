using BD.DI;
using BD.Domain.Data;
using BD.Domain.Entity;
using BD.Domain.Repository;

namespace BD.Domain.Service
{
    public class LogConsultaCRUDService
    {
        private static IDependencyInjectionContainer container;

        public LogConsultaCRUDService(IDependencyInjectionContainer _container)
        {
            container = _container;
        }

        public void Salvar(LogConsulta log)
        {
            var uow = container.Resolve<IUnitOfWork>();
            var repository = container.Resolve<ILogConsultaRepository>();
            uow.BeginTransaction();
            repository.Save(log);
            uow.Commit();
        }

    }
}
