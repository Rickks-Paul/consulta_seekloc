using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BD.Domain.ValueObject
{
    public class PedidoVO
    {
        public String NumeroPedido { get; set; }
        public String DataPedido { get; set; }
        public String FormaPagamento { get; set; }
        public String CodigoAutorizacao { get; set; }
        public String StatusPedido { get; set; }
        public String MotivoStatus { get; set; }
        public String CodigoServico { get; set; }
        public String DescricaoCurtaServico { get; set; }
        public String ValorServico { get; set; }
        public String EmiteNF { get; set; }
        public String CPFNF { get; set; }
    }
}
