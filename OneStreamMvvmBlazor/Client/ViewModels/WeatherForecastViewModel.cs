using System;
using OneStreamWebUI.Mvvm.ViewModel;
using OneStreamMvvmBlazor.Shared;

namespace OneStreamMvvmBlazor.Client
{
    public class WeatherForecastViewModel : ViewModelBase
    {
        private readonly WeatherForecastEntity _weatherForecastEntity;
        private int _temperatureC;
        private int _temperatureF;

        public WeatherForecastViewModel(WeatherForecastEntity weatherForecastEntity)
        {
            _weatherForecastEntity = weatherForecastEntity;
            TemperatureC = _weatherForecastEntity.TemperatureC;
            TemperatureF = _weatherForecastEntity.TemperatureF;
        }

        public DateTime Date => _weatherForecastEntity.Date;
        public string? Summary => _weatherForecastEntity.Summary;

        public int TemperatureC
        {
            get => _temperatureC;
            set => Set(ref _temperatureC, value, nameof(TemperatureC));
        }

        public int TemperatureF
        {
            get => _temperatureF;
            set => Set(ref _temperatureF, value, nameof(TemperatureF));
        }
    }
}