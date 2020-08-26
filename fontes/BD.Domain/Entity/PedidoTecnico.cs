
using System;

namespace BD.Domain.Entity
{
    public class PedidoTecnico
    {
        public virtual Int32? Id { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual Int16? Motivo { get; set; }
        public virtual String Descricao { get; set; }
        public virtual Int16? Status { get; set; }
        public virtual DateTime? DataFechamento { get; set; }
        public virtual String MotivoFechamento { get; set; }
        public virtual DateTime? DataCadastro { get; set; }
        public virtual String CPF { get; set; }
    }
}
