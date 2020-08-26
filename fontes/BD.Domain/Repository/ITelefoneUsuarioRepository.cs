using BD.Domain.Entity;
using System;

namespace BD.Domain.Repository
{
    public interface ITelefoneUsuarioRepository
    {
        TelefoneUsuario ObterTelefonePorUsuarioEIMEI(Int32? IdUsuario, String IMEI);
    }
}
