using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication2.Models;
using ConsoleApplication2.Models.OmdbWrapper;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using TMDbLib.Objects.People;

namespace ConsoleApplication2.DBHandler
{
    public class DbContext
    {
        public IMongoDatabase Database;
        public IMongoCollection<Movie> Movies;
        public IMongoCollection<MovieGenre> Genres; 
        public IMongoCollection<PersonMovies> People; 
        public IMongoCollection<FetchStat> FetchStat;

        public DbContext()
        {

            var connectionString = ConfigurationSettings.AppSettings["MongoDBConectionString"];
            var mongoUrl = new MongoUrl(connectionString);
            var client = new MongoClient(mongoUrl);
            var db = client.GetDatabase(mongoUrl.DatabaseName);
            Database = db;
            Movies= db.GetCollection<Movie>("Movie");
            Genres = db.GetCollection<MovieGenre>("Genres");
            People = db.GetCollection<PersonMovies>("People");
            FetchStat = db.GetCollection<FetchStat>("FetchStat");

        }

    }
}
