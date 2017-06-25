using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace ConsoleApplication2.Models
{
    public class ProductionCountry
    {
        /// <summary>
        /// A country code, e.g. US
        /// </summary>
        [BsonElement("iso_3166_1")]
        public string Iso_3166_1 { get; set; }

        [BsonElement("Name")]
        public string CountryName { get; set; }
    }
}
