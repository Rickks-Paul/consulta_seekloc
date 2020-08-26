using BD.Domain.Entity;
using BD.Domain.Repository;
using BD.Domain.ValueObject;
using System;
using System.Collections.Generic;
using NHibernate.Linq;
using System.Linq;
using BD.DI;
using BD.Domain.Data;

namespace BD.Infra.Repository
{
    public class ServicoRepository : NHibernateRepository<Servico>, IServicoRepository
    {
        public ServicoRepository(IUnitOfWork unitOfWork, IDependencyInjectionContainer container) : base(unitOfWork, container)
        {
        }

        public Servico ObterPeloCodigo(string codigo)
        {
            return Session.Query<Servico>().Where(w => w.Codigo.ToUpper() == codigo.ToUpper()).FirstOrDefault();
        }

        public List<ServicoVO> ObterTodosAtivos()
        {
            var q = from p in Session.Query<Servico>().Where(w => w.Ativo == "S")
                    orderby p.Valor, p.DescricaoCurta
                    select new ServicoVO { Codigo = p.Codigo, Descricao = p.DescricaoLonga, DescricaoCurta = p.DescricaoCurta, Valor = String.Format("{0:0.00}", p.Valor) };
            return q.ToList();
        }
    }
}
