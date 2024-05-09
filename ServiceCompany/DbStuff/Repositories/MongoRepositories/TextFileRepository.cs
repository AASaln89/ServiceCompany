using Azure.Core;
using ServiceCompany.DbStuff.ModelsMongo;
using ServiceCompany.Models.MongoViewModels;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace ServiceCompany.DbStuff.Repositories.MongoRepositories
{
    public class TextFileRepository : ITextFileRepository
    {
        private MongoClient _mongoClient;
        private IMongoDatabase _mongoDb;
        private readonly IGridFSBucket _gridFS;

        public TextFileRepository()
        {
            _mongoClient = new MongoClient("");
            _mongoDb = _mongoClient.GetDatabase("");
            _gridFS = new GridFSBucket(_mongoDb);
        }

        public async Task Add(IFormFile file)
        {
            await _gridFS.UploadFromStreamAsync(file.FileName, file.OpenReadStream());
        }

        public async Task<List<FileViewModel>> GetAll()
        {
            var filter = Builders<GridFSFileInfo>.Filter.Empty;
            var filesInfo = await _gridFS.FindAsync(filter);
            var files = await filesInfo.ToListAsync();

            var fileTxtList = files.Select(fileInfo => new FileViewModel
            {
                Id = fileInfo.Id.ToString(),
                FileName = fileInfo.Filename,
            })
                .ToList();

            return fileTxtList;
        }

        public async Task Remove(ObjectId id)
        {
            await _gridFS.DeleteAsync(id);
        }
    }
}
