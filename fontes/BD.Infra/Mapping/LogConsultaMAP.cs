using BD.Domain.Entity;
using FluentNHibernate.Mapping;

namespace BD.Infra.Mapping
{
    public class LogConsultaMAP : ClassMap<LogConsulta>
    {
        public LogConsultaMAP()
        {
            Table("LOG_CONSULTA");
            Id(x => x.Id).GeneratedBy.Increment().Column("ID");
            Map(x => x.Tipo).Column("TIPO");
            Map(x => x.CPFCNPJ).Column("CPFCNPJ");
            Map(x => x.Origem).Column("ORIGEM");
            Map(x => x.TemResultado).Column("TEMRESULTADO");
            Map(x => x.DataCadastro).Column("DATA_CADASTRO");

            References(x => x.Usuario).Column("USUARIO_ID").Cascade.None();
        }
    }
}
