using System.Collections.Generic;
using System.Threading.Tasks;
using OneStreamMvvmBlazor.Shared;

namespace OneStreamMvvmBlazor.Client
{
    public interface IWeatherForecastGetter
    {
        Task<IEnumerable<WeatherForecastEntity>> GetForecasts();
    }
}
