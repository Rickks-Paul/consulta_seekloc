using System;

namespace BD.Domain.Entity
{
    public class Emprego
    {
        public virtual Int32? Id { get; set; }
        public virtual PessoaFisica2 PessoaFisica2 { get; set; }
        public virtual String Empresa { get; set; }
        public virtual String CNPJ { get; set; }
        public virtual String Admissao { get; set; }
    }
}
