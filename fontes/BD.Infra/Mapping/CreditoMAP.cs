using BD.Domain.Entity;
using FluentNHibernate.Mapping;

namespace BD.Infra.Mapping
{
    public class CreditoMAP : ClassMap<Credito>
    {
        public CreditoMAP()
        {
            Table("CREDITO");
            Id(x => x.Id).GeneratedBy.Increment().Column("ID");
            Map(x => x.Quantidade).Column("QUANTIDADE");
            Map(x => x.DataCadastro).Column("DATACADASTRO");
            Map(x => x.DataAlteracao).Column("DATAALTERACAO");
            Map(x => x.Descricao).Column("DESCRICAO");

            References(x => x.Pedido).Column("PEDIDO_ID").Cascade.None();
            References(x => x.Usuario).Column("USUARIO_ID").Cascade.None();
        }
    }
}
