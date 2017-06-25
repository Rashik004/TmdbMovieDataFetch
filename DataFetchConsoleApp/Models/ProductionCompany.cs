using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace ConsoleApplication2.Models
{
    public class ProductionCompany
    {
        [BsonId]
        public int ProductionCompanyId { get; set; }

        [BsonElement("name")]
        public string ProductionCompanyName { get; set; }
    }
}
