using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using OneStreamMvvmBlazor.Shared;

namespace OneStreamMvvmBlazor.Client
{
    public class WeatherForecastGetterJson : IWeatherForecastGetter
    {
        private readonly HttpClient _httpClient;

        public WeatherForecastGetterJson(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<IEnumerable<WeatherForecastEntity>?> GetForecasts()
        {
            return _httpClient.GetFromJsonAsync<IEnumerable<WeatherForecastEntity>>("sample-data/weatherFOrecast.json");
        }
    }
}
