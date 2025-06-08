using MenagMeWebApi.Domain.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization;


namespace MenagMeWebApi.Infrastructure.Data
{
    public class MongoDbSettings
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
    }

    public class MenagMeMongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MenagMeMongoDbContext(IOptions<MongoDbSettings> settings)
        {

            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<User> Users =>
            _database.GetCollection<User>("Users");

        public IMongoCollection<Project> Projects =>
            _database.GetCollection<Project>("Projects");

        public IMongoCollection<Story> Stories =>
            _database.GetCollection<Story>("Stories");

        public IMongoCollection<Tasks> Tasks =>
            _database.GetCollection<Tasks>("Tasks");
    }
}
