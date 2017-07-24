using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TMDbLib.Objects.General;

namespace ConsoleApplication2.Models.DBSchemas
{
    [BsonIgnoreExtraElements]
    public class Movie
    {
        [BsonId]
        public ObjectId MovieId { get; set; }

        [BsonElement("tmdb_data")]
        public VendorMovieData TmdbData { get; set; }

        [BsonElement("imdb_data")]
        public VendorMovieData ImdbData { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("original_title")]
        public string OriginalTitle { get; set; }

        [BsonElement("original_language")]
        public string OriginalLanguage { get; set; }

        [BsonElement("alternative_titles")]
        public IList<AlternativeTitle> AlternativeTitles { get; set; }

        [BsonElement("genres")]
        public List<TMDbLib.Objects.General.Genre> Genres { get; set; }

        [BsonElement("adult")]
        public bool Adult { get; set; }

        [BsonElement("budget")]
        public long Budget { get; set; }

        [BsonElement("homepage")]
        public string Homepage { get; set; }

        [BsonElement("images")]
        public Images Images { get; set; }

        [BsonElement("overview")]
        public string Overview { get; set; }

        [BsonElement("release_date")]
        public DateTime? ReleaseDate { get; set; }

        [BsonElement("revenue")]
        public long Revenue { get; set; }

        [BsonElement("runtime")]
        public int? Runtime { get; set; }

        [BsonElement("spoken_languages")]
        public List<SpokenLanguage> SpokenLanguages { get; set; }

        [BsonElement("status")]
        public string Status { get; set; }

        [BsonElement("tagline")]
        public string Tagline { get; set; }

        [BsonElement("vote_average")]
        public double TmdbVoteAverage { get; set; }

        public string Rated { get; set; }

    }
}
