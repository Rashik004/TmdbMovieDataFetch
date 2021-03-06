﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ConsoleApplication2.Models
{
    public class ReleaseDateItem
    {
        [BsonElement("certification")]
        public string Certification { get; set; }

        /// <summary>
        /// A language code, e.g. en
        /// </summary>
        [BsonElement("iso_639_1")]
        public string Iso_639_1 { get; set; }

        [BsonElement("note")]
        public string Note { get; set; }

        [BsonElement("release_date")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime ReleaseDate { get; set; }

    }

}
