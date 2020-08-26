using BD.Domain.Repository;
using BD.Infra.Mapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BD.Infra.Repository
{
    public abstract class OldNHibernateRepository<T> : IRepository<T> where T : class
    {
        private ISession _context;
        private ISessionFactory _factory;

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

        private ISessionFactory CreateSessionFactory()
        {
            ISessionFactory fac = null;

            try
            {
                fac = Fluently.Configure()
                .Database(
                MySQLConfiguration.Standard.ConnectionString(c => c.FromConnectionStringWithKey("buscadadosDB")))
                    .CurrentSessionContext("web")
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<UsuarioMAP>().Conventions.Add(FluentNHibernate.Conventions.Helpers.DefaultLazy.Never()))
                    .BuildSessionFactory();
            }
            catch (Exception e)
            {
                String log = e.Message;
            }

            return fac;
        }

        public void Delete(int id)
        {
            try
            {
                var entity = Session.Load<T>(id);
                Session.Evict(entity);
                Session.Delete(entity);
            }
            catch (Exception ex)
            {
                String log = ex.Message;
                throw;
            }
        }

        public void Delete(T entity)
        {
            try
            {
                Session.Delete(entity);
            }
            catch (Exception ex)
            {
                String log = ex.Message;
                throw;
            }
        }

        public IList<T> GetAll()
        {
            try
            {
                return Session.CreateCriteria(typeof(T)).List<T>();
            }
            catch (Exception ex)
            {
                String log = ex.Message;
                throw;
            }
        }

        public T GetById(int id)
        {
            try
            {
                var entity = Session.Get<T>(id);
                return entity;
            }
            catch (Exception ex)
            {
                String log = ex.Message;
                throw;
            }
        }

        public T Save(T entity)
        {
            try
            {
                Session.SaveOrUpdate(entity);
                return entity;
            }
            catch (Exception ex)
            {
                String log = ex.Message;
                throw;
            }
        }

        public T Update(T entity)
        {
            try
            {
                Session.Update(entity);
                return entity;
            }
            catch (Exception ex)
            {
                String log = ex.Message;
                throw;
            }
        }

        public void Evict(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
