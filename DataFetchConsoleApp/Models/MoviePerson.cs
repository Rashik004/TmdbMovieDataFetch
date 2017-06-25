using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ConsoleApplication2.Models
{
    public class MoviePerson
    {
        [BsonId]
        public ObjectId MovieId { get; set; }

        public int PersonId { get; set; }

    }
}
