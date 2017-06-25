using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TMDbLib.Objects.People;

namespace ConsoleApplication2.Models
{
    [BsonIgnoreExtraElements]
    public class PersonMovies
    {
        [BsonId]
        public ObjectId PersonId { get; set; }

        public Person Person { get; set; }
        public List<ObjectId> MovieIds { get; set; }
    }
}
