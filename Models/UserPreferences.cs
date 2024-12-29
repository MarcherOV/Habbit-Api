using MongoDB.Bson.Serialization.Attributes;

namespace Habbit_Api.Models
{
    public class UserPreferences
    {
        [BsonElement("theme")]
        public string Theme { get; set; }

        [BsonElement("notifications_enabled")]
        public bool NotificationsEnabled { get; set; }
    }
}
