
using System;

namespace BD.Domain.Entity
{
    public class LimiteUsuario
    {
        public virtual Int32? Id { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual Int32 Quantidade { get; set; }
        public virtual DateTime? DataCadastro { get; set; }
    }
}
