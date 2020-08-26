using System;

namespace BD.Domain.Entity
{
    public class Credito
    {
        public virtual Int32? Id { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual Pedido Pedido { get; set; }
        public virtual Int32 Quantidade { get; set; }
        public virtual DateTime? DataCadastro { get; set; }
        public virtual DateTime? DataAlteracao { get; set; }
        public virtual String Descricao { get; set; }

    }
}
