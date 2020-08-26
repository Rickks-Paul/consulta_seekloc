using System.Collections.Generic;
using System.Xml.Serialization;

namespace BD.Domain.ValueObject
{
    [XmlRoot(ElementName = "xml")]
    public class Xml
    {
        [XmlElement(ElementName = "situacao")]
        public Situacao Situacao { get; set; }
        [XmlElement(ElementName = "pessoa")]
        public Pessoa Pessoa { get; set; }
        [XmlElement(ElementName = "telefones")]
        public Telefones Telefones { get; set; }
        [XmlElement(ElementName = "enderecos")]
        public Enderecos Enderecos { get; set; }
        [XmlElement(ElementName = "veiculos")]
        public Veiculos Veiculos { get; set; }
        [XmlElement(ElementName = "empregos")]
        public Empregos Empregos { get; set; }
        [XmlElement(ElementName = "emails")]
        public Emails Emails { get; set; }
        [XmlElement(ElementName = "vizinhos")]
        public Vizinhos Vizinhos { get; set; }
        [XmlElement(ElementName = "parentes")]
        public Parentes Parentes { get; set; }

        [XmlElement(ElementName = "irmaos")]
        public Irmaos Irmaos { get; set; }
    }

    [XmlRoot(ElementName = "situacao")]
    public class Situacao
    {
        [XmlElement(ElementName = "codigo")]
        public string Codigo { get; set; }
        [XmlElement(ElementName = "mensagem")]
        public string Mensagem { get; set; }
    }

    [XmlRoot(ElementName = "pessoa")]
    public class Pessoa
    {
        [XmlElement(ElementName = "cpf")]
        public string Cpf { get; set; }
        [XmlElement(ElementName = "nome")]
        public string Nome { get; set; }
        [XmlElement(ElementName = "dtnasc")]
        public string Dtnasc { get; set; }
        [XmlElement(ElementName = "mae")]
        public string Mae { get; set; }
        [XmlElement(ElementName = "sexo")]
        public string Sexo { get; set; }
    }

    [XmlRoot(ElementName = "telefone")]
    public class Telefone
    {
        [XmlElement(ElementName = "ddd")]
        public string Ddd { get; set; }
        [XmlElement(ElementName = "fone")]
        public string Fone { get; set; }
    }

    [XmlRoot(ElementName = "telefones")]
    public class Telefones
    {
        [XmlElement(ElementName = "telefone")]
        public List<Telefone> Telefone { get; set; }
    }

    [XmlRoot(ElementName = "endereco")]
    public class Endereco
    {
        [XmlElement(ElementName = "tipo")]
        public string Tipo { get; set; }
        [XmlElement(ElementName = "logradouro")]
        public string Logradouro { get; set; }
        [XmlElement(ElementName = "complemento")]
        public string Complemento { get; set; }
        [XmlElement(ElementName = "numero")]
        public string Numero { get; set; }
        [XmlElement(ElementName = "cep")]
        public string Cep { get; set; }
        [XmlElement(ElementName = "bairro")]
        public string Bairro { get; set; }
        [XmlElement(ElementName = "cidade")]
        public string Cidade { get; set; }
        [XmlElement(ElementName = "uf")]
        public string Uf { get; set; }
    }

    [XmlRoot(ElementName = "enderecos")]
    public class Enderecos
    {
        [XmlElement(ElementName = "endereco")]
        public List<Endereco> Endereco { get; set; }
    }

    [XmlRoot(ElementName = "veiculo")]
    public class Veiculo
    {
        [XmlElement(ElementName = "placa")]
        public string Placa { get; set; }
        [XmlElement(ElementName = "renavan")]
        public string Renavan { get; set; }
        [XmlElement(ElementName = "classi")]
        public string Classi { get; set; }
        [XmlElement(ElementName = "anofab")]
        public string Anofab { get; set; }
        [XmlElement(ElementName = "anomod")]
        public string Anomod { get; set; }
        [XmlElement(ElementName = "modelo")]
        public string Modelo { get; set; }
        [XmlElement(ElementName = "tipo")]
        public string Tipo { get; set; }
        [XmlElement(ElementName = "especie")]
        public string Especie { get; set; }
        [XmlElement(ElementName = "combustivel")]
        public string Combustivel { get; set; }
    }

    [XmlRoot(ElementName = "veiculos")]
    public class Veiculos
    {
        [XmlElement(ElementName = "veiculo")]
        public List<Veiculo> Veiculo { get; set; }
    }

    [XmlRoot(ElementName = "irmaos")]
    public class Irmaos
    {
        [XmlElement(ElementName = "irmao")]
        public List<Irmao> Irmao { get; set; }
    }

    [XmlRoot(ElementName = "irmao")]
    public class Irmao
    {
        [XmlElement(ElementName = "nome")]
        public string Nome { get; set; }
        [XmlElement(ElementName = "cpf")]
        public string CPF { get; set; }
    }

    [XmlRoot(ElementName = "emprego")]
    public class Emprego
    {
        [XmlElement(ElementName = "empresa")]
        public string Empresa { get; set; }
        [XmlElement(ElementName = "cnpj")]
        public string Cnpj { get; set; }
        [XmlElement(ElementName = "admissao")]
        public string Admissao { get; set; }
    }

    [XmlRoot(ElementName = "empregos")]
    public class Empregos
    {
        [XmlElement(ElementName = "emprego")]
        public List<Emprego> Emprego { get; set; }
    }

    [XmlRoot(ElementName = "emails")]
    public class Emails
    {
        [XmlElement(ElementName = "emprego")]
        public List<Emprego> Emprego { get; set; }
    }

    [XmlRoot(ElementName = "vizinho")]
    public class Vizinho
    {
        [XmlElement(ElementName = "nome")]
        public string Nome { get; set; }
        [XmlElement(ElementName = "cpf")]
        public string Cpf { get; set; }
        [XmlElement(ElementName = "tipo")]
        public string Tipo { get; set; }
        [XmlElement(ElementName = "logradouro")]
        public string Logradouro { get; set; }
        [XmlElement(ElementName = "numero")]
        public string Numero { get; set; }
        [XmlElement(ElementName = "cep")]
        public string Cep { get; set; }
        [XmlElement(ElementName = "bairro")]
        public string Bairro { get; set; }
        [XmlElement(ElementName = "cidade")]
        public string Cidade { get; set; }
        [XmlElement(ElementName = "uf")]
        public string Uf { get; set; }
    }

    [XmlRoot(ElementName = "vizinhos")]
    public class Vizinhos
    {
        [XmlElement(ElementName = "vizinho")]
        public List<Vizinho> Vizinho { get; set; }
    }

    [XmlRoot(ElementName = "parente")]
    public class Parente
    {
        [XmlElement(ElementName = "nome")]
        public string Nome { get; set; }
        [XmlElement(ElementName = "cpf")]
        public string Cpf { get; set; }
    }

    [XmlRoot(ElementName = "parentes")]
    public class Parentes
    {
        [XmlElement(ElementName = "parente")]
        public List<Parente> Parente { get; set; }
    }
}
