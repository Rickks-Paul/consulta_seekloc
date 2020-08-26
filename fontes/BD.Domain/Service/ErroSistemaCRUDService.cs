using BD.DI;
using BD.Domain.Data;
using BD.Domain.Entity;
using BD.Domain.Repository;
using System;

namespace BD.Domain.Service
{
    public class ErroSistemaCRUDService : CRUDBase<ErroSistema>
    {
        private static IDependencyInjectionContainer container;

        public ErroSistemaCRUDService(IDependencyInjectionContainer _container)
        {
            container = _container;
        }

        public void Salvar(Exception excecao)
        {
            var uow = container.Resolve<IUnitOfWork>();
            ErroSistema erro = new ErroSistema();
            erro.Data = DateTime.Now;
            erro.Excecao = excecao.Message.Length < 500 ? excecao.Message : excecao.Message.Substring(0, 499);
            erro.ExcecaoInterna = (excecao.InnerException != null && !String.IsNullOrEmpty(excecao.InnerException.Message) ? excecao.InnerException.Message : String.Empty);
            erro.Descricao = excecao.ToString().Substring(0, excecao.ToString().Length < 5000 ? excecao.ToString().Length : 4999);

            var repository = container.Resolve<IErroSistemaRepository>();
            uow.BeginTransaction();
            repository.Save(erro);
            uow.Commit();
        }
    }
}
