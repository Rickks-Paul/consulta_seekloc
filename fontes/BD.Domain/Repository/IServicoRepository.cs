using BD.Domain.Entity;
using BD.Domain.ValueObject;
using System;
using System.Collections.Generic;

namespace BD.Domain.Repository
{
    public interface IServicoRepository : IRepository<Servico>
    {
        Servico ObterPeloCodigo(String codigo);
        List<ServicoVO> ObterTodosAtivos();
    }
}
