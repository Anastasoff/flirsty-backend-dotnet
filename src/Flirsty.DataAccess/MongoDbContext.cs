using System.Configuration;
using Flirsty.DataAccess.Contracts;
using Flirsty.Domain.Entities;
using MongoDB.Driver;

namespace Flirsty.DataAccess
{
    public class MongoDbContext : IMongoDbContext
    {
        private static IMongoDbContext _mongoDbContext;

        private MongoDbContext()
        {
        }

        public IMongoDatabase MongoDatabase { get; set; }

        public static IMongoDbContext Create()
        {
            if (_mongoDbContext == null)
            {
                _mongoDbContext = new MongoDbContext();
                var url = new MongoUrl(ConfigurationManager.ConnectionStrings["MongoDbContextConnStr"].ConnectionString);
                var client = new MongoClient(url);
                _mongoDbContext.MongoDatabase = client.GetDatabase(url.DatabaseName);
                EnsureUniqueIndexOnUserEmail();
            }

            return _mongoDbContext;
        }

        private static void EnsureUniqueIndexOnUserEmail()
        {
            IMongoCollection<User> users = _mongoDbContext.MongoDatabase.GetCollection<User>(typeof(User).Name);
            var email = Builders<User>.IndexKeys.Ascending(t => t.Email);
            var options = new CreateIndexOptions { Unique = true, Name = "_email_"};
            users.Indexes.CreateOneAsync(email, options);
        }
    }
}