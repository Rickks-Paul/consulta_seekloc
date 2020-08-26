using System;
using System.Collections.Generic;

namespace BD.Domain.Entity
{
    [Serializable]
    public class PessoaJuridica
    {
        public virtual Int32? Id { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual Aplicativo Aplicativo { get; set; }
        public virtual Telefone Telefone { get; set; }
        public virtual String Nome { get; set; }
        public virtual String CNPJ { get; set; }
        public virtual String Logradouro { get; set; }
        public virtual String Complemento { get; set; }
        public virtual String Situacao { get; set; }
        public virtual String Numero { get; set; }
        public virtual String Bairro { get; set; }
        public virtual String Municipio { get; set; }
        public virtual String CEP { get; set; }
        public virtual String UF { get; set; }
        public virtual String NumeroTelefone { get; set; }
        public virtual String DataAbertura { get; set; }
        public virtual String NaturezaJuridica { get; set; }
        public virtual String Status { get; set; }
        public virtual String Tipo { get; set; }
        public virtual String NomeFantasia { get; set; }
        public virtual String SituacaoEspecial { get; set; }
        public virtual String MotivoSituacaoEspecial { get; set; }
        public virtual String DataSituacaoEspecial { get; set; }
        public virtual String CapitalSocial { get; set; }
        public virtual String UltimaAtualizacao { get; set; }
        public virtual String Email { get; set; }
        public virtual String DataCadastro { get; set; }
        public virtual String Importado { get; set; }
        public virtual String Hash { get; set; }

        public virtual List<PessoaJuridicaAtividade> Atividades { get; set; }
        public virtual List<PessoaJuridicaQSA> QSAs { get; set; }

    }
}
