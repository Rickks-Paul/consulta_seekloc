using BD.Domain.Entity;
using System;

namespace BD.Domain.Repository
{
    public  interface IUsuarioRepository : IRepository<Usuario>
    {
        Usuario ObterPeloEmail(String email);
        Usuario ObterPeloEmailESenha(String email, String senha);
        Boolean VerificarSeUsuarioExiste(String email);
    }
}
