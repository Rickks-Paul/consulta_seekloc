using BD.Domain.Entity;
using FluentNHibernate.Mapping;

namespace BD.Infra.Mapping
{
    public class TelefoneMAP : ClassMap<Telefone>
    {
        public TelefoneMAP()
        {
            Table("TELEFONE");
            Id(x => x.Id).GeneratedBy.Increment().Column("ID");
            Map(x => x.Operadora).Column("OPERADORA");
            Map(x => x.Modelo).Column("MODELO_CEL");
            Map(x => x.Numero).Column("NUMERO_CEL");
            Map(x => x.IMEI).Column("IMEI");
            Map(x => x.DataCadastro).Column("DATA_CADASTRO");

            References(x => x.Usuario, "USUARIO_ID").Cascade.None();
        }
    }
}