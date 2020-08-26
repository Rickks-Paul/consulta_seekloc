using BD.DI;
using BD.Domain.Data;
using BD.Domain.Entity;
using BD.Domain.Repository;
using BD.Domain.ValueObject;
using BuscaDadosAPI.Domain;
using NHibernate.Linq;
using System;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace BD.Infra.Repository
{
    public class PessoaFisicaRepository : NHibernateRepository<PessoaFisica>, IPessoaFisicaRepository
    {
        private readonly IDependencyInjectionContainer _container;

        public PessoaFisicaRepository(IUnitOfWork unitOfWork, IDependencyInjectionContainer container) : base(unitOfWork, container)
        {
            _container = container;
        }

        public PessoaFisica2 ObterNovaPessoaPeloCPF(string cpf)
        {
            var pessoa = new PessoaFisica2();


            return pessoa;
        }

        public PessoaFisica2 ObterUltimaConsultaPessoaFisicaPeloCPF(string cpf)
        {
            return Session.Query<PessoaFisica2>().Where(w => w.CPF == cpf).OrderByDescending(d => d.Id).FirstOrDefault();
        }
    }
}
