using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BD.Domain.ValueObject
{
    public class AssinaturaVO
    {
        public String NumeroPedido { get; set; }
        public String DataPedido { get; set; }
        public String DescricaoCurtaServico { get; set; }
        public String ValorServico { get; set; }
        public String DataInicio { get; set; }
        public String DataFim { get; set; } 
    }
}
