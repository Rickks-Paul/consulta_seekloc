using BD.Domain.Entity;
using FluentNHibernate.Mapping;

namespace BD.Infra.Mapping
{
    public class AssinaturaMAP : ClassMap<Assinatura>
    {
        public AssinaturaMAP()
        {
            Table("ASSINATURA");
            Id(x => x.Id).GeneratedBy.Increment().Column("ID");
            Map(x => x.DataInicio).Column("DATA_INICIO");
            Map(x => x.DataFim).Column("DATA_FIM");
            Map(x => x.DataCadastro).Column("DATA_CADASTRO");
            Map(x => x.Descricao).Column("DESCRICAO");

            References(x => x.Usuario, "USUARIO_ID").Cascade.None();
            References(x => x.Pedido, "PEDIDO_ID").Cascade.None();
        }
    }
}