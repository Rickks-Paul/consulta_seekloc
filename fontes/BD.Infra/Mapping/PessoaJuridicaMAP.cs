using BD.Domain.Entity;
using FluentNHibernate.Mapping;

namespace BD.Infra.Mapping
{
    public class PessoaJuridicaMAP : ClassMap<PessoaJuridica>
    {
        public PessoaJuridicaMAP()
        {
            Table("PESSOAJURIDICA");

            Id(x => x.Id).GeneratedBy.Increment().Column("ID");
            Map(x => x.Nome).Column("NOME");
            Map(x => x.CNPJ).Column("CNPJ");
            Map(x => x.Complemento).Column("COMPLEMENTO");
            Map(x => x.Situacao).Column("SITUACAO");
            Map(x => x.Logradouro).Column("LOGRADOURO");
            Map(x => x.Numero).Column("NUMERO");
            Map(x => x.Bairro).Column("BAIRRO");
            Map(x => x.Municipio).Column("MUNICIPIO");
            Map(x => x.CEP).Column("CEP");
            Map(x => x.UF).Column("UF");
            Map(x => x.NumeroTelefone).Column("TELEFONE");
            Map(x => x.DataAbertura).Column("DATA_ABERTURA");
            Map(x => x.NaturezaJuridica).Column("NATUREZA_JURIDICA");
            Map(x => x.Status).Column("STATUS");
            Map(x => x.Tipo).Column("TIPO");
            Map(x => x.NomeFantasia).Column("NOME_FANTASIA");
            Map(x => x.SituacaoEspecial).Column("SITUACAO_ESPECIAL");
            Map(x => x.MotivoSituacaoEspecial).Column("MOTIVO_SITUACAOESP");
            Map(x => x.DataSituacaoEspecial).Column("DATA_SITUACAOESP");
            Map(x => x.CapitalSocial).Column("CAPITALSOCIAL");
            Map(x => x.UltimaAtualizacao).Column("ULTIMA_ATUALIZACAO");
            Map(x => x.Email).Column("EMAIL");
            Map(x => x.DataCadastro).Column("DATA_CADASTRO");
            Map(x => x.Importado).Column("IMPORTADO");

            References(x => x.Usuario, "USUARIO_ID").Cascade.None();
            References(x => x.Aplicativo, "APLICATIVO_ID").Cascade.None();
            References(x => x.Telefone, "TELEFONE_ID").Cascade.None();

            HasMany(x => x.Atividades).KeyColumn("PESSOAJURIDICA_ID").Cascade.All();
            HasMany(x => x.QSAs).KeyColumn("PESSOAJURIDICA_ID").Cascade.All();
        }
    }
}
