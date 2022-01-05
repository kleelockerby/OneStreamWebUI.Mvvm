using System.Collections.Generic;
using System.Threading.Tasks;
using OneStreamMvvmBlazor.Shared;
using Newtonsoft.Json;


namespace OneStreamMvvmBlazor.Client
{
    public class WeatherForecastGetterJson : IWeatherForecastGetter
    {
        public Task<IEnumerable<WeatherForecastEntity>?> GetForecasts()
        {
            using (StreamReader r = new StreamReader(@"C:\SourceFiles\weather3.json"))
            {
                string json = r.ReadToEnd();
                //IEnumerable<WeatherForecastEntity>? forecasts = JsonConvert.DeserializeObject<IEnumerable<WeatherForecastEntity>>(json);
                //return Task.FromResult <WeatherForecastEntity> forecasts;

                return (Task<IEnumerable<WeatherForecastEntity>?>)Task.Run(() =>
                {
                    IEnumerable<WeatherForecastEntity>? forecasts = JsonConvert.DeserializeObject<IEnumerable<WeatherForecastEntity>>(json);
                });
            }
        }
    }
}
