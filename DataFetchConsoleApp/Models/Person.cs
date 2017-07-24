using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using TMDbLib.Helpers;
using TMDbLib.Objects.Changes;
using TMDbLib.Objects.General;
using TMDbLib.Objects.People;

namespace ConsoleApplication2.Models
{
    public class Person
    {
        [BsonElement("adult")]
        public bool Adult { get; set; }

        [BsonElement("also_known_as")]
        public List<string> AlsoKnownAs { get; set; }

        [BsonElement("biography")]
        public string Biography { get; set; }

        [BsonElement("birthday")]
        [JsonConverter(typeof(TmdbPartialDateConverter))]
        public DateTime? Birthday { get; set; }

        [BsonElement("changes")]
        public ChangesContainer Changes { get; set; }

        [BsonElement("deathday")]
        [JsonConverter(typeof(TmdbPartialDateConverter))]
        public DateTime? Deathday { get; set; }

        [BsonElement("gender")]
        public PersonGender Gender { get; set; }

        [BsonElement("homepage")]
        public string Homepage { get; set; }

        [BsonId]
        public ObjectId PersonId { get; set; }

        [BsonElement("images")]
        public ProfileImages Images { get; set; }

        [BsonElement("imdb_id")]
        public string ImdbId { get; set; }

        [BsonElement("tmdb_id")]
        public int TmdbId { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("place_of_birth")]
        public string PlaceOfBirth { get; set; }

        [BsonElement("popularity")]
        public double Popularity { get; set; }

        [BsonElement("profile_path")]
        public string ProfilePath { get; set; }

    }
}
