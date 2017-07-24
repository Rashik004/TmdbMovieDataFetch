using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace B2B.Mddb.Models.Models.DBSchema
{
    [BsonIgnoreExtraElements]
    public class MovieGenre
    {
        public ObjectId MovieId { get; set; }

        public List<Genre> Genres { get; set; }
    }
}
