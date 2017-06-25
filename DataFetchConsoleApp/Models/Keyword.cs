using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace ConsoleApplication2.Models
{
    public class Keyword
    {
        [BsonId]
        public int KeywordId { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }
    }
}
