using ServiceCompany.DbStuff.ModelsMongo;
using ServiceCompany.Models.MongoViewModels;
using MongoDB.Bson;

namespace ServiceCompany.DbStuff.Repositories.MongoRepositories
{
    public interface ITextFileRepository
    {
        Task Add(IFormFile file);
        Task<List<FileViewModel>> GetAll();
        Task Remove(ObjectId id);
    }
}
