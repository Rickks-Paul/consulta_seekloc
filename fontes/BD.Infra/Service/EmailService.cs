using BD.DI;
using BD.Domain.Service;
using System;
using System.Net;
using System.Net.Mail;

namespace BD.Infra.Service
{
    public class EmailService : IEmailService
    {
        private static IDependencyInjectionContainer container;
        public EmailService(IDependencyInjectionContainer _container)
        {
            container = _container;
        }

        public Boolean EnviarEmail(string destinatario, string emailRemetente, string nomeRemetente, string assunto, string texto, bool html)
        {
            try
            {
                String APIKey = "XXXXXXXXX";
                String SecretKey = "XXXXXXXXXX";
                SmtpClient client = new SmtpClient("in-v3.mailjet.com", 587);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(APIKey, SecretKey);

                MailMessage EmailParaEnviar = new MailMessage(emailRemetente, destinatario);
                EmailParaEnviar.To.Add(destinatario);

                EmailParaEnviar.Subject = assunto;
                EmailParaEnviar.Body = texto;
                EmailParaEnviar.IsBodyHtml = html;

                client.Send(EmailParaEnviar);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
