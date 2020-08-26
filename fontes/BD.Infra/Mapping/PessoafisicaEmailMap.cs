using BD.Domain.Entity;
using FluentNHibernate.Mapping;

namespace BD.Infra.Mapping
{
    public class PessoafisicaEmailMap : ClassMap<Email>
    {

        public PessoafisicaEmailMap()
        {
            Table("PESSOAFISICA_EMAIL");
            Id(x => x.Id).GeneratedBy.Increment().Column("ID");
            References(x => x.PessoaFisica2).Column("PESSOAFISICA_ID");
            Map(x => x.Valor).Column("EMAIL");
        }
    }
}
