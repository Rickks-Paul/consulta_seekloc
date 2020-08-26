using System;

namespace BD.Domain.Entity
{
    public class PessoaFisica
    {
        public virtual Int64 Id { get; set; }
        public virtual Usuario UsuarioConsulta { get; set; }
        public virtual Aplicativo Aplicativo { get; set; }
        public virtual String Nome { get; set; }
        public virtual String CPF { get; set; }
        public virtual String DataNascimento { get; set; }
        public virtual String NomeMae { get; set; }
        public virtual String Logradouro { get; set; }
        public virtual String NumeroRua { get; set; }
        public virtual String ComplementoEndereco { get; set; }
        public virtual String Bairro { get; set; }
        public virtual String CEP { get; set; }
        public virtual String Municipio { get; set; }
        public virtual String UF { get; set; }
        public virtual Telefone Telefone { get; set; }
        public virtual DateTime DataCadastro { get; set; }
        public virtual String Mensagem { get; set; }
        public virtual String Importado { get; set; }
        public virtual String Hash { get; set; }
    }
}