using System;

namespace BD.Domain.Entity
{
    [Serializable]

    public class PessoaJuridicaAtividade
    {
        public virtual Int32? Id { get; set; }
        public virtual PessoaJuridica PessoaJuridica { get; set; }
        public virtual String Codigo { get; set; }
        public virtual String Texto { get; set; }
        public virtual String Principal { get; set; }
    }
}
