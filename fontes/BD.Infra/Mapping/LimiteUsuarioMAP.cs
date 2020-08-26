using BD.Domain.Entity;
using FluentNHibernate.Mapping;

namespace BD.Infra.Mapping
{
    public class LimiteUsuarioMAP : ClassMap<LimiteUsuario>
    {
        public LimiteUsuarioMAP()
        {
            Table("USUARIO_LIMITECONSULTA");
            Id(x => x.Id).GeneratedBy.Increment().Column("ID");
            References(x => x.Usuario).Column("USUARIO_ID");
            Map(x => x.Quantidade).Column("QUANTIDADEDIA");
            Map(x => x.DataCadastro).Column("DATA_CADASTRO");
        }
    }
}
