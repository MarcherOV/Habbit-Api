using MongoDB.Driver;
using Habbit_Api.Models;

namespace Habbit_Api.Services
{
    public interface IDatabaseService
    {
        IMongoCollection<User> Users { get; }
        IMongoCollection<Habbit_Api.Models.Task> Tasks { get; }
    }
}
