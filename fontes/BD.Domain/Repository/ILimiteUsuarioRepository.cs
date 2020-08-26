using BD.Domain.Entity;
using BD.Domain.ValueObject;
using System;

namespace BD.Domain.Repository
{
    public interface ILimiteUsuarioRepository : IRepository<LimiteUsuario>
    {
        LimiteUsuarioVO ObterLimiteUsuarioPeloEmail(String email);
    }
}
