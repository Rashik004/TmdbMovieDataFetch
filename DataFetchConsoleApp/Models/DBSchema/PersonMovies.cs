using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TMDbLib.Objects.People;

namespace ConsoleApplication2.Models.DBSchema
{
    [BsonIgnoreExtraElements]
    public class PersonMovies:Person
    {
        //public PersonMovies()
        //{
            
        //}

        public List<ObjectId> MovieIds { get; set; }
    }
}
