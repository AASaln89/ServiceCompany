namespace ServiceCompany.Models.TestApiModels
{
    public class TestApiViewModel : BaseViewModel
    {
        public int Number { get; set; }

        public string JustFact { get; set; }

        public string MathFact { get; set; }

        public WeatherViewModel WeatherViewModel { get; set; }
    }
}
