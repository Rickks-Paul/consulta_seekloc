using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BD.Domain.Entity
{
    public class ErroLogin
    {
        public virtual Int64? ID { get; set; }
        public virtual String Email { get; set; }
        public virtual String Senha { get; set; }
        public virtual String IP { get; set; }
        public virtual String Operadora { get; set; }
        public virtual String IMEI { get; set; }
        public virtual DateTime Data { get; set; }
    }
}
