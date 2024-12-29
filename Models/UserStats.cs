using MongoDB.Bson.Serialization.Attributes;

namespace Habbit_Api.Models
{
    public class UserStats
    {
        [BsonElement("score")]
        public int Score { get; set; }

        [BsonElement("level")]
        public int Level { get; set; }

    }
}
