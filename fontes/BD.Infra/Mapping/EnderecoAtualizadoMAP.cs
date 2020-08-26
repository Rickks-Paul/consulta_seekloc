using BD.Domain.Entity;
using FluentNHibernate.Mapping;

namespace BD.Infra.Mapping
{
    public class EnderecoAtualizadoMAP : ClassMap<EnderecoAtualizado>
    {
        public EnderecoAtualizadoMAP()
        {
            Table("PESSOAFISICA_ENDERECO_ATUALIZADO");
            Id(x => x.Id).GeneratedBy.Increment().Column("ID");
            Map(x => x.Bairro).Column("BAIRRO");
            Map(x => x.CEP).Column("CEP");
            Map(x => x.Cidade).Column("CIDADE");
            Map(x => x.Complemento).Column("COMPLEMENTO");

            Map(x => x.CPF).Column("CPF");
            Map(x => x.DataCadastro).Column("DATACADASTRO");
            Map(x => x.Logradouro).Column("LOGRADOURO");
            Map(x => x.Numero).Column("NUMERO");
            Map(x => x.Origem).Column("ORIGEM");
            Map(x => x.UF).Column("UF");


            Map(x => x.Nome).Column("NOME");
            Map(x => x.NomeMae).Column("NOMEMAE");
            Map(x => x.DataNascimento).Column("DATA_NASCIMENTO");

        }
    }
}
