using BD.Domain.Entity;
using FluentNHibernate.Mapping;

namespace BD.Infra.Mapping
{
    public class AplicativoMAP : ClassMap<Aplicativo>
    {
        public AplicativoMAP()
        {
            Table("APLICATIVO");
            Id(x => x.Id).GeneratedBy.Increment().Column("ID");
            Map(x => x.Nome).Column("NOME");
            Map(x => x.Versao).Column("VERSAO");
            Map(x => x.DataCadastro).Column("DATACADASTRO");
        }
    }
}