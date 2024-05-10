namespace ServiceCompany.ApiServices
{
    public class WeatherApi
    {
        private HttpClient _httpClient;

        public WeatherApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<WeatherDto?> GetWeather(string latitude = "25.06", string longitude = "55.17")
        {
            return _httpClient.GetFromJsonAsync<WeatherDto>(
                $"/v1/forecast?latitude={latitude}&longitude={longitude}&current=temperature_2m&hourly=temperature_2m");
        }
    }
}