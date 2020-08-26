using BD.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BD.Domain.Repository
{
    public interface IAtividadeRepository  :IRepository<Atividade>
    {
        String ObterUltimoLogonPeloEmail(String email);
    }
}
