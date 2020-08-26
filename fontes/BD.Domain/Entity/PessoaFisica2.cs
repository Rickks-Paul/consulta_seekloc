using System;
using System.Collections.Generic;

namespace BD.Domain.Entity
{
    /// <summary>
    /// Pessoa física no modelo dos dados retornados pelo seekloc
    /// </summary>
    public class PessoaFisica2 
    {
        public PessoaFisica2()
        {
            Telefones = new List<Telefone2>();
            Enderecos = new List<Endereco>();
            Veiculos = new List<Veiculo>();
            Empregos = new List<Emprego>();
            Emails = new List<Email>();
            Vizinhos = new List<Vizinho>();
            Parentes = new List<Parente>();
        }

        public virtual Aplicativo Aplicativo { get; set; }
        public virtual string Bairro { get; set; }
        public virtual string CEP { get; set; }
        public virtual string ComplementoEndereco { get; set; }
        public virtual string CPF { get; set; }
        public virtual DateTime DataCadastro { get; set; }
        public virtual string DataNascimento { get; set; }
        public virtual string Hash { get; set; }
        public virtual Int32? Id { get; set; }
        public virtual string Importado { get; set; }
        public virtual string Logradouro { get; set; }
        public virtual string Mensagem { get; set; }
        public virtual string Municipio { get; set; }
        public virtual string Nome { get; set; }
        public virtual string NomeMae { get; set; }
        public virtual string NumeroRua { get; set; }
        public virtual Telefone Telefone { get; set; }
        public virtual string UF { get; set; }
        public virtual Usuario UsuarioConsulta { get; set; }
        public virtual String APISeekLoc { get; set; }

        public virtual String Sexo { get; set; }
        public virtual String Vitalicio { get; set; }
        public virtual IList<Telefone2> Telefones { get; set; }
        public virtual IList<Endereco> Enderecos { get; set; }
        public virtual IList<Veiculo> Veiculos { get; set; }
        public virtual IList<Emprego> Empregos { get; set; }
        public virtual IList<Email> Emails { get; set; }
        public virtual IList<Vizinho> Vizinhos { get; set; }
        public virtual IList<Parente> Parentes { get; set; }
    }
}
