using BD.Domain.Entity;
using FluentNHibernate.Mapping;

namespace BD.Infra.Mapping
{
    public class PessoaJuridicaQSAMAP : ClassMap<PessoaJuridicaQSA>
    {
        public PessoaJuridicaQSAMAP()
        {
            Table("PESSOAJURIDICA_QSA");
            Id(x => x.Id).GeneratedBy.Increment().Column("ID");
            References(x => x.PessoaJuridica).Column("PESSOAJURIDICA_ID");
            Map(x => x.Qualificacao).Column("QUALIFICACAO");
            Map(x => x.Nome).Column("NOME");
        }
    }
}
