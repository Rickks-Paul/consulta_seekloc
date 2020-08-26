using BD.Domain.Entity;
using FluentNHibernate.Mapping;

namespace BD.Infra.Mapping
{
    public class PedidoMAP : ClassMap<Pedido>
    {
        public PedidoMAP()
        {
            Table("PEDIDO");
            Id(x => x.Id).GeneratedBy.Increment().Column("ID");
            Map(x => x.FormaPagamento).Column("FORMA_PAGAMENTO");
            Map(x => x.CodigoAutorizacao).Column("CODIGO_AUTORIZACAO");
            Map(x => x.Data).Column("DATA_PEDIDO");
            Map(x => x.CodigoPedido).Column("CODIGO_PEDIDO");
            Map(x => x.EmiteNF).Column("EMITENF");
            Map(x => x.CPFNF).Column("CPFNF");
            Map(x => x.IP).Column("IP");
            Map(x => x.Status).Column("STATUS");
            Map(x => x.Motivo).Column("MOTIVO");
            Map(x => x.DataAlteracaoStatus).Column("DATA_ALTERACAO");

            References(x => x.Servico).Column("SERVICO_ID").Cascade.None();
            References(x => x.Usuario).Column("USUARIO_ID").Cascade.None();
        }
    }
}