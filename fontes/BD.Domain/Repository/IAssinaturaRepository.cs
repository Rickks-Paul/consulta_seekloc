using BD.Domain.Entity;
using BD.Domain.ValueObject;
using System;
using System.Collections.Generic;

namespace BD.Domain.Repository
{
    public interface  IAssinaturaRepository : IRepository<Assinatura>
    {
        String ObterDataFimAssinaturaMaisRecentePeloEmailDoUsuario(String email, Boolean somenteData = false);
        Assinatura ObterAssinaturaMaisRecentePeloEmailDoUsuario(String email);
        List<Assinatura> ObterTodasAssinaturasPorEmail(String email);
        List<AssinaturaVO> ObterTodasAssinaturasVOPorEmail(String email);
    }
}
