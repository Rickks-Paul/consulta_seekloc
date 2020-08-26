using BD.DI;
using BD.Domain.Data;
using BD.Domain.Entity;
using BD.Domain.Repository;
using BD.Domain.ValueObject;
using System;

namespace BD.Infra.Repository
{
    public class LimiteUsuarioRepository : NHibernateRepository<LimiteUsuario>, ILimiteUsuarioRepository
    {
        public LimiteUsuarioRepository(IUnitOfWork unitOfWork, IDependencyInjectionContainer container) : base(unitOfWork, container)
        {
        }

        public LimiteUsuarioVO ObterLimiteUsuarioPeloEmail(string email)
        {
            LimiteUsuarioVO limite = null;

            var query = @"SELECT USU.EMAIL, COUNT(1)  AS TOTAL, USU_LIMITE.QUANTIDADEDIA AS LIMITE FROM PESSOAFISICA PF, USUARIO USU, USUARIO_LIMITECONSULTA USU_LIMITE  WHERE PF.USUARIO_ID = USU.ID
                        AND USU.EMAIL = '{0}'
                        AND USU.ID = USU_LIMITE.USUARIO_ID
                        AND DATE_FORMAT(PF.DATA_CADASTRO, '%d/%m/%Y') = DATE_FORMAT(NOW(), '%d/%m/%Y')
                        GROUP BY USU.EMAIL";

            var queryFormatada = String.Format(query, email);

            var comando = Session.Connection.CreateCommand();
            comando.CommandText = queryFormatada;

            using (var reader = comando.ExecuteReader())
            {
                while (reader.Read())
                {
                    limite = new LimiteUsuarioVO();
                    limite.Usuario = new Usuario() { Email = email }; ;
                    limite.LimiteConsultas = Convert.ToInt32(reader["LIMITE"]);
                    limite.Quantidade = Convert.ToInt32(reader["TOTAL"]);
                }
            }

            return limite;
        }
    }
}
