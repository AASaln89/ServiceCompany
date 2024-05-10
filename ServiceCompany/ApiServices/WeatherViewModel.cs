namespace ServiceCompany.ApiServices
{
    public class WeatherViewModel
    {
        public double TemperatureNow { get; set; }
        public List<double> TemperaturesFor24Hours { get; set; }
    }
}