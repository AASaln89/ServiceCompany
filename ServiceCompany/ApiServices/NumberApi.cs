namespace ServiceCompany.ApiServices
{
    public class NumberApi
    {
        private HttpClient _httpClient;

        public NumberApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<string> GetFact(int number = 1)
        {
            return _httpClient.GetStringAsync($"/{number}");
        }

        public Task<string> GetMathFact(int number = 1)
        {
            return _httpClient.GetStringAsync($"/{number}/math");
        }
    }
}
