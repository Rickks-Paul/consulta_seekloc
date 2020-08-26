using System;

namespace BD.Domain.Entity
{
    public class ErroConsulta
    {
        public virtual Int32? Id { get; set; }
        public virtual String Email { get; set; }
        public virtual String Cpfcnpj { get; set; }
        public virtual String Ip { get; set; }
        public virtual Int32? Aplicativo { get; set; }
        public virtual String Versao { get; set; }
        public virtual String Numero { get; set; }
        public virtual String IMEI { get; set; }
        public virtual DateTime? Data { get; set; }
        public virtual String Mensagem { get; set; }
    }
}