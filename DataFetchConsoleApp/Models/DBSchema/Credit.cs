using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;

namespace ConsoleApplication2.Models.DBSchemas
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
