using BD.Domain.Entity;
using FluentNHibernate.Mapping;

namespace BD.Infra.Mapping
{
    public class AtividadeMAP : ClassMap<Atividade>
    {
        public AtividadeMAP()
        {
            Table("ATIVIDADE");
            Id(x => x.Id).GeneratedBy.Increment().Column("ID");
            Map(x => x.Data).Column("DATA");
            Map(x => x.IP).Column("IP");
            Map(x => x.TipoAtividade).Column("TIPO_ATIVIDADE");
            Map(x => x.Descricao).Column("DESCRICAO");
            Map(x => x.Origem).Column("ORIGEM");

            Map(x => x.Cidade).Column("CIDADE");
            Map(x => x.Pais).Column("PAIS");
            Map(x => x.Provedor).Column("PROVEDOR");
            Map(x => x.ModeloCelular).Column("MODELO_CELULAR");
            Map(x => x.IMEI).Column("IMEI");
            Map(x => x.Operadora).Column("OPERADORA");

            References(x => x.Usuario).Column("USUARIO_ID").Cascade.None();
        }
    }
}