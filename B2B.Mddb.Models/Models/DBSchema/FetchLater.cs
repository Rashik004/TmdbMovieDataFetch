using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace B2B.Mddb.Models.Models.DBSchema
{
    public class FetchLater
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public ObjectId EntityObjectId { get; set; }

        public int EntityTmdbId { get; set; }

        public EntityType EntityType { get; set; }
    }
}
