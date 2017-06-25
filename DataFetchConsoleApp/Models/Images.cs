using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace ConsoleApplication2.Models
{
    public class Images
    {
        [BsonElement("backdrops")]
        public List<ImageData> Backdrops { get; set; }

        [BsonElement("posters")]
        public List<ImageData> Posters { get; set; }
    }

}
