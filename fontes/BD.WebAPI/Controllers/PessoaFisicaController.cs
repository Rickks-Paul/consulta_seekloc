using BuscaDadosAPI.Domain;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Linq;
using BD.Domain.Entity;
using BD.Domain.Service;
using BD.Domain.ValueObject;
using BD.DI;
using BD.Domain.Repository;
using BD.Domain.Enum;

namespace BuscaDadosAPI.Web.Controllers
{
    public class PessoaFisicaController : ApiController
    {
        private static IDependencyInjectionContainer container;

        public PessoaFisicaController(IDependencyInjectionContainer _container)
        {
            container = _container;
        }

        [HttpPost]
        public IHttpActionResult ObterPessoaFisicaPeloCPF(RequisicaoConsultaVO requisicao)
        {
            Boolean usaCredito = false;
            var erro = new ErroConsulta();
            erro.Cpfcnpj = requisicao.cpf;
            erro.Email = requisicao.email;
            erro.Aplicativo = requisicao.aplicativo;
            erro.Data = DateTime.Now;
            erro.Versao = requisicao.versao;
            erro.Ip = requisicao.ip;

            var consultaVO = new ConsultaCPFCNPJVO();
            consultaVO.CPF = requisicao.cpf;
            consultaVO.Email = requisicao.email;
            consultaVO.Versao = requisicao.versao;
            consultaVO.IP = requisicao.ip;
            consultaVO.Hash = requisicao.hash;

            var hashGerado = DomainUtil.GerarHashMD5(String.Concat(requisicao.cpf, requisicao.email, requisicao.ip, requisicao.cpf));

            consultaVO.Hash = requisicao.senha;
            var validacao = container.Resolve<ValidacaoConsultaCPFService>();

            validacao.Validate(consultaVO);

            if (!validacao.TemErro)
            {
                var pessoaFisicaRepository = container.Resolve<IPessoaFisicaRepository>();

                var pessoa = pessoaFisicaRepository.ObterUltimaConsultaPessoaFisicaPeloCPF(requisicao.cpf);
                var usuario = container.Resolve<IUsuarioRepository>().ObterPeloEmail(requisicao.email);
                if ((TipoAplicativoEnum)requisicao.aplicativo == TipoAplicativoEnum.Android)
                {
                    var telefoneUsuarioRepo = container.Resolve<ITelefoneUsuarioRepository>();
                    var telefone = telefoneUsuarioRepo.ObterTelefonePorUsuarioEIMEI(usuario.Id, requisicao.imei);
                    if (telefone != null && requisicao.imei != telefone.IMEI)
                    {
                        return Json(new { erro = "S", mensagem = String.Format("Já existe um telefone com outro IMEI cadastrado para esta conta. Fabricante: {0} Modelo: {1} - Caso tenha trocado de aparelho contate o desenvolvedor para fazer a atualização de IMEI do seu cadastro.", telefone.Fabricante, telefone.Modelo) });
                    }
                    else
                    {
                        TelefoneUsuario tel = new TelefoneUsuario();
                        tel.DataCadastro = DateTime.Now;
                        tel.Fabricante = requisicao.fabricante;
                        tel.IMEI = requisicao.imei;
                        tel.Modelo = requisicao.modelo;
                        tel.Numero = requisicao.numero;
                        tel.Operadora = requisicao.operadora;
                        tel.Usuario = usuario;
                    }
                }

                if (pessoa != null && !String.IsNullOrEmpty(pessoa.Nome) && pessoa.Vitalicio != "S")
                {
                    if (pessoa.DataCadastro.Date.AddDays(+90) < DateTime.Now.Date)
                    {
                        ILimiteUsuarioRepository limiteRepository = container.Resolve<ILimiteUsuarioRepository>();
                        var limite = limiteRepository.ObterLimiteUsuarioPeloEmail(requisicao.email);

                        if (limite != null)
                        {
                            ICreditoRepository creditoRepo = container.Resolve<ICreditoRepository>();
                            var credito = creditoRepo.ObterCreditoPeloUsuario(Convert.ToInt32(usuario.Id));
                            if (credito == null)
                            {
                                if (limite.Quantidade >= limite.LimiteConsultas && usuario.Especial != "S")
                                {
                                    return Json(new { erro = "S", mensagem = String.Format("O seu usuário já atingiu o limite diário de consultas, que são {0} consultas, caso queira fazer novas consultas aguarde até o próximo dia ou adquira um pacote de consultas avulsas.", limite.LimiteConsultas) });
                                }
                            }
                            else
                            {
                                usaCredito = true;
                            }
                        }
                        else
                        {
                            usaCredito = true;
                        }

                        pessoa = pessoaFisicaRepository.ObterNovaPessoaPeloCPF(requisicao.cpf);
                        if (pessoa.Id == 0)
                        {
                            pessoa.Id = (Int32?)null;
                        }

                        var logConsulta = new LogConsulta();
                        logConsulta.Origem = Convert.ToInt32(OrigemConsultaEnum.SeekLoc);
                        logConsulta.Tipo = Convert.ToInt32(TipoConsultaEnum.CPF);
                        logConsulta.Usuario = usuario;
                        logConsulta.CPFCNPJ = requisicao.cpf;
                        logConsulta.DataCadastro = DateTime.Now;
                        logConsulta.TemResultado = (pessoa != null && !String.IsNullOrEmpty(pessoa.Nome)) ? "S" : "N";
                        container.Resolve<LogConsultaCRUDService>().Salvar(logConsulta);

                        if (pessoa != null)
                        {
                            var atividadeAtual = new Atividade() { Usuario = usuario, IP = requisicao.ip, Data = DateTime.Now, TipoAtividade = Convert.ToInt32(TipoAtividade.ConsultaPessoaFisicaCPF), Origem = Convert.ToInt32(OrigemAcesso.Web), Descricao = "O usuário com o e-mail " + requisicao.email + " fez uma pesquisa de CPF com o número " + requisicao.cpf };

                            container.Resolve<AtividadeCRUDService>().Salvar(atividadeAtual);                                   

                            if (usaCredito)
                            {
                                container.Resolve<CreditoCRUDService>().Remover(Convert.ToInt32(usuario.Id), 1);
                            }

                            pessoa.UsuarioConsulta = usuario;
                            pessoa.CPF = requisicao.cpf;
                            pessoa.DataCadastro = DateTime.Now;

                            container.Resolve<PessoaFisica2CRUDService>().Salvar(pessoa);

                            pessoa.Enderecos.ToList().ForEach(x => x.PessoaFisica2 = null);
                            pessoa.Veiculos.ToList().ForEach(x => x.PessoaFisica2 = null);
                            pessoa.Emails.ToList().ForEach(x => x.PessoaFisica2 = null);
                            pessoa.Empregos.ToList().ForEach(x => x.PessoaFisica2 = null);
                            pessoa.Vizinhos.ToList().ForEach(x => x.PessoaFisica2 = null);
                            pessoa.Parentes.ToList().ForEach(x => x.PessoaFisica2 = null);
                            pessoa.Telefones.ToList().ForEach(x => x.PessoaFisica2 = null);

                            if (pessoa.Veiculos != null)
                            {
                                pessoa.Veiculos.ToList().ForEach(x => x.CombustivelTexto = DomainUtil.ObterCombustivelTexto(x.Combustivel));
                                pessoa.Veiculos.ToList().ForEach(x => x.EspecieTexto = DomainUtil.ObterEspecieVeiculoTexto(x.Especie));
                                pessoa.Veiculos.ToList().ForEach(x => x.TipoTexto = DomainUtil.ObterTipoVeiculoTexto(x.Tipo));
                            }

                            return Json(new
                            {
                                erro = "N",
                                Nome = pessoa.Nome,
                                CPF = pessoa.CPF,
                                DataNascimento = pessoa.DataNascimento,
                                NomeMae = pessoa.NomeMae,
                                Enderecos = pessoa.Enderecos,
                                Veiculos = pessoa.Veiculos,
                                Emails = pessoa.Emails,
                                Empregos = pessoa.Empregos,
                                Vizinhos = pessoa.Vizinhos,
                                Parentes = pessoa.Parentes,
                                Telefones = pessoa.Telefones,
                                Importado = pessoa.Importado,
                                Hash = Guid.NewGuid().ToString(),
                                HoraConsulta = DateTime.Now.ToString("HH:mm:ss"),
                                DataConsulta = DateTime.Now.ToString("dd/MM/yyyy"),
                                NovaAPI = pessoa.APISeekLoc
                            });
                        }
                    }
                    else
                    {
                        pessoa.Enderecos.ToList().ForEach(x => x.PessoaFisica2 = null);
                        pessoa.Veiculos.ToList().ForEach(x => x.PessoaFisica2 = null);
                        pessoa.Emails.ToList().ForEach(x => x.PessoaFisica2 = null);
                        pessoa.Empregos.ToList().ForEach(x => x.PessoaFisica2 = null);
                        pessoa.Vizinhos.ToList().ForEach(x => x.PessoaFisica2 = null);
                        pessoa.Parentes.ToList().ForEach(x => x.PessoaFisica2 = null);
                        pessoa.Telefones.ToList().ForEach(x => x.PessoaFisica2 = null);

                        if (pessoa.Veiculos != null)
                        {
                            pessoa.Veiculos.ToList().ForEach(x => x.CombustivelTexto = DomainUtil.ObterCombustivelTexto(x.Combustivel));
                            pessoa.Veiculos.ToList().ForEach(x => x.EspecieTexto = DomainUtil.ObterEspecieVeiculoTexto(x.Especie));
                            pessoa.Veiculos.ToList().ForEach(x => x.TipoTexto = DomainUtil.ObterTipoVeiculoTexto(x.Tipo));
                        }

                        return Json(new
                        {
                            erro = "N",
                            Nome = pessoa.Nome,
                            CPF = pessoa.CPF,
                            DataNascimento = pessoa.DataNascimento,
                            NomeMae = pessoa.NomeMae,
                            Enderecos = pessoa.Enderecos,
                            Veiculos = pessoa.Veiculos,
                            Emails = pessoa.Emails,
                            Empregos = pessoa.Empregos,
                            Vizinhos = pessoa.Vizinhos,
                            Parentes = pessoa.Parentes,
                            Telefones = pessoa.Telefones,
                            Importado = pessoa.Importado,
                            Hash = Guid.NewGuid().ToString(),
                            HoraConsulta = DateTime.Now.ToString("HH:mm:ss"),
                            DataConsulta = DateTime.Now.ToString("dd/MM/yyyy")
                        });
                    }
                }
                else
                {
                    ILimiteUsuarioRepository limiteRepository = container.Resolve<ILimiteUsuarioRepository>();
                    var limite = limiteRepository.ObterLimiteUsuarioPeloEmail(requisicao.email);

                    if (limite != null)
                    {
                        ICreditoRepository creditoRepo = container.Resolve<ICreditoRepository>();
                        var credito = creditoRepo.ObterCreditoPeloUsuario(Convert.ToInt32(usuario.Id));
                        if (credito == null)
                        {
                            if (limite.Quantidade >= limite.LimiteConsultas && usuario.Especial != "S")
                            {
                                return Json(new { erro = "S", mensagem = String.Format("O seu usuário já atingiu o limite diário de consultas, que são {0} consultas, caso queira fazer novas consultas aguarde até o próximo dia ou adquira um pacote de consultas avulsas.", limite.LimiteConsultas) });
                            }
                        }
                        else
                        {
                            usaCredito = true;
                        }
                    }
                    else
                    {
                        usaCredito = true;
                    }

                    pessoa = pessoaFisicaRepository.ObterNovaPessoaPeloCPF(requisicao.cpf);

                    if (pessoa != null)
                    {
                        if (usaCredito)
                        {
                            container.Resolve<CreditoCRUDService>().Remover(Convert.ToInt32(usuario.Id), 1);
                        }
                    }

                    var logConsulta = new LogConsulta();
                    logConsulta.Origem = Convert.ToInt32(OrigemConsultaEnum.SeekLoc);
                    logConsulta.Tipo = Convert.ToInt32(TipoConsultaEnum.CPF);
                    logConsulta.Usuario = usuario;
                    logConsulta.CPFCNPJ = requisicao.cpf;
                    logConsulta.DataCadastro = DateTime.Now;
                    logConsulta.TemResultado = (pessoa != null && !String.IsNullOrEmpty(pessoa.Nome)) ? "S" : "N";

                    container.Resolve<LogConsultaCRUDService>().Salvar(logConsulta);

                    if (pessoa != null && String.IsNullOrEmpty(pessoa.Mensagem))
                    {
                        var atividadeAtual = new Atividade() { Usuario = usuario, IP = requisicao.ip, Data = DateTime.Now, TipoAtividade = Convert.ToInt32(TipoAtividade.ConsultaPessoaFisicaCPF), Origem = Convert.ToInt32(OrigemAcesso.Web), Descricao = "O usuário com o e-mail " + requisicao.email + " fez uma pesquisa de CPF com o número " + requisicao.cpf };

                        container.Resolve<AtividadeCRUDService>().Salvar(atividadeAtual);

                        pessoa.UsuarioConsulta = usuario;
                        pessoa.CPF = requisicao.cpf;
                        pessoa.Hash = requisicao.hash;
                        pessoa.DataCadastro = DateTime.Now;

                        container.Resolve<PessoaFisica2CRUDService>().Salvar(pessoa);

                        pessoa.Enderecos.ToList().ForEach(x => x.PessoaFisica2 = null);
                        pessoa.Veiculos.ToList().ForEach(x => x.PessoaFisica2 = null);
                        pessoa.Emails.ToList().ForEach(x => x.PessoaFisica2 = null);
                        pessoa.Empregos.ToList().ForEach(x => x.PessoaFisica2 = null);
                        pessoa.Vizinhos.ToList().ForEach(x => x.PessoaFisica2 = null);
                        pessoa.Parentes.ToList().ForEach(x => x.PessoaFisica2 = null);
                        pessoa.Telefones.ToList().ForEach(x => x.PessoaFisica2 = null);

                        if (pessoa.Veiculos != null)
                        {
                            pessoa.Veiculos.ToList().ForEach(x => x.CombustivelTexto = DomainUtil.ObterCombustivelTexto(x.Combustivel));
                            pessoa.Veiculos.ToList().ForEach(x => x.EspecieTexto = DomainUtil.ObterEspecieVeiculoTexto(x.Especie));
                            pessoa.Veiculos.ToList().ForEach(x => x.TipoTexto = DomainUtil.ObterTipoVeiculoTexto(x.Tipo));
                        }

                        return Json(new
                        {
                            erro = "N",
                            Nome = pessoa.Nome,
                            CPF = pessoa.CPF,
                            DataNascimento = pessoa.DataNascimento,
                            NomeMae = pessoa.NomeMae,
                            Enderecos = pessoa.Enderecos,
                            Veiculos = pessoa.Veiculos,
                            Emails = pessoa.Emails,
                            Empregos = pessoa.Empregos,
                            Vizinhos = pessoa.Vizinhos,
                            Parentes = pessoa.Parentes,
                            Telefones = pessoa.Telefones,
                            Importado = pessoa.Importado,
                            Hash = Guid.NewGuid().ToString(),
                            HoraConsulta = DateTime.Now.ToString("HH:mm:ss"),
                            DataConsulta = DateTime.Now.ToString("dd/MM/yyyy")
                        });
                    }
                    else
                    {
                        return Json(new { erro = "S", mensagem = "Sem resultado para este CPF." });
                    }
                }
            }
            else
            {
                return Json(new { erro = "S", mensagem = validacao.erroVO.mensagem });
            }


            return Json(new { erro = "S", mensagem = "Sem resultado" });
        }
    }
}