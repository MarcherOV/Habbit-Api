using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Habbit_Api.Models
{
    public class Task
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [BsonElement("user_id")]
        public string UserId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public Attribute Attribute { get; set; }
        public Type Type { get; set; }

        [BsonElement("score")]
        public double Score { get; set; }

        [BsonElement("completion_date")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime? CompletionDate { get; set; }
    }

    public enum Type
    {
        Habbit,
        Goal
    }
    public enum Attribute
    {
        Strength,
        Intelligence,
        Charisma
    }
}
