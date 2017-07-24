using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace B2B.Mddb.Models.Models
{
    public class MoviePerson
    {
        [BsonId]
        public ObjectId MovieId { get; set; }

        public int PersonId { get; set; }

    }
}
