using System;

namespace BD.Domain.Entity
{
    public class Assinatura
    {
        public virtual Int32? Id { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual Pedido Pedido { get; set; }
        public virtual DateTime DataInicio { get; set; }
        public virtual DateTime DataFim { get; set; }
        public virtual DateTime? DataCadastro { get; set; }
        public virtual String Descricao { get; set; }
    }
}