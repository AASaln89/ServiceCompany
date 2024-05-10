using ServiceCompany.BusinessServices;
using ServiceCompany.Controllers.CustomAuthAttributes;
using ServiceCompany.DbStuff.Models;
using ServiceCompany.DbStuff.Models.Enums;
using ServiceCompany.DbStuff.Repositories;
using ServiceCompany.Models;
using ServiceCompany.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using ServiceCompany.ApiServices;

namespace ServiceCompany.Controllers
{
    public class ServiceCompanyController : Controller
    {
        private CompanyRepository _companyRepository;
        private ProjectRepository _projectRepository;
        private UserRepository _userRepository;
        private UserProfileRepository _userProfileRepository;
        private ArticleRepository _articleRepository;
        private UserTaskRepository _userTaskRepository;
        private MemberRoleRepository _memberRoleRepository;
        private TaskStatusRepository _taskStatusRepository;
        private AuthService _authService;
        private UserBusinessService _userBusinessService;
        private NumberApi _numberApi;
        private WeatherApi _weatherApi;
        private WeatherViewModelBuilder _weatherViewModelBuilder;

        private IWebHostEnvironment _webHostEnvironment;

        public ServiceCompanyController(
            CompanyRepository companyRepository,
            ProjectRepository projectRepository,
            UserRepository userRepository,
            UserTaskRepository userTaskRepository,
            MemberRoleRepository memberPermissionRepository,
            AuthService authService,
            IWebHostEnvironment webHostEnvironment,
            TaskStatusRepository taskStatusRepository,
            ArticleRepository articleRepository,
            UserBusinessService userBusinessService,
            UserProfileRepository userProfileRepository,
            NumberApi numberApi,
            WeatherApi weatherApi,
            WeatherViewModelBuilder weatherViewModelBuilder)
        {
            _companyRepository = companyRepository;
            _projectRepository = projectRepository;
            _userRepository = userRepository;
            _userTaskRepository = userTaskRepository;
            _memberRoleRepository = memberPermissionRepository;
            _authService = authService;
            _webHostEnvironment = webHostEnvironment;
            _taskStatusRepository = taskStatusRepository;
            _articleRepository = articleRepository;
            _userBusinessService = userBusinessService;
            _userProfileRepository = userProfileRepository;
            _numberApi = numberApi;
            _weatherApi = weatherApi;
            _weatherViewModelBuilder = weatherViewModelBuilder;
        }

        public async Task<IActionResult> Index()
        {
            var dbAllTasks = _userTaskRepository.GetAll()
                .ToList();

            var dbWorkTasks = _userTaskRepository.GetInProgressTasks().ToList();

            var dbCompletedTasks = _userTaskRepository.GetCompletedTasks().ToList();

            var dbProjects = _projectRepository
                .GetAll().ToList();

            var viewModel = new IndexViewModel();

            CheckUser(viewModel);

            viewModel.AllTasks = dbAllTasks
                .Select(dbAllTask => new TaskViewModel
                {
                    Name = dbAllTask.Name
                })
                .ToList();

            viewModel.WorkInProgressTasks = dbWorkTasks
                .Select(dbWorkTask => new TaskViewModel
                {
                    Name = dbWorkTask.Name
                })
                .ToList();

            viewModel.CompletedTasks = dbCompletedTasks
                .Select(dbCompletedTask => new TaskViewModel
                {
                    Name = dbCompletedTask.Name
                })
                .ToList();

            viewModel.Projects = dbProjects
                .Select(dbProject => new ProjectViewModel
                {
                    ProjectName = dbProject.Name
                })
                .ToList();

            viewModel.Users = _userBusinessService.GetUsersAndManagers();

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

        public async Task<IActionResult> IndexUsers()
        {
            var dbAllTasks = _userTaskRepository.GetAll();

            var dbWorkTasks = _userTaskRepository.GetInProgressTasks();

            var dbCompletedTasks = _userTaskRepository.GetCompletedTasks();

            var dbProjects = _projectRepository.GetAll();

            var viewModel = new IndexViewModel();

            viewModel.AllTasks = dbAllTasks
                .Select(dbAllTask => new TaskViewModel
                {
                    Name = dbAllTask.Name
                })
                .ToList();

            viewModel.WorkInProgressTasks = dbWorkTasks
                .Select(dbWorkTask => new TaskViewModel
                {
                    Name = dbWorkTask.Name
                })
                .ToList();

            viewModel.CompletedTasks = dbCompletedTasks
                .Select(dbCompletedTask => new TaskViewModel
                {
                    Name = dbCompletedTask.Name
                })
                .ToList();

            viewModel.Projects = dbProjects
                .Select(dbProject => new ProjectViewModel
                {
                    ProjectName = dbProject.Name
                })
                .ToList();

            viewModel.Users = _userBusinessService.GetUsers();

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

        public async Task<IActionResult> IndexAdmins()
        {
            var dbAllTasks = _userTaskRepository.GetAll();

            var dbWorkTasks = _userTaskRepository.GetInProgressTasks();

            var dbCompletedTasks = _userTaskRepository.GetCompletedTasks();

            var dbProjects = _projectRepository.GetAll();

            var viewModel = new IndexViewModel();

            viewModel.AllTasks = dbAllTasks
                .Select(dbAllTask => new TaskViewModel
                {
                    Name = dbAllTask.Name
                })
                .ToList();

            viewModel.WorkInProgressTasks = dbWorkTasks
                .Select(dbWorkTask => new TaskViewModel
                {
                    Name = dbWorkTask.Name
                })
                .ToList();

            viewModel.CompletedTasks = dbCompletedTasks
                .Select(dbCompletedTask => new TaskViewModel
                {
                    Name = dbCompletedTask.Name
                })
                .ToList();

            viewModel.Projects = dbProjects
                .Select(dbProject => new ProjectViewModel
                {
                    ProjectName = dbProject.Name
                })
                .ToList();

            viewModel.Users = _userBusinessService.GetManagers();

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

        public IActionResult Chat()
        {
            var dbArticles = _articleRepository.GetAllWithAuthor();

            var blogViewModel = new ChatViewModel();
            blogViewModel.UserNickName = _authService.GetCurrentUserNickName();

            blogViewModel.Articles =
                dbArticles
                .Select(dbArticle => new ArticleViewModel
                {
                    Id = dbArticle.Id,
                    Title = dbArticle.Title,
                    Description = dbArticle.Description,
                    ThumbsUp = dbArticle.ThumbsUp,
                    ThumbsDown = dbArticle.ThumbsDown,
                })
                .ToList();

            CheckUser(blogViewModel);

            blogViewModel.Articles.Count();

            return View(blogViewModel);
        }

        public IActionResult About()
        {
            var model = new BaseViewModel();
            CheckUser(model);
            return View(model);
        }

        public IActionResult Contacts()
        {
            var model = new BaseViewModel();
            CheckUser(model);
            return View(model);
        }

        [HttpGet]
        [AdminOnly]
        public IActionResult AdminPanel()
        {
            var dbRoles = _memberRoleRepository.GetAll();

            var dbCompanies = _companyRepository.GetCompaniesWithProfile();

            var dbProjects = _projectRepository.GetProjectsWithStatus();

            var dbExecuters = _userRepository.GetExecutors();

            var viewModel = new AdminPanelViewModel();

            viewModel.Roles = dbRoles
                .Select(dbPermission => new RoleViewModel
                {
                    Id = dbPermission.Id,
                    Permission = dbPermission.Role,
                })
                .ToList();

            viewModel.Companies = dbCompanies
                .Select(dbCompany => new CompanyViewModel
                {
                    Id = dbCompany.Id,
                    CompanyName = dbCompany.Name,
                    CompanyShortName = dbCompany.ShortName,
                    CompanyAdress = dbCompany?.Profile?.Address,
                    CompanyEmail = dbCompany?.Profile?.Email,
                    CompanyPhoneNumber = dbCompany?.Profile?.PhoneNumber,
                    CompanyStatus = dbCompany.IsActive.ToString(),
                })
                .ToList();

            viewModel.Projects = dbProjects
                .Select(dbProject => new ProjectViewModel
                {
                    Id = dbProject.Id,
                    ProjectName = dbProject.Name,
                    ProjectShortName = dbProject.ShortName,
                    ProjectAdress = dbProject.Address,
                    ProjectStatus = dbProject.IsActive.ToString(),
                    ProjectLinkCompany = dbProject.Company.Name
                })
                .ToList();

            viewModel.Executors = dbExecuters
                .Select(dbExecuter => new ExecutorViewModel
                {
                    Id = dbExecuter.Id,
                    Email = dbExecuter.Email,
                    Password = dbExecuter.Password,
                    ExpireDate = dbExecuter.ExpireDate,
                    FirstName = dbExecuter.Profile.FirstName,
                    LastName = dbExecuter.Profile.LastName,
                    //Company = dbExecuter.Company.Name,
                    MemberRole = _memberRoleRepository.GetById(dbExecuter.MemberRole.Id).Role,
                    MemberStatus = dbExecuter.IsActive.ToString()
                })
                .ToList();

            CheckUser(viewModel);

            return View(viewModel);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Profile()
        {
            var user = _authService.GetCurrentMcUser();
            var userProfile = _userProfileRepository.GetById(user.Id);
            var userTasks = _userTaskRepository.GetCurrentUserTasks(user);

            var model = new ProfileViewModel()
            {
                Id = userProfile.Id,
                FirstName = userProfile.FirstName,
                LastName = userProfile.LastName,
                NickName = userProfile.NickName,
                PhoneNumber = userProfile.PhoneNumber,
                Email = userProfile.User.Email,
                Password = userProfile.User.Password,
            };
            model.CurrentUserTasks = userTasks.Select(userTask => new TaskViewModel
            {
                Name = userTask.Name,
                Description = userTask.Description,
                Status = userTask.Status?.Status ?? "---",
                CreationDate = userTask.CreationDate,
                StartDate = userTask.StartDate,
                CompletionDate = userTask.CompletionDate
            })
                .ToList();

            CheckUser(model);

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Profile(ProfileViewModel model, int id)
        {
            _userRepository.UpdateUser(model, id);

            var user = _authService.GetCurrentMcUser();

            model.CurrentLocalLang = _authService.GetCurrentUserLocalLanguage();

            var userTasks = _userTaskRepository.GetCurrentUserTasks(user);

            model.CurrentUserTasks = userTasks.Select(userTask => new TaskViewModel
            {
                Name = userTask.Name,
                Description = userTask.Description,
                Status = userTask.Status?.Status ?? "---",
                CreationDate = userTask.CreationDate,
                StartDate = userTask.StartDate,
                CompletionDate = userTask.CompletionDate
            })
                .ToList();

            CheckUser(model);

            return View(model);
        }

        [HttpGet]
        [AdminOnly]
        public IActionResult AddCompany()
        {
            return View();
        }

        [HttpPost]
        [AdminOnly]
        public IActionResult AddCompany(CompanyViewModel companyViewModel)
        {
            companyViewModel.Companies = _companyRepository.GetAll()
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                .ToList();

            companyViewModel.Projects = _projectRepository.GetAll()
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                .ToList();

            companyViewModel.Permissions = _memberRoleRepository.GetAll()
                .Select(x => new SelectListItem(x.Role, x.Id.ToString()))
                .ToList();

            var company = new Company
            {
                Name = companyViewModel.CompanyName,
                ShortName = companyViewModel.CompanyShortName,

            };
            var companyProfile = new CompanyProfile
            {
                Email = companyViewModel.CompanyEmail,
                Address = companyViewModel.CompanyAdress,
                PhoneNumber = companyViewModel.CompanyPhoneNumber,
            };
            company.Profile = companyProfile;
            _companyRepository.Add(company);

            return RedirectToAction("AdminPanel");
        }

        [HttpGet]
        [AdminOnly]
        public IActionResult UpdateCompany(int companyId)
        {
            if (companyId > 0)
            {
                var company = _companyRepository.GetCompaniesWithProfile()
                .SingleOrDefault(x => x.Id == companyId);

                var viewModel = new CompanyViewModel();

                viewModel.Id = company.Id;
                viewModel.CompanyName = company.Name;
                viewModel.CompanyShortName = company.ShortName;
                viewModel.CompanyAdress = company?.Profile?.Address;
                viewModel.CompanyEmail = company?.Profile?.Email;
                viewModel.CompanyPhoneNumber = company?.Profile?.PhoneNumber;
                viewModel.CompanyStatus = company.IsActive.ToString();

                return View(viewModel);
            }

            return NotFound("Not Found");
        }

        [HttpPost]
        [AdminOnly]
        public IActionResult UpdateCompany(CompanyViewModel companyViewModels, int id, int statusId)
        {
            _companyRepository.UpdateCompany(companyViewModels, id, statusId);

            return RedirectToAction("AdminPanel");
        }

        [HttpPost]
        [AdminOnly]
        public IActionResult RemoveCompany(int id)
        {
            _companyRepository.Delete(id);

            return RedirectToAction("AdminPanel");
        }

        [HttpGet]
        [AdminOnly]
        public IActionResult AddProject()
        {
            ProjectViewModel projectViewModel = new ProjectViewModel();

            projectViewModel.Companies = _companyRepository.GetAll()
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                .ToList();

            projectViewModel.Projects = _projectRepository.GetAll()
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                .ToList();

            projectViewModel.Permissions = _memberRoleRepository.GetAll()
                .Select(x => new SelectListItem(x.Role, x.Id.ToString()))
                .ToList();

            return View(projectViewModel);
        }

        [HttpPost]
        [AdminOnly]
        public IActionResult AddProject(ProjectViewModel projectViewModel, int companyId)
        {
            projectViewModel.Companies = _companyRepository.GetAll()
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                .ToList();

            projectViewModel.Projects = _projectRepository.GetAll()
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                .ToList();

            projectViewModel.Permissions = _memberRoleRepository.GetAll()
                .Select(x => new SelectListItem(x.Role, x.Id.ToString()))
                .ToList();

            var project = new Project
            {
                Name = projectViewModel.ProjectName,
                ShortName = projectViewModel.ProjectShortName,
                Address = projectViewModel.ProjectAdress,
                Company = _companyRepository.GetById(companyId)
            };

            _projectRepository.Add(project);

            return RedirectToAction("AdminPanel");
        }

        [HttpPost]
        [AdminOnly]
        public IActionResult RemoveProject(int id)
        {
            _projectRepository.Delete(id);

            return RedirectToAction("AdminPanel");
        }

        [HttpGet]
        [AdminOnly]
        public IActionResult AddExecutor()
        {
            var executorViewModel = new ExecutorViewModel();

            executorViewModel.Companies = _companyRepository.GetAll()
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                .ToList();

            executorViewModel.Projects = _projectRepository.GetAll()
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                .ToList();

            executorViewModel.Permissions = _memberRoleRepository.GetAll()
                .Select(x => new SelectListItem(x.Role, x.Id.ToString()))
                .ToList();

            return View(executorViewModel);
        }

        [HttpPost]
        [AdminOnly]
        public IActionResult AddExecutor(ExecutorViewModel executorViewModel, int companyId, int projectId, int permissionId)
        {
            executorViewModel.Companies = _companyRepository.GetAll()
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                .ToList();

            executorViewModel.Projects = _projectRepository.GetAll()
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                .ToList();

            executorViewModel.Permissions = _memberRoleRepository.GetAll()
                .Select(x => new SelectListItem(x.Role, x.Id.ToString()))
                .ToList();

            _userBusinessService.AddExecutor(executorViewModel);

            return RedirectToAction("AdminPanel");
        }

        [HttpGet]
        [AdminOnly]
        public IActionResult UpdateExecutor(int id)
        {
            var viewModel = new ExecutorViewModel();

            if (id != 0)
            {
                var user = _userRepository.GetExecutor(id);

                viewModel.Id = user.Id;
                viewModel.Email = user.Email;
                viewModel.ExpireDate = user.ExpireDate;
                viewModel.Password = user.Password;
                viewModel.MemberRole = user.MemberRole.Role;
                viewModel.MemberStatus = user.IsActive.ToString();
                viewModel.NickName = user.Name;
                viewModel.Company = user.Company.Name;
                viewModel.Companies = _companyRepository.GetAll()
                    .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                    .ToList();
                viewModel.Projects = _projectRepository.GetAll()
                    .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                    .ToList();
                viewModel.Permissions = _memberRoleRepository.GetAll()
                    .Select(x => new SelectListItem(x.Role, x.Id.ToString()))
                    .ToList();

            }
            return View(viewModel);
        }

        [HttpPost]
        [AdminOnly]
        public IActionResult UpdateExecutor(ExecutorViewModel executorViewModels, int id, int statusId, int companyId, int projectId, int permissionId)
        {
            _userRepository.UpdateExecutor(executorViewModels, id, statusId, companyId, projectId, permissionId);

            return RedirectToAction("AdminPanel");
        }

        [HttpPost]
        [AdminOnly]
        public IActionResult RemoveExecutor(int id)
        {
            _userRepository.Delete(id);

            return RedirectToAction("AdminPanel");
        }

        [HttpPost]
        [AdminOnly]
        public IActionResult RemoveUser(int id)
        {
            _userRepository.Delete(id);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [UserOnly]
        public IActionResult AddTask()
        {
            var model = new TaskViewModel();

            CheckUser(model);

            return View(model);
        }

        [HttpPost]
        [UserOnly]
        public IActionResult AddTask(TaskViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var a = _taskStatusRepository.GetById((int)UserTaskStatusEnum.Create);

            var task = new UserTask
            {
                Name = model.Name,
                Description = model.Description,
                Status = _taskStatusRepository.GetById((int)UserTaskStatusEnum.Create),
                Author = _authService.GetCurrentMcUser()
            };

            _userTaskRepository.Add(task);

            return RedirectToAction("Profile", new { Id = task.Author.Id });
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
