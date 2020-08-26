using System;

namespace BD.Domain.Service
{
    public interface IEmailService
    {
        Boolean EnviarEmail(String destinatario, String emailRemetente, String nomeRemetente, String assunto, String texto, Boolean html);
    }
}
