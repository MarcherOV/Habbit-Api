using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace Habbit_Api.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)] // Store GUID as a string
        public string Id { get; set; } = Guid.NewGuid().ToString();  // Default to new GUID

        [BsonElement("auth0_id")] // MongoDB field "auth0_id"
        public string Auth0Id { get; set; }

        [BsonElement("username")] // MongoDB field "username"
        public string Username { get; set; }

        [BsonElement("email")] // MongoDB field "email"
        public string Email { get; set; }

        [BsonElement("avatar_url")] // MongoDB field "avatar_url"
        public string AvatarUrl { get; set; }

        [BsonElement("stats")] // MongoDB field "stats"
        public UserStats Stats { get; set; } = new UserStats(); // Assuming UserStats is another class

        [BsonElement("preferences")] // MongoDB field "preferences"
        public UserPreferences Preferences { get; set; } = new UserPreferences(); // Assuming UserPreferences is another class
    }
}
