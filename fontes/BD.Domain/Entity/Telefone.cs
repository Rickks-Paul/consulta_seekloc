using System;

namespace BD.Domain.Entity
{
    public class Telefone
    {
        public virtual Int32 Id { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual String Modelo { get; set; }
        public virtual String Numero { get; set; }
        public virtual String IMEI { get; set; }
        public virtual String Operadora { get; set; }
        public virtual DateTime DataCadastro { get; set; }
    }
}