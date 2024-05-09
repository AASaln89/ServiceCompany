using ServiceCompany.BusinessServices;
using ServiceCompany.Controllers.CustomAuthAttributes;
using ServiceCompany.DbStuff.Models;
using ServiceCompany.DbStuff.ModelsMongo;
using ServiceCompany.DbStuff.Repositories;
using ServiceCompany.DbStuff.Repositories.MongoRepositories;
using ServiceCompany.Models;
using ServiceCompany.Models.MongoViewModels;
using ServiceCompany.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace ServiceCompany.Controllers.MongoControllers
{
    public class JpgController : Controller
    {
        private ProjectRepository _projectRepository;
        private UserTaskRepository _userTaskRepository;
        private AuthService _authService;
        private IFileJpgRepository _fileJpgRepository;
        private ITextFileRepository _fileRepository;

        public JpgController(IFileJpgRepository fileJpgRepository, ITextFileRepository fileRepository,
            AuthService authService, UserTaskRepository userTaskRepository, ProjectRepository projectRepository)
        {
            _fileJpgRepository = fileJpgRepository;
            _fileRepository = fileRepository;
            _authService = authService;
            _userTaskRepository = userTaskRepository;
            _projectRepository = projectRepository;
        }

        [SuperAdminOnly]
        public IActionResult Index()
        {
            var user = _authService.GetCurrentUser();

            var dbAllTasks = _userTaskRepository.GetAll()
            .ToList();

            var dbWorkTasks = _userTaskRepository.GetInProgressTasks().ToList();

            var dbCompletedTasks = _userTaskRepository.GetCompletedTasks().ToList();

            var dbProjects = _projectRepository
                .GetAll().ToList();

            var viewModel = new IndexMongoViewModel();

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

            var files = _fileJpgRepository.GetAll();
            var txtFiles = _fileRepository.GetAll();

            viewModel.FilesJpg = files
                .Select(x => new FileJpgViewModel
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToList();

            viewModel.Files = txtFiles.Result;

            return View(viewModel);
        }

        [HttpPost]
        [SuperAdminOnly]
        public IActionResult UploadFile(IFormFile file)
        {
            _fileRepository.Add(file);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [SuperAdminOnly]
        public IActionResult RemoveFile(string id)
        {
            if (!ObjectId.TryParse(id, out var objId))
            {
                return BadRequest("Invalid ObjectId format");
            }
            _fileRepository.Remove(objId);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [SuperAdminOnly]
        public IActionResult AddJpg(string name)
        {
            var fileJpg = new FileJpg
            {
                Name = name
            };

            _fileJpgRepository.Add(fileJpg);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [SuperAdminOnly]
        public IActionResult RemoveJpg(string id)
        {
            _fileJpgRepository.Remove(id);

            return RedirectToAction("Index");
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