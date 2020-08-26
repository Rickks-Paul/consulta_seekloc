using System;

namespace BD.Domain.Entity
{
    public class Pedido
    {
        public virtual Int32? Id { get; set; }
        public virtual Servico Servico { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual DateTime Data { get; set; }
        public virtual Int32? FormaPagamento { get; set; }
        public virtual String CodigoAutorizacao { get; set; }
        public virtual String CodigoPedido { get; set; }
        public virtual String EmiteNF { get; set; }
        public virtual String CPFNF { get; set; }
        public virtual String IP { get; set; }
        public virtual Int32? Status { get; set; }
        public virtual String Motivo { get; set; }
        public virtual DateTime? DataAlteracaoStatus { get; set; }
    }
}