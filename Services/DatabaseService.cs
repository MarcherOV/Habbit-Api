using MongoDB.Driver;
using MongoDB.Bson;
using Habbit_Api.Models;

namespace Habbit_Api.Services
{
    public class DatabaseService : IDatabaseService
    {
        private readonly IMongoDatabase _database;

        public DatabaseService(IMongoClient mongoClient)
        {
            _database = mongoClient.GetDatabase("db-name");
        }

        public IMongoCollection<User> Users => _database.GetCollection<User>("users");
        public IMongoCollection<Habbit_Api.Models.Task> Tasks => _database.GetCollection<Habbit_Api.Models.Task>("tasks");
    }
}