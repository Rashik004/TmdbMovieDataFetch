using MongoDB.Bson;
using Newtonsoft.Json;

namespace B2B.Mddb.Models.Models
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
