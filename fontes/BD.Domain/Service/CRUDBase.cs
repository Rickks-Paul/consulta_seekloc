using BD.Domain.Enum;
using BD.Domain.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BD.Domain.Service
{
    public class CRUDBase<T> where T : class
    {
        public List<MensagensVO> Mensagens { get; set; }

        public CRUDBase()
        {
            Mensagens = new List<MensagensVO>();
        }

        public Boolean temErros()
        {
            return Mensagens.Where(x => x.TipoErro == TipoErro.ErroFatal).Count() > 0;
        }
    }
}
