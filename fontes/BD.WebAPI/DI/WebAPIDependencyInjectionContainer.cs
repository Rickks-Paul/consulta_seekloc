using BD.DI;
using BD.Domain.Data;
using BD.Domain.Repository;
using BD.Infra.Repository;
using BD.Infra.UnitOfWork;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using System;
using System.Web.Http;

namespace BD.WebAPI.DI
{
    public class WebAPIDependencyInjectionContainer : IDependencyInjectionContainer
    {
        private static readonly Container instance = new Container();

        public static Container Instance
        {
            get
            {
                return instance;
            }
        }

        public void Register<TService>() where TService : class
        {
            instance.Register<TService>();
        }

        public TService Register<TService>(Func<TService> instanceCreator) where TService : class
        {
            return Register<TService>(instanceCreator);
        }

        public void RegisterDependencies()
        {
            //MapService.Register();

            Instance.Options.DefaultLifestyle = ScopedLifestyle.Transient;
            Instance.RegisterSingleton<IAplicativoRepository, AplicativoRepository>();
            Instance.RegisterSingleton<IAssinaturaRepository, AssinaturaRepository>();
            Instance.RegisterSingleton<IAtividadeRepository, AtividadeRepository>();
            Instance.RegisterSingleton<IConfiguracaoRepository, ConfiguracaoRepository>();
            Instance.RegisterSingleton<ICPFBloqueadoRepository, CPFBloqueadoRepository>();
            Instance.RegisterSingleton<ICreditoRepository, CreditoRepository>();
            Instance.RegisterSingleton<IEmailRepository, EmailRepository>();
            //Instance.RegisterSingleton<IEmpregoRepository, EmpregoRepository>();
            Instance.RegisterSingleton<IEnderecoAtualizadoRepository, EnderecoAtualizadoRepository>();
            //Instance.RegisterSingleton<IEnderecoRepository, EnderecoRepository>();
            Instance.RegisterSingleton<IErroConsultaRepository, ErroConsultaRepository>();
            Instance.RegisterSingleton<IErroLoginRepository, ErroLoginRepository>();
            Instance.RegisterSingleton<IErroSistemaRepository, ErroSistemaRepository>();
            Instance.RegisterSingleton<ILimiteUsuarioRepository, LimiteUsuarioRepository>();
            Instance.RegisterSingleton<ILogConsultaRepository, LogConsultaRepository>();
            Instance.RegisterSingleton<IPedidoRepository, PedidoRepository>();
            Instance.RegisterSingleton<IPedidoTecnicoRepository, PedidoTecnicoRepository>();
            Instance.RegisterSingleton<IPessoaFisica2Repository, PessoaFisica2Repository>();
            Instance.RegisterSingleton<IPessoaFisicaRepository, PessoaFisicaRepository>();
            Instance.RegisterSingleton<IRecuperacaoSenhaRepository, RecuperacaoSenhaRepository>();  
            Instance.RegisterSingleton<IServicoRepository, ServicoRepository>();
            Instance.RegisterSingleton<ITelefoneUsuarioRepository, TelefoneUsuarioRepository>();
            Instance.RegisterSingleton<IUsuarioRepository, UsuarioRepository>();
            instance.RegisterSingleton<IDependencyInjectionContainer, WebAPIDependencyInjectionContainer>();
            instance.RegisterSingleton<IUnitOfWork, NHibernateUnitOfWork>();
            Instance.RegisterWebApiControllers(GlobalConfiguration.Configuration);
            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(Instance);
        }

        public object Resolve(Type type)
        {
            return instance.GetInstance(type);
        }

        public TService Resolve<TService>() where TService : class
        {
            try
            {
                return instance.GetInstance<TService>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void VerifyConsistency()
        {
            if (!instance.IsVerifying)
            {
                instance.Verify();
            }
        }

        void IDependencyInjectionContainer.RegisterSingleton<TService, TImplementation>()
        {
            instance.Register<TService, TImplementation>(Lifestyle.Singleton);
        }

    }
}