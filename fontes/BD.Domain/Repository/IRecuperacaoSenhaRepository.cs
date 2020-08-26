using BD.Domain.Entity;
using System;

namespace BD.Domain.Repository
{
    public interface IRecuperacaoSenhaRepository : IRepository<RecuperacaoSenha>
    {
        RecuperacaoSenha ObterPeloChave(String chave);
    }
}
