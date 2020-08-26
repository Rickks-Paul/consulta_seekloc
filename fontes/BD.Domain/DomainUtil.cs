using BD.Domain.Entity;
using BD.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace BuscaDadosAPI.Domain
{
    public static class DomainUtil
    {
        public static Boolean EmailEhValido(String email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static String TratarDataAPI(String data)
        {
            String dataFormatada = String.Empty;
            if (!String.IsNullOrEmpty(data))
            {
                var ano = data.Substring(0, 4);
                var mes = data.Substring(4, 2);
                var dia = data.Substring(6, 2);
                dataFormatada = dia + "/" + mes + "/" + ano;
            }

            return dataFormatada;
        }

        public static Boolean CPFEhValido(String cpf)
        {
            int[] mt1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] mt2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string TempCPF;
            string Digito;
            int soma;
            int resto;

            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");

            if (cpf == "11111111111"
             || cpf == "22222222222"
             || cpf == "33333333333"
             || cpf == "44444444444"
             || cpf == "55555555555"
             || cpf == "66666666666"
             || cpf == "77777777777"
             || cpf == "88888888888"
             || cpf == "99999999999")
                return false;

            if (cpf.Length != 11)
                return false;

            TempCPF = cpf.Substring(0, 9);
            soma = 0;
            for (int i = 0; i < 9; i++)
                soma += int.Parse(TempCPF[i].ToString()) * mt1[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            Digito = resto.ToString();
            TempCPF = TempCPF + Digito;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(TempCPF[i].ToString()) * mt2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            Digito = Digito + resto.ToString();

            return cpf.EndsWith(Digito);
        }


        public static String GerarHashMD5(String valor)
        {
            // step 1, calculate MD5 hash from input

            MD5 md5 = MD5.Create();

            byte[] inputBytes = Encoding.ASCII.GetBytes(valor);

            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)

            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString();
        }

        public static void AtualizarTextoVeiculos(IList<Veiculo> veiculos)
        {
            if (veiculos != null)
            {
                foreach (var vei in veiculos)
                {
                    vei.CombustivelTexto = ObterCombustivelTexto(vei.Combustivel);
                    vei.EspecieTexto = ObterEspecieVeiculoTexto(vei.Especie);
                    vei.TipoTexto = ObterCombustivelTexto(vei.Tipo);
                }
            }
        }

        public static String ObterEspecieVeiculoTexto(String EspecieVeiculo)
        {
            String texto = String.Empty;

            if (String.IsNullOrEmpty(EspecieVeiculo))
            {
                return texto;
            }

            switch (EspecieVeiculo)
            {
                case "2":
                    texto = "Carga";
                    break;

                case "4":
                    texto = "Corrida";
                    break;

                case "6":
                    texto = "Especial";
                    break;

                case "3":
                    texto = "Misto";
                    break;

                case "1":
                    texto = "Passageiro";
                    break;

                case "5":
                    texto = "Tração";
                    break;

                default:
                    break;
            }

            return texto;
        }

        public static String ObterCombustivelTexto(String Combustivel)
        {
            String texto = String.Empty;

            if (String.IsNullOrEmpty(Combustivel))
            {
                return texto;
            }

            switch (Combustivel)
            {
                case "1":
                    texto = "Alcool";
                    break;

                case "9":
                    texto = "Alcool/GNC";
                    break;

                case "3":
                    texto = "Diesel";
                    break;

                case "10":
                    texto = "Diesel/GNC";
                    break;

                case "7":
                    texto = "Elfont EX";
                    break;

                case "6":
                    texto = "Elfont IN";
                    break;

                case "5":
                    texto = "Gás metano";
                    break;

                case "4":
                    texto = "Gasogênio";
                    break;

                case "8":
                    texto = "Gasolina/GNC";
                    break;

                case "2":
                    texto = "Gasolina";
                    break;

                case "12":
                    texto = "Álcool/Gasolina";
                    break;

                case "13":
                    texto = "Álcool/Gasolina/GNV";
                    break;


                default:
                    break;
            }

            return texto;
        }

        public static String ObterTipoVeiculoTexto(String Tipo)
        {
            String texto = String.Empty;

            if (String.IsNullOrEmpty(Tipo))
            {
                return texto;
            }

            if (!String.IsNullOrEmpty(Tipo))
            {
                switch (Tipo)
                {
                    case "6":
                        texto = "Automóvel";
                        break;

                    case "14":
                        texto = "Caminhão";
                        break;

                    case "13":
                        texto = "Caminhoneta";
                        break;

                    case "24":
                        texto = "Carga/CAM";
                        break;

                    case "2":
                        texto = "Ciclomoto";
                        break;

                    case "22":
                        texto = "ESP/ônibus";
                        break;

                    case "7":
                        texto = "Microônibus";
                        break;

                    case "23":
                        texto = "Misto/CAM";
                        break;

                    case "4":
                        texto = "Motociclo";
                        break;

                    case "3":
                        texto = "Motoneta";
                        break;

                    case "8":
                        texto = "Ônibus";
                        break;

                    case "10":
                        texto = "Reboque";
                        break;

                    case "5":
                        texto = "Triciclo";
                        break;

                    case "17":
                        texto = "C. trator";
                        break;

                    default:
                        break;
                }
            }

            return texto;
        }


        public static string stringEntreString(string str, string strInicio, string strFinal)
        {
            var ini = str.IndexOf(strInicio);
            var fim = str.IndexOf(strFinal);

            if (ini == -1)
            {
                ini = str.IndexOf(strInicio.ToUpper());
            }

            if (fim == -1)
            {
                fim = str.IndexOf(strFinal.ToUpper());
            }

            if (ini > 0)
                ini = ini + strInicio.Length;

            if (fim > 0)
                fim = fim + strFinal.Length;

            var diff = ((fim - ini) - strFinal.Length);
            if ((fim > ini) && (diff > 0) && (ini != -1))
                return str.Substring(ini, diff);
            else
                return string.Empty;
        }

        public static string stringSaltaString(string str, string strInicio)
        {
            var ini = str.IndexOf(strInicio);

            if (ini > 0)
            {
                ini = ini + strInicio.Length;
                return str.Substring(ini);
            }
            else
                return str;
        }

        public static string stringPrimeiraLetraMaiuscula(string str)
        {
            return
                str.Length > 0 ?
                    str.Substring(0, 1).ToUpper() + str.Substring(1, str.Length - 1).ToLower() :
                    string.Empty;

        }

        public static string recuperaColunaValor(string pattern, Coluna col)
        {
            var s = pattern.Replace("\n", "").Replace("\t", "").Replace("\r", "");

            switch (col)
            {
                case Coluna.RazaoSocial:
                    {
                        s = stringEntreString(s, "<!-- Início Linha NOME EMPRESARIAL -->", "<!-- Fim Linha NOME EMPRESARIAL -->");
                        s = stringEntreString(s, "<tr>", "</tr>");
                        s = stringEntreString(s, "<b>", "</b>");
                        return s.Trim();
                    }
                case Coluna.NomeFantasia:
                    {
                        s = stringEntreString(s, "<!-- Início Linha ESTABELECIMENTO -->", "<!-- Fim Linha ESTABELECIMENTO -->");
                        s = stringEntreString(s, "<tr>", "</tr>");
                        s = stringEntreString(s, "<b>", "</b>");
                        return s.Trim();
                    }
                case Coluna.NaturezaJuridica:
                    {
                        s = stringEntreString(s, "<!-- Início Linha NATUREZA JURÍDICA -->", "<!-- Fim Linha NATUREZA JURÍDICA -->");
                        s = stringEntreString(s, "<tr>", "</tr>");
                        s = stringEntreString(s, "<b>", "</b>");
                        return s.Trim();
                    }
                case Coluna.AtividadeEconomicaPrimaria:
                    {
                        s = stringEntreString(s, "<!-- Início Linha ATIVIDADE ECONOMICA -->", "<!-- Fim Linha ATIVIDADE ECONOMICA -->");
                        s = stringEntreString(s, "<tr>", "</tr>");
                        s = stringEntreString(s, "<b>", "</b>");
                        return s.Trim();
                    }
                case Coluna.AtividadeEconomicaSecundaria:
                    {
                        s = stringEntreString(s, "<!-- Início Linha ATIVIDADE ECONOMICA SECUNDARIA-->", "<!-- Fim Linha ATIVIDADE ECONOMICA SECUNDARIA -->");
                        s = stringEntreString(s, "<tr>", "</tr>");
                        s = stringEntreString(s, "<b>", "</b>");
                        return s.Trim();
                    }
                case Coluna.NumeroDaInscricao:
                    {
                        s = stringEntreString(s, "<!-- Início Linha NÚMERO DE INSCRIÇÃO -->", "<!-- Fim Linha NÚMERO DE INSCRIÇÃO -->");
                        s = stringEntreString(s, "<tr>", "</tr>");
                        s = stringEntreString(s, "<b>", "</b>");
                        return s.Trim();
                    }
                case Coluna.MatrizFilial:
                    {
                        s = stringEntreString(s, "<!-- Início Linha NÚMERO DE INSCRIÇÃO -->", "<!-- Fim Linha NÚMERO DE INSCRIÇÃO -->");
                        s = stringEntreString(s, "<tr>", "</tr>");
                        s = stringSaltaString(s, "</b>");
                        s = stringEntreString(s, "<b>", "</b>");
                        return s.Trim();
                    }
                case Coluna.EnderecoLogradouro:
                    {
                        s = stringEntreString(s, "<!-- Início Linha LOGRADOURO -->", "<!-- Fim Linha LOGRADOURO -->");
                        s = stringEntreString(s, "<tr>", "</tr>");
                        s = stringEntreString(s, "<b>", "</b>");
                        return s.Trim();
                    }
                case Coluna.EnderecoNumero:
                    {
                        s = stringEntreString(s, "<!-- Início Linha LOGRADOURO -->", "<!-- Fim Linha LOGRADOURO -->");
                        s = stringEntreString(s, "<tr>", "</tr>");
                        s = stringSaltaString(s, "</b>");
                        s = stringEntreString(s, "<b>", "</b>");
                        return s.Trim();
                    }
                case Coluna.EnderecoComplemento:
                    {
                        s = stringEntreString(s, "<!-- Início Linha LOGRADOURO -->", "<!-- Fim Linha LOGRADOURO -->");
                        s = stringEntreString(s, "<tr>", "</tr>");
                        s = stringSaltaString(s, "</b>");
                        s = stringSaltaString(s, "</b>");
                        s = stringEntreString(s, "<b>", "</b>");
                        return s.Trim();
                    }
                case Coluna.EnderecoCEP:
                    {
                        s = stringEntreString(s, "<!-- Início Linha CEP -->", "<!-- Fim Linha CEP -->");
                        s = stringEntreString(s, "<tr>", "</tr>");
                        s = stringEntreString(s, "<b>", "</b>");
                        return s.Trim();
                    }
                case Coluna.EnderecoBairro:
                    {
                        s = stringEntreString(s, "<!-- Início Linha CEP -->", "<!-- Fim Linha CEP -->");
                        s = stringEntreString(s, "<tr>", "</tr>");
                        s = stringSaltaString(s, "</b>");
                        s = stringEntreString(s, "<b>", "</b>");
                        return s.Trim();
                    }
                case Coluna.EnderecoCidade:
                    {
                        s = stringEntreString(s, "<!-- Início Linha CEP -->", "<!-- Fim Linha CEP -->");
                        s = stringEntreString(s, "<tr>", "</tr>");
                        s = stringSaltaString(s, "</b>");
                        s = stringSaltaString(s, "</b>");
                        s = stringEntreString(s, "<b>", "</b>");
                        return s.Trim();
                    }
                case Coluna.EnderecoEstado:
                    {
                        s = stringEntreString(s, "<!-- Início Linha CEP -->", "<!-- Fim Linha CEP -->");
                        s = stringEntreString(s, "<tr>", "</tr>");
                        s = stringSaltaString(s, "</b>");
                        s = stringSaltaString(s, "</b>");
                        s = stringSaltaString(s, "</b>");
                        s = stringEntreString(s, "<b>", "</b>");
                        return s.Trim();
                    }
                case Coluna.SituacaoCadastral:
                    {
                        s = stringEntreString(s, "<!-- Início Linha SITUAÇÃO CADASTRAL -->", "<!-- Fim Linha SITUACAO CADASTRAL -->");
                        s = stringEntreString(s, "<tr>", "</tr>");
                        s = stringEntreString(s, "<b>", "</b>");
                        return s.Trim();
                    }
                case Coluna.DataSituacaoCadastral:
                    {
                        s = stringEntreString(s, "<!-- Início Linha SITUAÇÃO CADASTRAL -->", "<!-- Fim Linha SITUACAO CADASTRAL -->");
                        s = stringEntreString(s, "<tr>", "</tr>");
                        s = stringSaltaString(s, "</b>");
                        s = stringEntreString(s, "<b>", "</b>");
                        return s.Trim();
                    }
                case Coluna.MotivoSituacaoCadastral:
                    {
                        s = stringEntreString(s, "<!-- Início Linha MOTIVO DE SITUAÇÃO CADASTRAL -->", "<!-- Fim Linha MOTIVO DE SITUAÇÃO CADASTRAL -->");
                        s = stringEntreString(s, "<tr>", "</tr>");
                        s = stringEntreString(s, "<b>", "</b>");
                        return s.Trim();
                    }

                default:
                    {
                        return s;
                    }
            }
        }
    }
}
