using System;

namespace BD.Domain.Entity
{
    public class ErroSistema
    {
        public virtual Int32? Id { get; set; }
        public virtual String Descricao { get; set; }
        public virtual String Excecao { get; set; }
        public virtual String ExcecaoInterna { get; set; }
        public virtual DateTime Data { get; set; }
    }
}
