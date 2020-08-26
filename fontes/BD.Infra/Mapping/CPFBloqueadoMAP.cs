
using BD.Domain.Entity;
using FluentNHibernate.Mapping;

namespace BD.Infra.Mapping
{
    public class CPFBloqueadoMAP : ClassMap<CPFBloqueado>
    {
        public CPFBloqueadoMAP()
        {
            Table("CPF_BLOQUEADO");
            Id(x => x.Id).GeneratedBy.Increment().Column("ID");
            Map(x => x.CPF).Column("CPF");
            Map(x => x.Motivo).Column("MOTIVO");
            Map(x => x.DataCadastro).Column("DATA_CADASTRO");
        }
    }
}