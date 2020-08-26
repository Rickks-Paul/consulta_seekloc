using BD.DI;
using BD.Domain.Data;
using BD.Domain.Entity;
using BD.Domain.Repository;
using System;

namespace BD.Infra.Repository
{
    public class TelefoneUsuarioRepository : NHibernateRepository<TelefoneUsuario>, ITelefoneUsuarioRepository
    {
        public TelefoneUsuarioRepository(IUnitOfWork unitOfWork, IDependencyInjectionContainer container) : base(unitOfWork, container)
        {
        }

        public TelefoneUsuario ObterTelefonePorUsuarioEIMEI(int? IdUsuario, string IMEI)
        {
            throw new NotImplementedException();
        }
    }
}
