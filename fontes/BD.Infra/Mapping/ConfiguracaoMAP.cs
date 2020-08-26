
using BD.Domain.Entity;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BD.Infra.Mapping
{
    public class ConfiguracaoMAP : ClassMap<Configuracao>
    {
        public ConfiguracaoMAP()
        {
            Table("CONFIGURACAO");
            Id(x => x.Id).GeneratedBy.Increment().Column("ID");
            Map(x => x.AmbienteProducao).Column("AMBIENTEPRODUCAO");
            Map(x => x.SiteManutencao).Column("SITEMANUTENCAO");
            Map(x => x.APIManutencao).Column("APIMANUTENCAO");
            Map(x => x.ArquivoConfiguracaoPagSeguro).Column("ARQCONFIGPAGSEGURO");
            Map(x => x.EmailAdministrador).Column("EMAILADMIN");
        }
    }
}
