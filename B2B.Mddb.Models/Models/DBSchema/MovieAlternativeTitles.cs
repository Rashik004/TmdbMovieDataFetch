using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace B2B.Mddb.Models.Models.DBSchema
{
    public class MovieAlternativeTitles
    {
        [BsonId]
        public ObjectId MovieId { get; set; }

        [BsonElement("alternativeTitles")]
        public List<TMDbLib.Objects.General.AlternativeTitle> AlternativeTitles { get; set; }
    }
}
