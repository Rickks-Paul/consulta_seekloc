using BD.Domain.Entity;
using BD.Domain.ValueObject;
using System;
using System.Collections.Generic;

namespace BD.Domain.Repository
{
    public interface IPedidoRepository : IRepository<Pedido>
    {
        List<PedidoVO> ObterTodosPedidosPorEmail(String email);
        List<PedidoVO> ObterResumoValoresPedidosPorMesAno(String email);
    }
}
