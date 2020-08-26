using BD.Domain.Entity;
using FluentNHibernate.Mapping;

namespace BD.Infra.Mapping
{
    public class ErroSistemaMAP : ClassMap<ErroSistema>
    {
        public ErroSistemaMAP()
        {
            Table("ERROSISTEMA");
            Id(x => x.Id).GeneratedBy.Increment().Column("ID");
            Map(x => x.Excecao).Column("EXCECAO");
            Map(x => x.ExcecaoInterna).Column("EXCECAO_INTERNA");
            Map(x => x.Data).Column("DATA_ERRO");
            Map(x => x.Descricao).Column("DESCRICAO");
        }
    }
}
