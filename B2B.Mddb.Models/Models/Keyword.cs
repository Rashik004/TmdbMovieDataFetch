using MongoDB.Bson.Serialization.Attributes;

namespace B2B.Mddb.Models.Models
{
    public class Keyword
    {
        [BsonId]
        public int KeywordId { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }
    }
}
