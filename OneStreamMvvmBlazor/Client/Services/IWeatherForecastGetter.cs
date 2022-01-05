using System.Collections.Generic;
using System.Threading.Tasks;
using OneStreamMvvmBlazor.Shared;

namespace OneStreamMvvmBlazor.Client.Services
{
    public interface IWeatherForecastGetter
    {
        Task<IEnumerable<WeatherForecastEntity>> GetForecasts();
    }
}
