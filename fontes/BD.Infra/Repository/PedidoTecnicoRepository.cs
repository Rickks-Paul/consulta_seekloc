using BD.DI;
using BD.Domain.Data;
using BD.Domain.Entity;
using BD.Domain.Repository;
using BD.Domain.ValueObject;
using System;
using System.Collections.Generic;

namespace BD.Infra.Repository
{
    public class PedidoTecnicoRepository : NHibernateRepository<Pedido>, IPedidoTecnicoRepository
    {
        public PedidoTecnicoRepository(IUnitOfWork unitOfWork, IDependencyInjectionContainer container) : base(unitOfWork, container)
        {
        }

        public void Delete(PedidoTecnico entity)
        {
            throw new NotImplementedException();
        }

        public void Evict(PedidoTecnico entity)
        {
            throw new NotImplementedException();
        }

        public List<PedidoTecnico> ObterPedidosTecnicosPeloCPF(string cpf)
        {
            throw new NotImplementedException();
        }

        public List<PedidoTecnicoVO> ObterPedidosTecnicosUsuario(int? IdUsuario)
        {
            throw new NotImplementedException();
        }

        public List<PedidoTecnico> ObterPedidosTecnicosUsuarioPeloCPF(int? IdUsuario, string cpf)
        {
            throw new NotImplementedException();
        }

        public PedidoTecnico Save(PedidoTecnico entity)
        {
            throw new NotImplementedException();
        }

        public PedidoTecnico Update(PedidoTecnico entity)
        {
            throw new NotImplementedException();
        }

        IList<PedidoTecnico> IRepository<PedidoTecnico>.GetAll()
        {
            throw new NotImplementedException();
        }

        PedidoTecnico IRepository<PedidoTecnico>.GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
