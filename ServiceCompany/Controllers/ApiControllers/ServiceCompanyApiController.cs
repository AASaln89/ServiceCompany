using ServiceCompany.BusinessServices;
using ServiceCompany.DbStuff.Repositories;
using ServiceCompany.Models;
using Microsoft.AspNetCore.Mvc;

namespace ServiceCompany.Controllers.ApiControllers
{
    [ApiController]
    [Route("/api/ServiceCompany/{action}")]
    public class ServiceCompanyApiController : Controller
    {
        private ArticleRepository _articleRepository;
        private UserBusinessService _userBusinessService;

        public ServiceCompanyApiController(ArticleRepository articleRepository, UserBusinessService userBusinessService)
        {
            _articleRepository = articleRepository;
            _userBusinessService = userBusinessService;
        }

        public int? AddLike(int articleId)
        {
            return _articleRepository.AddLike(articleId);
        }

        public int? AddDislike(int articleId)
        {
            return _articleRepository.AddDislike(articleId);
        }

        public List<UserViewModel> GetUsers()
        {
            return _userBusinessService.GetUsersAndManagers();
        }

        public int AddExecutor([FromBody]ExecutorViewModel viewModel)
        {
            return _userBusinessService.AddExecutor(viewModel);
        }

        [Route("{id}")]
        public void DeleteExecutor([FromRoute] int id)
        {
            _userBusinessService.DeleteExecutor(id);
        }
    }
}
