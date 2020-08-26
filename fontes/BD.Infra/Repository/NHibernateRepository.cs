using BD.DI;
using BD.Domain.Data;
using BD.Domain.Repository;
using BD.Infra.UnitOfWork;
using NHibernate;
using System;
using System.Collections.Generic;

namespace BD.Infra.Repository
{
    public abstract class NHibernateRepository<T> : IRepository<T> where T : class
    {
        private readonly NHibernateUnitOfWork _unitOfWork;
        private readonly IDependencyInjectionContainer _container;

        public NHibernateRepository(IUnitOfWork unitOfWork, IDependencyInjectionContainer container)
        {
            if (unitOfWork == null) throw new ArgumentNullException("unitOfWork");
            _unitOfWork = (NHibernateUnitOfWork)unitOfWork;

            _container = container;
        }

        protected ISession Session
        {
            get
            {
                return _unitOfWork.Session;
            }
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
            Session.Evict(entity);
        }
    }
}
