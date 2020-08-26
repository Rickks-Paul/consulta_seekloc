using System;

namespace BD.Domain.Entity
{
    public class EnderecoAtualizado
    {
        public virtual Int32? Id { get; set; }
        public virtual String CPF { get; set; }
        public virtual String Logradouro { get; set; }
        public virtual String Complemento { get; set; }
        public virtual String Numero { get; set; }
        public virtual String CEP { get; set; }
        public virtual String Bairro { get; set; }
        public virtual String Cidade { get; set; }
        public virtual String UF { get; set; }
        public virtual Int16 Origem { get; set; }
        public virtual DateTime? DataCadastro { get; set; }
        public virtual String Nome { get; set; }
        public virtual String NomeMae { get; set; }
        public virtual String DataNascimento { get; set; }
    }
}
