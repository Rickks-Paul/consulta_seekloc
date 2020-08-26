using BD.Domain.Entity;
using System;

namespace BD.Domain.Repository
{
    public interface IErroLoginRepository : IRepository<ErroLogin>
    {
        Boolean CometeuMuitosErrosLogin(String email);
    }
}
