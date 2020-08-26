using BD.Domain.Entity;
using BD.Domain.ValueObject;
using System;
using System.Collections.Generic;

namespace BD.Domain.Repository
{
    public interface IPedidoTecnicoRepository : IRepository<PedidoTecnico>
    {
        List<PedidoTecnicoVO> ObterPedidosTecnicosUsuario(Int32? IdUsuario);
        List<PedidoTecnico> ObterPedidosTecnicosPeloCPF(String cpf);
        List<PedidoTecnico> ObterPedidosTecnicosUsuarioPeloCPF(Int32? IdUsuario, String cpf);
    }
}
