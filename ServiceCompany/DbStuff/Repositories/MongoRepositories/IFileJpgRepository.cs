using ServiceCompany.DbStuff.ModelsMongo;
using MongoDB.Bson;

namespace ServiceCompany.DbStuff.Repositories.MongoRepositories
{
    public interface IFileJpgRepository
    {
        public FileJpg Add(FileJpg jpg);

        public FileJpg Get(string id);

        public List<FileJpg> GetAll();

        public string Remove(string id);
    }
}
