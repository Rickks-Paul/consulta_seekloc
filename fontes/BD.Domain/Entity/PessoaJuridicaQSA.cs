using System;

namespace BD.Domain.Entity
{
    [Serializable]

    public class PessoaJuridicaQSA
    {
        public virtual Int32? Id { get; set; }
        public virtual PessoaJuridica PessoaJuridica { get; set; }
        public virtual String Qualificacao { get; set; }
        public virtual String Nome { get; set; }
    }
}
