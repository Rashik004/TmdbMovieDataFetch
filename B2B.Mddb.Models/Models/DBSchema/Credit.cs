using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace B2B.Mddb.Models.Models.DBSchema
{
    [BsonIgnoreExtraElements]
    public class Credit
    {
        [BsonId]
        public ObjectId MovieId { get; set; }

        [BsonElement("casts")]
        public List<Cast> Casts { get; set; }

        [BsonElement("crew")]
        public List<Crew> Crew { get; set; }

    }
}
