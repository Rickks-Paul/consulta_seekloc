using System;

namespace BD.Domain.Entity
{
    public class Veiculo
    {
        public virtual Int32? Id { get; set; }
        public virtual PessoaFisica2 PessoaFisica2 { get; set; }
        public virtual String Placa { get; set; }
        public virtual String Renavan { get; set; }
        public virtual String Chassi { get; set; }
        public virtual String Anofab { get; set; }
        public virtual String Anomod { get; set; }
        public virtual String Modelo { get; set; }
        public virtual String Tipo { get; set; }
        public virtual String TipoTexto { get; set; }
        public virtual String Especie { get; set; }
        public virtual String EspecieTexto { get; set; }
        public virtual String Combustivel { get; set; }
        public virtual String CombustivelTexto { get; set; }
    }
}
