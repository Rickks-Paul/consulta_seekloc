using System;

namespace BD.Domain.Entity
{
    public class RecuperacaoSenha
    {
        public virtual Int64 Id { get; set; }
        public virtual String Email { get; set; }
        public virtual String Chave { get; set; }
        public virtual DateTime Data { get; set; }
        public virtual String IP { get; set; }
        public virtual Int32 Status { get; set; }
    }
}