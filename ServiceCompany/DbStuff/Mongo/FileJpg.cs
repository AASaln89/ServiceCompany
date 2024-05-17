using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ServiceCompany.DbStuff.ModelsMongo
{
    public class FileJpg
    {
        [BsonRepresentation(BsonType.ObjectId)]

        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        public string Name { get; set; } 
    }
}
