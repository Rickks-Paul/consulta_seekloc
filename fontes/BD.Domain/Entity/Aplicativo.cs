using System;

namespace BD.Domain.Entity
{
    public class Aplicativo
    {
        public virtual Int32? Id { get; set; }
        public virtual String Nome { get; set; }
        public virtual String Versao { get; set; }
        public virtual DateTime DataCadastro { get; set; }
    }
}