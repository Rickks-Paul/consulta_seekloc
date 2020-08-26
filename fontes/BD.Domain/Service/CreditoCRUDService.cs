using BD.DI;
using BD.Domain.Data;
using BD.Domain.Entity;
using BD.Domain.Repository;
using System;

namespace BD.Domain.Service
{
    public class CreditoCRUDService
    {
        private static IDependencyInjectionContainer container;

        public CreditoCRUDService(IDependencyInjectionContainer _container)
        {
            container = _container;
        }

        public void Salvar(Credito credito)
        {
            var uow = container.Resolve<IUnitOfWork>();
            var repository = container.Resolve<ICreditoRepository>();
            uow.BeginTransaction();
            repository.Save(credito);
            uow.Commit();
        }

        public Boolean Remover(Int32 IdUsuario, Int32 QuantidadeParaRemover)
        {
            Boolean removeu = false;

            try
            {
                var uow = container.Resolve<IUnitOfWork>();
                var repo = container.Resolve<ICreditoRepository>();
                var credito = repo.ObterCreditoPeloUsuario(IdUsuario);
                if (credito != null)
                {
                    uow.BeginTransaction();
                    credito.Quantidade = credito.Quantidade - QuantidadeParaRemover;
                    credito.DataAlteracao = DateTime.Now;
                    repo.Save(credito);
                    uow.Commit();
                    removeu = true;
                }

            }
            catch (Exception ex)
            {
                String log = ex.Message;
            }

            return removeu;
        }
    }
}
