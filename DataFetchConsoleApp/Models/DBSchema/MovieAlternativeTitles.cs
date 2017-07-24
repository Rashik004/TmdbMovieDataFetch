using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ConsoleApplication2.Models.DBSchemas
{
    public class MovieAlternativeTitles
    {
        [BsonId]
        public ObjectId MovieId { get; set; }

        [BsonElement("alternativeTitles")]
        public List<TMDbLib.Objects.General.AlternativeTitle> AlternativeTitles { get; set; }
    }
}
