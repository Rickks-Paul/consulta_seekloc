using System;
using System.IO;
using System.Net;
using System.Web.Http;
using DeathByCaptcha;
using System.Text.RegularExpressions;
using System.Text;

namespace BuscaDadosAPI.Web.Controllers
{
    public class PessoaJuridicaController : ApiController
    {
        private CookieContainer _cookies;
        private readonly string urlBaseReceitaFederal;
        private readonly string paginaValidacao;
        private readonly string paginaPrincipal;
        private readonly string paginaCaptcha;
        private readonly string paginaqsa;

        public PessoaJuridicaController()
        {
            _cookies = new CookieContainer();
            urlBaseReceitaFederal = "http://www.receita.fazenda.gov.br/pessoajuridica/cnpj/cnpjreva/";
            paginaValidacao = "valida.asp";
            paginaPrincipal = "cnpjreva_solicitacao2.asp";
            paginaCaptcha = "captcha/gerarCaptcha.asp";
            paginaqsa = "Cnpjreva_qsa.asp";
        }

        private string ObterDados(string aCNPJ, string aCaptcha, CookieContainer cookies)
        {
            var request = (HttpWebRequest)WebRequest.Create(urlBaseReceitaFederal + paginaValidacao);
            request.ProtocolVersion = HttpVersion.Version10;
            request.CookieContainer = _cookies;
            request.Method = "POST";

            var postData = string.Empty;
            postData += "origem=comprovante&";
            postData += "cnpj=" + new Regex(@"[^\d]").Replace(aCNPJ, string.Empty) + "&";
            postData += "txtTexto_captcha_serpro_gov_br=" + aCaptcha + "&";
            postData += "submit1=Consultar&";
            postData += "search_type=cnpj";

            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;

            var dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            var stHtml = new StreamReader(request.GetResponse().GetResponseStream(), Encoding.GetEncoding("ISO-8859-1"));
            var teste = ObterDadosQSA(aCNPJ, aCaptcha, cookies);
            return stHtml.ReadToEnd();
        }

        private string ObterDadosQSA(string aCNPJ, string aCaptcha, CookieContainer cookies)
        {
            var request = (HttpWebRequest)WebRequest.Create(urlBaseReceitaFederal + paginaqsa);
            request.ProtocolVersion = HttpVersion.Version10;
            request.CookieContainer = cookies;
            request.Method = "GET";

            var postData = string.Empty;

            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;

            var stHtml = new StreamReader(request.GetResponse().GetResponseStream(), Encoding.GetEncoding("ISO-8859-1"));
            return stHtml.ReadToEnd();
        }

        [HttpGet]
        public IHttpActionResult ObterPessoaJuridicaPeloCNPJ(String cnpj)
        {
            //var htmlResult = string.Empty;

            //using (var wc = new CookieAwareWebClient(_cookies))
            //{
            //    wc.Headers[HttpRequestHeader.UserAgent] = "Mozilla/4.0 (compatible; Synapse)";
            //    wc.Headers[HttpRequestHeader.KeepAlive] = "300";
            //    htmlResult = wc.DownloadString(urlBaseReceitaFederal + paginaPrincipal);
            //}

            //if (htmlResult.Length > 0)
            //{
            //    using (var wc2 = new CookieAwareWebClient(_cookies))
            //    {
            //        wc2.Headers[HttpRequestHeader.UserAgent] = "Mozilla/4.0 (compatible; Synapse)";
            //        wc2.Headers[HttpRequestHeader.KeepAlive] = "300";
            //        byte[] data = wc2.DownloadData(urlBaseReceitaFederal + paginaCaptcha);

            //        Client client = (Client)new HttpClient("xx", "x");

            //        Captcha captcha = client.Decode(data, 15);

            //        int index = 0;
            //        int interval = 0;

            //        var base64 = Convert.ToBase64String(data, 0, data.Length);

            //        while (captcha.Uploaded && !captcha.Solved)
            //        {
            //            Client.GetPollInterval(index, out interval, out index);
            //            System.Threading.Thread.Sleep(interval * 1000);
            //            captcha = client.GetCaptcha(captcha.Id);
            //        }

            //        if (captcha.Solved)
            //        {
            //            var msg = string.Empty;
            //            var resp = ObterDados(cnpj, captcha.Text, _cookies);

            //            if (resp.Contains("Verifique se o mesmo foi digitado corretamente"))
            //                msg = "O número do CNPJ não foi digitado corretamente";

            //            if (resp.Contains("Erro na Consulta"))
            //                msg += "Os caracteres não conferem com a imagem";

            //        }


            //    }                                
            //}

            return null;
        }
    }
}