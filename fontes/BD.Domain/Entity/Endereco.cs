using System;

namespace BD.Domain.Entity
{
    public class Endereco
    {
        public virtual Int32? Id { get; set; }
        public virtual PessoaFisica2 PessoaFisica2 { get; set; }
        public virtual String Tipo { get; set; }
        public virtual String Logradouro { get; set; }
        public virtual String Complemento { get; set; }
        public virtual String Numero { get; set; }
        public virtual String Cep { get; set; }
        public virtual String Bairro { get; set; }
        public virtual String Cidade { get; set; }
        public virtual String UF { get; set; }
    }
}
