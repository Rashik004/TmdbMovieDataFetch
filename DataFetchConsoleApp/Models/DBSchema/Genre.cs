using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ConsoleApplication2.Models.DBSchemas
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
