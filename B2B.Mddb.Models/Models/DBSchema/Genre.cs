using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace B2B.Mddb.Models.Models.DBSchema
{

    [BsonIgnoreExtraElements]
    public class Genre
    {
        [BsonId]
        public ObjectId GenreId { get; set; }

        public int TmdbId { get; set; }

        public string GenreName { get; set; }
    }
}
