using BD.Domain.Entity;
using FluentNHibernate.Mapping;

namespace BD.Infra.Mapping
{
    public class ErroLoginMAP : ClassMap<ErroLogin>
    {
        public ErroLoginMAP()
        {
            Table("ERRO_LOGIN");
            Id(x => x.ID).GeneratedBy.Increment().Column("ID");
            Map(x => x.Data).Column("DATA");
            Map(x => x.IP).Column("IP");
            Map(x => x.Email).Column("EMAIL");
            Map(x => x.Senha).Column("SENHA");
            Map(x => x.Operadora).Column("OPERADORA");
            Map(x => x.IMEI).Column("IMEI");
        }
    }
}
