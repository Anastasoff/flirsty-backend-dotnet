using MongoDB.Driver;

namespace Flirsty.DataAccess.Contracts
{
    public interface IMongoDbContext
    {
        IMongoDatabase MongoDatabase { get; set; }
    }
}