using System;

namespace BD.DI
{
    public interface IDependencyInjectionContainer
    {
        void RegisterDependencies();
        Object Resolve(Type type);
        TService Resolve<TService>() where TService : class;
        TService Register<TService>(Func<TService> instanceCreator) where TService : class;
        void Register<TService>() where TService : class;
        void RegisterSingleton<TService, TImplementation>() where TService : class
            where TImplementation : class, TService;
        void VerifyConsistency();
    }
}
