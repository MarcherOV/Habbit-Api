using MongoDB.Bson.Serialization.Attributes;

namespace Habbit_Api.Models
{
    public class UserStats
    {
        [BsonElement("currentProgressStrengh")]
        public double СurrentProgressStrengh { get; set; }

        [BsonElement("currentProgressIntelligence")]
        public double СurrentProgressIntelligence { get; set; }

        [BsonElement("currentProgressCharisma")]
        public double СurrentProgressCharisma { get; set; }
    }
}
