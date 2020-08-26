using System;

namespace BD.Domain.ValueObject
{
    public class RequisicaoConsultaVO
    {
        public String cpf { get; set; }
        public String email { get; set; }
        public String senha { get; set; }
        public String imei { get; set; }
        public String versao { get; set; }
        public String hash { get; set; }
        public Int16? aplicativo { get; set; }
        public String ip { get; set; }
        public String modelo { get; set; }
        public String fabricante { get; set; }
        public String numero { get; set; }
        public String token { get; set; }
        public String operadora { get; set; }
    }
}
