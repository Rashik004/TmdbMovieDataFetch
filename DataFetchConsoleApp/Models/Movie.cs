using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using TMDbLib.Objects.General;


namespace ConsoleApplication2.Models
{
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
        public List<Genre> Genres { get; set; }

        [BsonElement("adult")]
        public bool Adult { get; set; }

        //[BsonElement("belongs_to_collection")]
        //public SearchCollection BelongsToCollection { get; set; }

        public List<Rating> MiscRatings { get; set; }

        [BsonElement("budget")]
        public long Budget { get; set; }

        //[BsonElement("changes")]
        //public ChangesContainer Changes { get; set; }

        //[BsonElement("credits")]
        //public Credits Credits { get; set; }

        [BsonElement("homepage")]
        public string Homepage { get; set; }

        [BsonElement("images")]
        public Images Images { get; set; }

        [BsonElement("keywords")]
        public List<Keyword> Keywords { get; set; }

        [BsonElement("overview")]
        public string Overview { get; set; }

        [BsonElement("production_companies")]
        public List<ProductionCompany> ProductionCompanies { get; set; }

        [BsonElement("production_countries")]
        public List<string> ProductionCountries { get; set; }

        [BsonElement("release_date")]
        public DateTime? ReleaseDate { get; set; }

        [BsonElement("release_dates")]
        public List<ReleaseDatesContainer> ReleaseDates { get; set; }

        //[BsonElement("releases")]
        //public Releases Releases { get; set; }

        [BsonElement("revenue")]
        public long Revenue { get; set; }

        //[BsonElement("reviews")]
        //public SearchContainer<ReviewBase> Reviews { get; set; }

        [BsonElement("runtime")]
        public int? Runtime { get; set; }

        //[BsonElement("similar")]
        //public SearchContainer<SearchMovie> Similar { get; set; }

        //[BsonElement("recommendations")]
        //public SearchContainer<SearchMovie> Recommendations { get; set; }

        public TMDbLib.Objects.Movies.Credits Credits { get; set; }

        [BsonElement("spoken_languages")]
        public List<SpokenLanguage> SpokenLanguages { get; set; }

        [BsonElement("status")]
        public string Status { get; set; }

        [BsonElement("tagline")]
        public string Tagline { get; set; }

        public string Awards { get; set; }


        //[BsonElement("translations")]
        //public TranslationsContainer Translations { get; set; }

        [BsonElement("video")]
        public bool Video { get; set; }

        [BsonElement("videos")]
        public List<Video> Videos { get; set; }

        [BsonElement("vote_average")]
        public double TmdbVoteAverage { get; set; }

        public string Rated { get; set; }


    }
}
