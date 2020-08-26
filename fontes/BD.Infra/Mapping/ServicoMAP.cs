using BD.Domain.Entity;
using FluentNHibernate.Mapping;

namespace BD.Infra.Mapping
{
    public class ServicoMAP : ClassMap<Servico>
    {
        public ServicoMAP()
        {
            Table("SERVICO");
            Id(x => x.Id).GeneratedBy.Increment().Column("ID");
            Map(x => x.Codigo).Column("CODIGO");
            Map(x => x.DescricaoCurta).Column("DESCRICAOCURTA");
            Map(x => x.DescricaoLonga).Column("DESCRICAOLONGA");
            Map(x => x.Valor).Column("VALOR");
            Map(x => x.GeraMeses).Column("GERAMESES");
            Map(x => x.QuantidadeMeses).Column("QTDMESES");
            Map(x => x.GeraCredito).Column("GERACREDITO");
            Map(x => x.QuantidadeCredito).Column("QTDCREDITO");
            Map(x => x.DataCadastro).Column("DATA_CADASTRO");
            Map(x => x.Ativo).Column("ATIVO");
        }
    }
}