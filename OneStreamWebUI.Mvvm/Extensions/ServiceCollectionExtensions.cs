using Microsoft.Extensions.DependencyInjection;
using OneStreamWebUI.Mvvm.Bindings;
using OneStreamWebUI.Mvvm.Parameters;
using OneStreamWebUI.Mvvm.WeakEventListener;

namespace OneStreamWebUI.Mvvm.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection OneStreamMvvm(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IWeakEventManagerFactory, WeakEventManagerFactory>();
            serviceCollection.AddSingleton<IBindingFactory, BindingFactory>();
            serviceCollection.AddSingleton<IParameterResolver, ParameterResolver>();
            serviceCollection.AddSingleton<IParameterCache, ParameterCache>();
            serviceCollection.AddSingleton<IViewModelParameterSetter, ViewModelParameterSetter>();
            serviceCollection.AddHttpContextAccessor();

            return serviceCollection;
        }
    }
}