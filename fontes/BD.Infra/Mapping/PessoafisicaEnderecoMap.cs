using BD.Domain.Entity;
using FluentNHibernate.Mapping;

namespace BD.Infra.Mapping
{
    public class PessoafisicaEnderecoMap : ClassMap<Endereco>
    {

        public PessoafisicaEnderecoMap()
        {
            Table("PESSOAFISICA_ENDERECO");
            Id(x => x.Id).GeneratedBy.Increment().Column("ID");
            References(x => x.PessoaFisica2).Column("PESSOAFISICA_ID");
            Map(x => x.Tipo).Column("TIPO").Not.Nullable();
            Map(x => x.Logradouro).Column("LOGRADOURO");
            Map(x => x.Complemento).Column("COMPLEMENTO");
            Map(x => x.Numero).Column("NUMERO");
            Map(x => x.Cep).Column("CEP");
            Map(x => x.Bairro).Column("BAIRRO");
            Map(x => x.Cidade).Column("CIDADE");
            Map(x => x.UF).Column("UF");
        }
    }
}
