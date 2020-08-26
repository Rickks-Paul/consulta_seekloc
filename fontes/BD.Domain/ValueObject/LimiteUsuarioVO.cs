using BD.Domain.Entity;
using System;

namespace BD.Domain.ValueObject
{
    public class LimiteUsuarioVO : LimiteUsuario
    {
        public virtual Int32 LimiteConsultas { get; set; }
    }
}
