using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace B2B.Mddb.Models.Models.DBSchema
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
