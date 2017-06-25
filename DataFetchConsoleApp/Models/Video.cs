using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace ConsoleApplication2.Models
{
    public class Video
    {
        [BsonId]
        public string Id { get; set; }

        /// <summary>
        /// A country code, e.g. US
        /// </summary>
        [BsonElement("iso_3166_1")]
        public string Iso_3166_1 { get; set; }

        /// <summary>
        /// A language code, e.g. en
        /// </summary>
        [BsonElement("iso_639_1")]
        public string Iso_639_1 { get; set; }

        [BsonElement("key")]
        public string Key { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("site")]
        public string Site { get; set; }

        [BsonElement("size")]
        public int Size { get; set; }

        [BsonElement("type")]
        public string Type { get; set; }
    }

}
