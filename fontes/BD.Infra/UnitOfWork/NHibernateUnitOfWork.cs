using BD.Domain.Data;
using BD.Infra.Mapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using System;

namespace BD.Infra.UnitOfWork
{
    public class NHibernateUnitOfWork : IUnitOfWork
    {
        //private readonly DBEnvironment _environment;

        private ISession _context;
        private ISessionFactory _factory;
        private ITransaction _transaction;

        private ISessionFactory CreateSessionFactory()
        {
            ISessionFactory fac = null;

            try
            {
                fac = Fluently.Configure()
                .Database(
                MySQLConfiguration.Standard.ConnectionString(c => c.FromConnectionStringWithKey("buscadadosDB2")))
                    .CurrentSessionContext("web")
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<UsuarioMAP>().Conventions.Add(FluentNHibernate.Conventions.Helpers.DefaultLazy.Never()))
                    .BuildSessionFactory();
            }
            catch (Exception e)
            {
                String log = e.Message;
                throw;
            }

            return fac;
        }

        public ISession Session
        {
            get
            {
                if (_factory == null)
                {
                    _factory = CreateSessionFactory();
                }

                if (_context == null || !_context.IsOpen)
                {
                    _context = _factory.OpenSession();
                }
                return _context;
            }
        }

        public void BeginTransaction()
        {
            if (_context != null && _context.IsOpen)
            {
                _transaction = _context.BeginTransaction();
            }
            else
            {
                _transaction = Session.BeginTransaction();
            }
        }

        public void Commit()
        {
            try
            {
                if (_transaction != null && _transaction.IsActive)
                    _transaction.Commit();
            }
            catch (Exception)
            {
                if (_transaction != null && _transaction.IsActive)
                    _transaction.Rollback();

                throw;
            }
        }

        public void Rollback()
        {
            try
            {
                //Se ocorrer rollback descarta a sessão, necessário para não ocorrer o erro null id in entry (don't flush the Session after an exception occurs)
                //e conforme documentação em http://nhibernate.info/doc/nhibernate-reference/manipulatingdata.html
                if (_transaction != null && _transaction.IsActive)
                {
                    _transaction.Rollback();
                    Session.Dispose();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
