using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using OneStreamMvvmBlazor.Client.Pages;
using Index = OneStreamMvvmBlazor.Client.Pages.Index;

namespace OneStreamMvvmBlazor.Client
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddViewModels(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<WeatherForecastsViewModel>();
            serviceCollection.AddTransient<CounterViewModel>();
            serviceCollection.AddTransient<ClockViewModel>();
            serviceCollection.AddTransient<ParametersViewModel>();
            serviceCollection.AddScoped<NavbarViewModel>();

            return serviceCollection;
        }

        public static IServiceCollection AddComponents(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddNavigationItem<Index>("Index", "oi-home");
            serviceCollection.AddNavigationItem<Counter>("Counter", "oi-plus");
            serviceCollection.AddNavigationItem<WeatherForecasts>("Weather forecasts", "oi-list-rich");
            serviceCollection.AddNavigationItem<Clock>("Clock", "oi-home");
            serviceCollection.AddNavigationItem<Parameters>("Parameters", "oi-home");

            return serviceCollection;
        }

        public static IServiceCollection AddNavigationItem<TPage>(this IServiceCollection serviceCollection, string title, string? icon = null) where TPage : ComponentBase
        {
            serviceCollection.AddSingleton(new NavbarItem(typeof(TPage), title, icon));
            return serviceCollection;
        }

        public static IServiceCollection AddDomain(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<INavbarService, NavbarService>();
            return serviceCollection;
        }
    }
}
