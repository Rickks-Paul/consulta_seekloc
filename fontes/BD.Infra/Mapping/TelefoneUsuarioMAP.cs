using BD.Domain.Entity;
using FluentNHibernate.Mapping;

namespace BD.Infra.Mapping
{
    public class TelefoneUsuarioMAP : ClassMap<TelefoneUsuario>
    {
        public TelefoneUsuarioMAP()
        {
            Table("TELEFONE");
            Id(x => x.Id).GeneratedBy.Increment().Column("ID");
            References(x => x.Usuario).Column("USUARIO_ID");
            Map(x => x.Modelo).Column("MODELO_CEL");
            Map(x => x.Numero).Column("NUMERO_CEL");
            Map(x => x.IMEI).Column("IMEI");
            Map(x => x.Operadora).Column("OPERADORA");
            Map(x => x.DataCadastro).Column("DATA_CADASTRO");
            Map(x => x.DataAlteracao).Column("DATA_ALTERACAO");
            Map(x => x.Fabricante).Column("FABRICANTE");
        }
    }
}
