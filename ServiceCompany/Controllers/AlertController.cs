using ServiceCompany.DbStuff.Models;
using ServiceCompany.DbStuff.Repositories;
using ServiceCompany.Hubs;
using ServiceCompany.Models;
using ServiceCompany.Models.Alerts;
using ServiceCompany.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ServiceCompany.Controllers
{
    public class AlertController : Controller
    {
        private IHubContext<AlertHub, IAlertHub> _alertHubContext;

        private AlertRepository _alertRepository;

        private AuthService _authService;

        public AlertController(IHubContext<AlertHub, IAlertHub> alertHubContext, AlertRepository alertRepository, AuthService authService)
        {
            _alertHubContext = alertHubContext;
            _alertRepository = alertRepository;
            _authService = authService;
        }

        [HttpGet]
        public IActionResult AddAlert()
        {
            var lastAlerts = _alertRepository.GetLastAlerts();

            var viewModel = new AlertViewModel();
            viewModel.IsSuperAdmin = _authService.IsSuperAdmin();

            viewModel.LastAlerts = lastAlerts
                .Select(lastAlert => new AlertViewModel
                {
                    AlertId = lastAlert.Id,
                    Message = lastAlert.Message,
                    CreationDate = lastAlert.CreationDate,
                    ExpireDate = lastAlert.ExpireDate,
                })
                .ToList();

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddAlert(AlertViewModel viewModel)
        {
            var lastAlerts = _alertRepository.GetLastAlerts();

            viewModel.LastAlerts = lastAlerts
                .Select(lastAlert => new AlertViewModel
                {
                    AlertId = lastAlert.Id,
                    Message = lastAlert.Message,
                    CreationDate = lastAlert.CreationDate,
                    ExpireDate = lastAlert.ExpireDate,
                })
                .ToList();

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var alert = new Alert()
            {
                Message = viewModel.Message,
                ExpireDate = viewModel.ExpireDate,
                IsRead = true,
                Author = _authService.GetCurrentMcUser()
            };

            _alertRepository.Add(alert);

            await _alertHubContext.Clients.All.PushAlertAsync(viewModel.Message, viewModel.ExpireDate, alert.Id);

            viewModel.IsSuperAdmin = _authService.IsSuperAdmin();

            CheckUser(viewModel);

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
