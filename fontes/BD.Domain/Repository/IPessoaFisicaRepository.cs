using BD.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BD.Domain.Repository
{
    public interface IPessoaFisicaRepository : IRepository<PessoaFisica>
    {
        PessoaFisica2 ObterUltimaConsultaPessoaFisicaPeloCPF(String cpf);
        PessoaFisica2 ObterNovaPessoaPeloCPF(String cpf);
    }
}
