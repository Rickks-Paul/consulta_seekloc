using BD.DI;
using BD.Domain.Data;
using BD.Domain.Entity;
using BD.Domain.Repository;
using NHibernate.Linq;
using System.Linq;

namespace BD.Infra.Repository
{
    public class UsuarioRepository : NHibernateRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(IUnitOfWork unitOfWork, IDependencyInjectionContainer container) : base(unitOfWork, container)
        {
        }

        public Usuario ObterPeloEmail(string email)
        {
            return Session.Query<Usuario>().Where(w => w.Email.ToUpper() == email.ToUpper()).FirstOrDefault();
        }

        public Usuario ObterPeloEmailESenha(string email, string senha)
        {
            return Session.Query<Usuario>().Where(w => w.Email.ToUpper() == email.ToUpper() && w.Senha == senha).FirstOrDefault();
        }

        public bool VerificarSeUsuarioExiste(string email)
        {
            return Session.Query<Usuario>().Where(w => w.Email.ToUpper() == email.ToUpper()).Any();
        }
    }
}
