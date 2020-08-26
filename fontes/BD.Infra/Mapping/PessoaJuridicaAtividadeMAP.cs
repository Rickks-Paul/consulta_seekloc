
using BD.Domain.Entity;
using FluentNHibernate.Mapping;

namespace BD.Infra.Mapping
{
    public class PessoaJuridicaAtividadeMAP : ClassMap<PessoaJuridicaAtividade>
    {
        public PessoaJuridicaAtividadeMAP()
        {
            Table("PESSOAJURIDICA_ATIVIDADE");
            Id(x => x.Id).GeneratedBy.Increment().Column("ID");
            References(x => x.PessoaJuridica).Column("PESSOAJURIDICA_ID");
            Map(x => x.Codigo).Column("CODIGO");
            Map(x => x.Texto).Column("TEXTO");
            Map(x => x.Principal).Column("PRINCIPAL");
        }
    }
}
