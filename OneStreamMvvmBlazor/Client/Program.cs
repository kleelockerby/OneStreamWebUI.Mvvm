using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using OneStreamMvvmBlazor.Client;
using OneStreamWebUI.Mvvm.Extensions;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.OneStreamMvvm();
builder.Services.AddDomain();
builder.Services.AddComponents();
builder.Services.AddViewModels();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IWeatherForecastGetter, WeatherForecastGetter>();

await builder.Build().RunAsync();
