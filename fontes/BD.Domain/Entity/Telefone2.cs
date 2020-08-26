using System;

namespace BD.Domain.Entity
{
    public class Telefone2
    {
        public virtual Int32? Id { get; set; }
        public virtual PessoaFisica2 PessoaFisica2 { get; set; }
        public virtual String DDD { get; set; }
        public virtual String Numero { get; set; }
    }
}
