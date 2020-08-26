using System;

namespace BD.Domain.Entity
{
    public class Parente
    {
        public virtual Int32? Id { get; set; }
        public virtual PessoaFisica2 PessoaFisica2 { get; set; }
        public virtual String Nome { get; set; }
        public virtual String CPF { get; set; }
    }
}
