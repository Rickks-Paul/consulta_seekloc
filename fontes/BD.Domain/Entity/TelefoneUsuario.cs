
using System;

namespace BD.Domain.Entity
{
    public class TelefoneUsuario : Telefone
    {
        public virtual DateTime? DataAlteracao { get; set; }
        public virtual String Fabricante { get; set; }
    }
}
