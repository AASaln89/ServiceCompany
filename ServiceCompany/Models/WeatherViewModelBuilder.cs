using ServiceCompany.ApiServices;
using ServiceCompany.Models.TestApiModels;

namespace ServiceCompany.Models
{
    public class WeatherViewModelBuilder
    {
        public WeatherViewModel Build(WeatherDto dto)
        {
            var viewModel = new WeatherViewModel();
            viewModel.TemperatureNow = dto.current.temperature_2m;
            viewModel.TemperaturesFor24Hours = dto
                .hourly
                .temperature_2m
                .Take(24)
                .ToList();

            return viewModel;
        }
    }
}
