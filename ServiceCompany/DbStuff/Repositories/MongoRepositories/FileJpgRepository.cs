using ServiceCompany.DbStuff.ModelsMongo;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ServiceCompany.DbStuff.Repositories.MongoRepositories
{
    public class FileJpgRepository : IFileJpgRepository
    {
        private MongoClient _mongoClient;
        private IMongoDatabase _mongoDb;
        private IMongoCollection<FileJpg> _mongoDbTable;

        public FileJpgRepository()
        {
            _mongoClient = new MongoClient("localhost:");
            _mongoDb = _mongoClient.GetDatabase("FileDb");
            _mongoDbTable = _mongoDb.GetCollection<FileJpg>("ImageJpg");
        }

        public FileJpg Add(FileJpg jpg)
        {
            var file = _mongoDbTable.Find(x => x.Id == jpg.Id).FirstOrDefault();

            if (file is null)
            {
                _mongoDbTable.InsertOne(jpg);
            }
            else
            {
                _mongoDbTable.ReplaceOne(x => x.Id == jpg.Id, jpg);
            }

            return jpg;
        }

        public FileJpg Get(string id)
        {
            return _mongoDbTable.Find(x => x.Id == id).FirstOrDefault();
        }

        public List<FileJpg> GetAll()
        {
            return _mongoDbTable.Find(FilterDefinition<FileJpg>.Empty).ToList();
        }

        public string Remove(string id)
        {
            _mongoDbTable.DeleteOne(x => x.Id == id);

            return "Removed";
        }
    }
}
