using BD.Domain.Entity;
using FluentNHibernate.Mapping;

namespace BD.Infra.Mapping
{
    public class UsuarioMAP : ClassMap<Usuario>
    {
        public UsuarioMAP()
        {
            Table("USUARIO");

            Id(x => x.Id).GeneratedBy.Increment().Column("ID");
            Map(x => x.Nome).Column("NOME");
            Map(x => x.Email).Column("EMAIL");
            Map(x => x.Senha).Column("SENHA");
            Map(x => x.DataCadastro).Column("DATA_CADASTRO");
            Map(x => x.Especial).Column("ESPECIAL").Default("N");
            Map(x => x.Ativo).Column("ATIVO").Default("S");
            Map(x => x.MotivoEspecial).Column("MOTIVO_ESPECIAL");
            Map(x => x.MotivoCadastro).Column("MOTIVO_CADASTRO");
            Map(x => x.CPF).Column("CPF");
            Map(x => x.IPCadastro).Column("IP_CADASTRO");
            Map(x => x.MotivoInativo).Column("MOTIVO_INATIVO");
            Map(x => x.IndicadorPor).Column("INDICADOPOR");
        }
    }
}