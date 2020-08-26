using System;

namespace BD.Domain.Entity
{
    public class Atividade
    {
        public virtual Int32? Id { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual DateTime? Data { get; set; }
        public virtual String IP { get; set; }
        public virtual Int32? TipoAtividade { get; set; }
        public virtual String Descricao { get; set; }
        public virtual Int32? Origem { get; set; }
        public virtual String Cidade { get; set; }
        public virtual String Pais { get; set; }
        public virtual String Provedor { get; set; }
        public virtual String ModeloCelular { get; set; }
        public virtual String IMEI { get; set; }
        public virtual String Operadora { get; set; }
    }
}