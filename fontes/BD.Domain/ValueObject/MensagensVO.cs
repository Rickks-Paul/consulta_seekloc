using BD.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BD.Domain.ValueObject
{
    public class MensagensVO
    {
        public String Mensagem { get; set; }
        public TipoErro TipoErro { get; set; }
    }
}
