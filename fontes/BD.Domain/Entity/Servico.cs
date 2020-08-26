using System;

namespace BD.Domain.Entity
{
    public class Servico
    {
        public virtual Int32 Id { get; set; }
        public virtual String Codigo { get; set; }
        public virtual String DescricaoCurta { get; set; }
        public virtual string DescricaoLonga { get; set; }
        public virtual Decimal Valor { get; set; }
        public virtual String GeraMeses { get; set; }
        public virtual Int32? QuantidadeMeses { get; set; }
        public virtual String GeraCredito { get; set; }
        public virtual Int32? QuantidadeCredito { get; set; }
        public virtual DateTime? DataCadastro { get; set; }
        public virtual String Ativo { get; set; }

    }
}