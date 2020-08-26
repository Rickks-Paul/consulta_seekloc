using BD.Domain.Entity;
using FluentNHibernate.Mapping;

namespace BD.Infra.Mapping
{
    public class ErroConsultaMAP : ClassMap<ErroConsulta>
    {
        public ErroConsultaMAP()
        {
            Table("ERROCONSULTA");
            Id(x => x.Id).GeneratedBy.Increment().Column("ID");
            Map(x => x.Email).Column("EMAIL");
            Map(x => x.Cpfcnpj).Column("CPFCNPJ");
            Map(x => x.Ip).Column("IP");
            Map(x => x.Aplicativo).Column("APLICATIVO");
            Map(x => x.Versao).Column("VERSAO");
            Map(x => x.Numero).Column("NUMERO");
            Map(x => x.IMEI).Column("IMEI");
            Map(x => x.Data).Column("DATA");
            Map(x => x.Mensagem).Column("MENSAGEM");
        }
    }
}