using MongoDB.Bson;

namespace MongoDBTest.Models
{
    public class ModelBase
    {
        public ObjectId? _id { get; set; } = ObjectId.GenerateNewId();
    }
}
