using BD.Domain.Entity;
using FluentNHibernate.Mapping;

namespace BD.Infra.Mapping
{
    public class PessoafisicaEmpregoMap : ClassMap<Emprego>
    {

        public PessoafisicaEmpregoMap()
        {
            Table("PESSOAFISICA_EMPREGO");
            Id(x => x.Id).GeneratedBy.Increment().Column("ID");
            References(x => x.PessoaFisica2).Column("PESSOAFISICA_ID");
            Map(x => x.Empresa).Column("EMPRESA");
            Map(x => x.CNPJ).Column("CNPJ");
            Map(x => x.Admissao).Column("DATA_ADMISSAO");
        }
    }
}
