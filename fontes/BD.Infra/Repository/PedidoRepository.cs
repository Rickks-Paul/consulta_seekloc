using BD.Domain.Entity;
using BD.Domain.Repository;
using BD.Domain.ValueObject;
using System;
using NHibernate.Linq;
using System.Collections.Generic;
using System.Linq;
using BD.DI;
using BD.Domain.Data;

namespace BD.Infra.Repository
{
    public class PedidoRepository : NHibernateRepository<Pedido>, IPedidoRepository
    {
        public PedidoRepository(IUnitOfWork unitOfWork, IDependencyInjectionContainer container) : base(unitOfWork, container)
        {
        }

        public List<PedidoVO> ObterResumoValoresPedidosPorMesAno(string email)
        {
            var q = from p in Session.Query<Pedido>().Where(w => w.Usuario.Email.ToUpper() == email.ToUpper())
                    orderby p.Id descending
                    select new PedidoVO { NumeroPedido = p.Id.ToString(), DataPedido = String.Format("{0:dd/MM/yyyy HH:mm:ss}", p.Data), FormaPagamento = p.FormaPagamento.ToString(), CodigoAutorizacao = p.CodigoAutorizacao, CodigoServico = p.Servico.Codigo, DescricaoCurtaServico = p.Servico.DescricaoCurta, StatusPedido = p.Status.ToString(), MotivoStatus = p.Motivo, ValorServico = String.Format("{0:0.00}", p.Servico.Valor), EmiteNF = p.EmiteNF, CPFNF = p.CPFNF };
            return q.ToList();
        }

        public List<PedidoVO> ObterTodosPedidosPorEmail(string email)
        {
            var q = from p in Session.Query<Pedido>().Where(w => w.Usuario.Email.ToUpper() == email.ToUpper())
                    orderby p.Id descending
                    select new PedidoVO { NumeroPedido = p.Id.ToString(), DataPedido = String.Format("{0:dd/MM/yyyy HH:mm:ss}", p.Data), FormaPagamento = p.FormaPagamento.ToString(), CodigoAutorizacao = p.CodigoAutorizacao, CodigoServico = p.Servico.Codigo, DescricaoCurtaServico = p.Servico.DescricaoCurta, StatusPedido = p.Status.ToString(), MotivoStatus = p.Motivo, ValorServico = String.Format("{0:0.00}", p.Servico.Valor), EmiteNF = p.EmiteNF, CPFNF = p.CPFNF };
            return q.ToList();
        }
    }
}
