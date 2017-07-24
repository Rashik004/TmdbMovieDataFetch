using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using TMDbLib.Objects.People;

namespace ConsoleApplication2.Models
{
    public class Crew
    {

        [JsonProperty("department")]
        public string Department { get; set; }

        [JsonProperty("person_id")]
        public ObjectId PersonId { get; set; }

        [JsonProperty("job")]
        public string Job { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("profile_path")]
        public string ProfilePath { get; set; }
    }
}
