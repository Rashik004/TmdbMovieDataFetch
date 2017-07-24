using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ConsoleApplication2.Models.DBSchema
{
    public class FetchLater
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public ObjectId EntityObjectId { get; set; }

        public int EntityTmdbId { get; set; }

        public EntityType EntityType { get; set; }
    }
}
