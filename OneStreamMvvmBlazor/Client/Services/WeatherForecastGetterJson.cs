using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using OneStreamMvvmBlazor.Shared;
using Newtonsoft.Json;

namespace OneStreamMvvmBlazor.Client
{
    public class WeatherForecastGetterJson : IWeatherForecastGetter
    {
       private  string path = "weatherForecast.json";

        public Task<IEnumerable<WeatherForecastEntity>?> GetForecasts()
        {
            //using (StreamReader r = new StreamReader(@"C:\SourceFiles\weather3.json"))
            try
            {
                Console.WriteLine(Environment.CurrentDirectory);

                /*using (StreamReader r = new StreamReader(path))
                {
                    string json = r.ReadToEnd();
                    //IEnumerable<WeatherForecastEntity>? forecasts = JsonConvert.DeserializeObject<IEnumerable<WeatherForecastEntity>>(json);
                    //return Task.FromResult <WeatherForecastEntity> forecasts;

                    return (Task<IEnumerable<WeatherForecastEntity>?>)Task.Run(() =>
                    {
                        IEnumerable<WeatherForecastEntity>? forecasts = JsonConvert.DeserializeObject<IEnumerable<WeatherForecastEntity>>(json);
                    });
                }*/
                return null;
            }
            catch (Exception ex)
            {
                string fuckYouMS = ex.Message;
                throw;
            }
        }
    }
}
