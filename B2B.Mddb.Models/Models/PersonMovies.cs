using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace B2B.Mddb.Models.Models
{
    [BsonIgnoreExtraElements]
    public class PersonMovies
    {
        [BsonId]
        public ObjectId PersonId { get; set; }

        public TMDbLib.Objects.People.Person Person { get; set; }
        public List<ObjectId> MovieIds { get; set; }
    }
}
