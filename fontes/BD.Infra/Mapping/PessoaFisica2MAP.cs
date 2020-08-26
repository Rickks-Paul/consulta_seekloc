using BD.Domain.Entity;
using FluentNHibernate.Mapping;

namespace BD.Infra.Mapping
{
    public class PessoaFisica2MAP : ClassMap<PessoaFisica2>
    {
        public PessoaFisica2MAP()
        {
            Table("PESSOAFISICA");
            Id(x => x.Id).GeneratedBy.Increment().Column("ID");
            Map(x => x.CPF).Column("CPF");
            Map(x => x.Nome).Column("NOME");
            Map(x => x.Logradouro).Column("LOGRADOURO");
            Map(x => x.NumeroRua).Column("NUMERORUA");
            Map(x => x.Bairro).Column("BAIRRO");
            Map(x => x.ComplementoEndereco).Column("COMPLEMENTO_ENDERECO");
            Map(x => x.CEP).Column("CEP");
            Map(x => x.Municipio).Column("MUNICIPIO");
            Map(x => x.UF).Column("UF");
            Map(x => x.DataNascimento).Column("DATA_NASCIMENTO");
            Map(x => x.NomeMae).Column("NOMEMAE");
            Map(x => x.DataCadastro).Column("DATA_CADASTRO");
            Map(x => x.Importado).Column("IMPORTADO");
            Map(x => x.Hash).Column("HASH");
            Map(x => x.APISeekLoc).Column("APISEEKLOC");
            Map(x => x.Vitalicio).Column("VITALICIO");

            References(x => x.Telefone, "TELEFONE_ID").Cascade.None();
            References(x => x.Aplicativo, "APLICATIVO_ID").Cascade.None();
            References(x => x.UsuarioConsulta, "USUARIO_ID").Cascade.None();

            HasMany(x => x.Emails).KeyColumn("PESSOAFISICA_ID").Cascade.All();
            HasMany(x => x.Empregos).KeyColumn("PESSOAFISICA_ID").Cascade.All();
            HasMany(x => x.Enderecos).KeyColumn("PESSOAFISICA_ID").Cascade.All();
            HasMany(x => x.Parentes).KeyColumn("PESSOAFISICA_ID").Cascade.All();
            HasMany(x => x.Vizinhos).KeyColumn("PESSOAFISICA_ID").Cascade.All();
            HasMany(x => x.Veiculos).KeyColumn("PESSOAFISICA_ID").Cascade.All();
            HasMany(x => x.Telefones).KeyColumn("PESSOAFISICA_ID").Cascade.All();
        }
    }
}
