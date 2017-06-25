using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace ConsoleApplication2.Models
{
    public class ImageData
    {
        [BsonElement("aspect_ratio")]
        public double AspectRatio { get; set; }

        [BsonElement("file_path")]
        public string FilePath { get; set; }

        [BsonElement("height")]
        public int Height { get; set; }

        /// <summary>
        /// A language code, e.g. en
        /// </summary>
        [BsonElement("iso_639_1")]
        public string Iso_639_1 { get; set; }

        [BsonElement("vote_average")]
        public double VoteAverage { get; set; }

        [BsonElement("vote_count")]
        public int VoteCount { get; set; }

        [BsonElement("width")]
        public int Width { get; set; }
    }

}
