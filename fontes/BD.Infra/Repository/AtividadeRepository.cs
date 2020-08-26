using BD.Domain.Entity;
using BD.Domain.Repository;
using System;
using NHibernate.Linq;
using System.Linq;
using BD.Domain.Enum;
using BD.DI;
using BD.Domain.Data;

namespace BD.Infra.Repository
{
    public class AtividadeRepository : NHibernateRepository<Atividade>, IAtividadeRepository
    {
        public AtividadeRepository(IUnitOfWork unitOfWork, IDependencyInjectionContainer container) : base(unitOfWork, container)
        {
        }

        public string ObterUltimoLogonPeloEmail(string email)
        {
            var data = String.Empty;
            var atividade = Session.Query<Atividade>().Where(w => w.Usuario.Email.ToUpper() == email.ToUpper() && w.TipoAtividade == Convert.ToInt32(TipoAtividade.LogonUsuario)).OrderByDescending(x => x.Id).FirstOrDefault();
            if (atividade != null)
            {
                data = String.Format("{0:dd/MM/yyyy HH:mm:ss}", atividade.Data); ;
            }
            return data;
        }
    }
}
