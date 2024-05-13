using Microsoft.AspNetCore.Mvc;
using ServiceCompany.ApiServices;
using ServiceCompany.Models;
using ServiceCompany.Models.TestApiModels;
using ServiceCompany.Services;

namespace ServiceCompany.Controllers
{
    public class TestApiController : Controller
    {
        private AuthService _authService;
        private NumberApi _numberApi;
        private WeatherApi _weatherApi;
        private WeatherViewModelBuilder _weatherViewModelBuilder;

        public TestApiController(AuthService authService, NumberApi numberApi, WeatherApi weatherApi,
            WeatherViewModelBuilder weatherViewModelBuilder)
        {
            _authService = authService;
            _numberApi = numberApi;
            _weatherApi = weatherApi;
            _weatherViewModelBuilder = weatherViewModelBuilder;
        }

        public IActionResult Index()
        {
            var viewModel = new TestApiViewModel();

            CheckUser(viewModel);

            var number = DateTime.Now.Second;
            var justFactTask = _numberApi.GetFact(number);
            var mathFactTask = _numberApi.GetMathFact(number);
            var weatherTask = _weatherApi.GetWeather();

            Task.WaitAll(justFactTask, mathFactTask, weatherTask);

            viewModel.Number = number;
            viewModel.JustFact = justFactTask.Result;
            viewModel.MathFact = mathFactTask.Result;
            viewModel.WeatherViewModel = _weatherViewModelBuilder.Build(weatherTask.Result);

            return View(viewModel);
        }

        private void CheckUser(BaseViewModel model)
        {
            var user = _authService.GetCurrentUser();
            if (user is not null)
            {
                if (user?.MemberRole?.Id == 5)
                {
                    model.IsUser = true;
                }
                else if (user?.MemberRole?.Id == 1)
                {
                    model.IsSuperAdmin = true;
                }
                else
                {
                    model.IsAdmin = true;
                }
            }
            else
            {
                model.IsGuest = true;
            }
        }
    }
}
