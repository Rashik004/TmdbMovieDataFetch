using MongoDB.Bson.Serialization.Attributes;

namespace B2B.Mddb.Models.Models.EmbeddedData
{
    public class GenreBase
    {
        [BsonId]
        public int GenreId { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }
    }
}
