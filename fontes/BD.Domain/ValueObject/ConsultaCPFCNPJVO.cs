using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BD.Domain.ValueObject
{
    public class ConsultaCPFCNPJVO
    {
        public String CPF { get; set; }
        public String CNPJ { get; set; }
        public String Email { get; set; }
        public String Aplicativo { get; set; }
        public String Versao { get; set; }
        public String ModeloCelular { get; set; }
        public String NumeroCelular { get; set; }
        public String IMEI { get; set; }
        public String Operadora { get; set; }
        public String Hash { get; set; }
        public String IP { get; set; }
    }
}
