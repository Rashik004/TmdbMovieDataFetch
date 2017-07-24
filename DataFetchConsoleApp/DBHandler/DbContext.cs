using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication2.Models.DBSchemas;
using MongoDB.Driver;
using FetchStat = ConsoleApplication2.Models.FetchStat;
using  ConsoleApplication2.Models.DBSchema;
using PersonMovies = ConsoleApplication2.Models.PersonMovies;

namespace ConsoleApplication2.DBHandler
{
    public class DbContext
    {
        public IMongoDatabase Database;
        public IMongoCollection<Movie> Movies;
        public IMongoCollection<MovieGenre> MovieGenres;
        public IMongoCollection<Models.DBSchema.PersonMovies> People;
        public IMongoCollection<FetchStat> FetchStat;
        public IMongoCollection<MovieAlternativeTitles> MovieAlternativeTitles;
        public IMongoCollection<Genre> Genres;
        public IMongoCollection<Credit> Credits;
        public IMongoCollection<FetchLater> FetchLater;

        public DbContext()
        {
            var connectionString = ConfigurationSettings.AppSettings["MongoDBConectionString"];
            var mongoUrl = new MongoUrl(connectionString);
            var client = new MongoClient(mongoUrl);
            var db = client.GetDatabase(mongoUrl.DatabaseName);

            Database = db;
            Movies = db.GetCollection<Movie>("movies");
            MovieGenres = db.GetCollection<MovieGenre>("movieGenres");
            People = db.GetCollection<Models.DBSchema.PersonMovies>("people");
            FetchStat = db.GetCollection<FetchStat>("FetchStat");
            MovieAlternativeTitles = db.GetCollection<MovieAlternativeTitles>("MovieAlternativeTitles");
            Credits = db.GetCollection<Credit>("credits");
            Genres = db.GetCollection<Genre>("genres");
            FetchLater = db.GetCollection<FetchLater>("FetchLater");
            
        }
    }
}
