using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BD.Domain.ValueObject
{
    public class TokenVO
    {
        public String Token { get; set; }
        public String CriadoEm { get; set; }
        public String ExpiraEm { get; set; }
    }
}
