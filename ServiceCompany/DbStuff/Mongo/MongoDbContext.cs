using MongoDB.Driver;

namespace ServiceCompany.DbStuff.ModelsMongo
{
    public class MongoDbContext : MongoClient
    {
        public MongoDbContext() : base () { }
    }
}
