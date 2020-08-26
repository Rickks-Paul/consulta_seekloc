using BD.Domain.Entity;
using FluentNHibernate.Mapping;

namespace BD.Infra.Mapping
{
    public class PedidoTecnicoMAP : ClassMap<PedidoTecnico>
    {
        public PedidoTecnicoMAP()
        {
            Table("PEDIDO_TECNICO");
            Id(x => x.Id).GeneratedBy.Increment().Column("ID");
            References(x => x.Usuario, "USUARIO_ID").Cascade.None();


            Map(x => x.Motivo).Column("MOTIVO");
            Map(x => x.Descricao).Column("DESCRICAO");
            Map(x => x.Status).Column("STATUS");
            Map(x => x.DataFechamento).Column("DATA_FECHAMENTO");
            Map(x => x.MotivoFechamento).Column("MOTIVO_FECHAMENTO");
            Map(x => x.DataCadastro).Column("DATA_CADASTRO");
            Map(x => x.CPF).Column("CPF");
        }
    }
}
