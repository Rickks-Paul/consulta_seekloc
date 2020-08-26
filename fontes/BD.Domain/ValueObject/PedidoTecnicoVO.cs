using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BD.Domain.ValueObject
{
    public class PedidoTecnicoVO
    {
        public Int32? NumeroPedidoTecnico { get; set; }
        public Int16 NumeroStatus { get; set; }
        public String StatusTexto { get; set; }
        public String Descricao { get; set; }
        public Int16 NumeroMotivo { get; set; }
        public String DescricaoMotivo { get; set; }
        public DateTime? DataAbertura { get; set; }
        public String DataAberturaTexto { get; set; }
        public DateTime? DataFechamento { get; set; }
        public String DataFechamentoTexto { get; set; }
        public String MotivoFechamento { get; set; }
    }
}
