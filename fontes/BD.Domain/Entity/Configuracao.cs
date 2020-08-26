using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BD.Domain.Entity
{
    public class Configuracao
    {
        public virtual Int32 Id { get; set; }
        public virtual String AmbienteProducao { get; set; }
        public virtual String SiteManutencao { get; set; }
        public virtual String APIManutencao { get; set; }
        public virtual String ArquivoConfiguracaoPagSeguro { get; set; }
        public virtual String EmailAdministrador { get; set; }
    }
}
