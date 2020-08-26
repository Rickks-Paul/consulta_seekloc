using System;

namespace BD.Domain.Entity
{
    public class Email
    {
        public virtual Int32? Id { get; set; }
        public virtual PessoaFisica2 PessoaFisica2 { get; set; }
        public virtual String Valor { get; set; }
    }
}
