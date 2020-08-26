
using System;

namespace BD.Domain.Entity
{
    public class LogConsulta
    {
        public virtual Int32? Id { get; set; }
        public Usuario Usuario { get; set; }
        public virtual Int32? Tipo { get; set; }
        public virtual String CPFCNPJ { get; set; }
        public virtual Int32? Origem { get; set; }
        public virtual String TemResultado { get; set; }
        public virtual DateTime? DataCadastro { get; set; }
    }
}
