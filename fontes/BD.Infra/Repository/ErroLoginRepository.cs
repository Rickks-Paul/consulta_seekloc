using BD.DI;
using BD.Domain.Data;
using BD.Domain.Entity;
using BD.Domain.Repository;
using NHibernate.Linq;
using System;
using System.Linq;

namespace BD.Infra.Repository
{
    public class ErroLoginRepository : NHibernateRepository<ErroLogin>, IErroLoginRepository
    {
        public ErroLoginRepository(IUnitOfWork unitOfWork, IDependencyInjectionContainer container) : base(unitOfWork, container)
        {
        }

        public bool CometeuMuitosErrosLogin(string email)
        {
            return Session.Query<ErroLogin>().Where(w => w.Email == email && w.Data.Date == DateTime.Now.Date).Count() > 3;
        }
    }
}
