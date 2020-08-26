using BD.Domain.Entity;
using FluentNHibernate.Mapping;

namespace BD.Infra.Mapping
{
    public class PessoafisicaParenteMap : ClassMap<Parente>
    {

        public PessoafisicaParenteMap()
        {
            Table("PESSOAFISICA_PARENTE");
            Id(x => x.Id).GeneratedBy.Increment().Column("ID");
            References(x => x.PessoaFisica2).Column("PESSOAFISICA_ID");
            Map(x => x.Nome).Column("NOME");
            Map(x => x.CPF).Column("CPF");
        }
    }
}
