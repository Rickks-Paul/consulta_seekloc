using System;

namespace BD.Domain.Entity
{
    public class CPFBloqueado
    {
        public virtual Int32? Id { get; set; }
        public virtual String CPF { get; set; }
        public virtual String Motivo { get; set; }
        public virtual DateTime? DataCadastro { get; set; }
    }
}