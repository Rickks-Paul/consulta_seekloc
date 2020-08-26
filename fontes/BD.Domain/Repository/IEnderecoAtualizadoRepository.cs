using BD.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BD.Domain.Repository
{
    public interface IEnderecoAtualizadoRepository : IRepository<EnderecoAtualizado>
    {
        EnderecoAtualizado ObterEndereco(String cpf);
    }
}
