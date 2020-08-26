using BD.Domain.Entity;
using FluentNHibernate.Mapping;

namespace BD.Infra.Mapping
{
    public class RecuperacaoSenhaMAP : ClassMap<RecuperacaoSenha>
    {
        public RecuperacaoSenhaMAP()
        {
            Table("RECUPERA_SENHA");
            Id(x => x.Id).GeneratedBy.Increment().Column("ID");
            Map(x => x.Email).Column("EMAIL");
            Map(x => x.Chave).Column("CHAVE");
            Map(x => x.Data).Column("DATA");
            Map(x => x.IP).Column("IP");
            Map(x => x.Status).Column("STATUS");
        }
    }
}