using System;

namespace BD.Domain.Entity
{
    public class Usuario
    {
        public virtual Int32? Id { get; set; }
        public virtual String Nome { get; set; }
        public virtual String Email { get; set; }
        public virtual String Senha { get; set; }
        public virtual DateTime? DataCadastro { get; set; }
        public virtual String Especial { get; set; }
        public virtual String Ativo { get; set; }
        public virtual String MotivoEspecial { get; set; }
        public virtual String MotivoCadastro { get; set; }
        public virtual String MotivoInativo { get; set; }
        public virtual String IPCadastro { get; set; }
        public virtual String CPF { get; set; }
        public virtual Int32? IndicadorPor { get; set; }
    }
}