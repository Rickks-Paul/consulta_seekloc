using BD.Domain.Entity;

using FluentNHibernate.Mapping;

namespace BD.Infra.Mapping
{
    public class PessoafisicaVeiculoMap : ClassMap<Veiculo>
    {

        public PessoafisicaVeiculoMap()
        {
            Table("PESSOAFISICA_VEICULO");
            Id(x => x.Id).GeneratedBy.Increment().Column("ID");
            References(x => x.PessoaFisica2).Column("PESSOAFISICA_ID");
            Map(x => x.Placa).Column("PLACA");
            Map(x => x.Renavan).Column("RENAVAM");
            Map(x => x.Chassi).Column("CHASSI");
            Map(x => x.Anofab).Column("ANOFABRICACAO");
            Map(x => x.Anomod).Column("ANOMODELO");
            Map(x => x.Tipo).Column("TIPO");
            Map(x => x.Especie).Column("ESPECIE");
            Map(x => x.Combustivel).Column("COMBUSTIVEL");
        }
    }
}
