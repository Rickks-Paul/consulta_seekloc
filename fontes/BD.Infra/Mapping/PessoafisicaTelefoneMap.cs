using BD.Domain.Entity;
using FluentNHibernate.Mapping;

namespace BD.Infra.Mapping
{
    public class PessoafisicaTelefoneMap : ClassMap<Telefone2>
    {

        public PessoafisicaTelefoneMap()
        {
            Table("PESSOAFISICA_TELEFONE");
            Id(x => x.Id).GeneratedBy.Increment().Column("ID");
            References(x => x.PessoaFisica2).Column("PESSOAFISICA_ID");
            Map(x => x.DDD).Column("DDD");
            Map(x => x.Numero).Column("NUMERO");
        }
    }
}
