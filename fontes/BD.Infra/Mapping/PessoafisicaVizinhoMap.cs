using BD.Domain.Entity;

using FluentNHibernate.Mapping;

namespace BD.Infra.Mapping
{
    public class PessoafisicaVizinhoMap : ClassMap<Vizinho>
    {

        public PessoafisicaVizinhoMap()
        {
            Table("PESSOAFISICA_VIZINHO");
            Id(x => x.Id).GeneratedBy.Increment().Column("ID");
            References(x => x.PessoaFisica2).Column("PESSOAFISICA_ID");
            Map(x => x.Nome).Column("NOME");
            Map(x => x.CPF).Column("CPF");
            Map(x => x.Tipo).Column("TIPO");
            Map(x => x.Logradouro).Column("LOGRADOURO");
            Map(x => x.Numero).Column("NUMERO");
            Map(x => x.CEP).Column("CEP");
            Map(x => x.Bairro).Column("BAIRRO");
            Map(x => x.Cidade).Column("CIDADE");
            Map(x => x.UF).Column("UF");
        }
    }
}
