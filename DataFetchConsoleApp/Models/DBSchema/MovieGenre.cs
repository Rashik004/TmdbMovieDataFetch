using System.Collections.Generic;
using ConsoleApplication2.Models.DBSchemas;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ConsoleApplication2.Models.DBSchema
{
    [BsonIgnoreExtraElements]
    public class MovieGenre
    {
        public ObjectId MovieId { get; set; }

        public List<Genre> Genres { get; set; }
    }
}
