using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BD.Domain.ValueObject
{
    [Serializable]
    public class DadosCNPJVO
    {
        public String data_situacao { get; set; }
        public String complemento { get; set; }
        public String nome { get; set; }
        public String uf { get; set; }
        public String telefone { get; set; }
        public String email { get; set; }
        public String situacao { get; set; }
        public String bairro { get; set; }
        public String logradouro { get; set; }
        public String numero { get; set; }
        public String cep { get; set; }
        public String municipio { get; set; }
        public String abertura { get; set; }
        public String natureza_juridica { get; set; }
        public String cnpj { get; set; }
        public String ultima_atualizacao { get; set; }
        public String status { get; set; }
        public String tipo { get; set; }
        public String fantasia { get; set; }
        public String efr { get; set; }
        public String motivo_situacao { get; set; }
        public String situacao_especial { get; set; }
        public String data_situacao_especial { get; set; }
        public String capital_social { get; set; }
        // public String extra { get; set; }
        public String mensagem { get; set; }

        public List<AtividadesSecundarias> atividades_secundarias { get; set; }
        public List<QuadroSocietario> qsa { get; set; }
    }
}
