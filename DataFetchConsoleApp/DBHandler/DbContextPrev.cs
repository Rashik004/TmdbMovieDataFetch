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
    public class DbContextPrev
    {
        public IMongoDatabase Database;
        public IMongoCollection<MoviePrev> Movies;
        public IMongoCollection<MovieGenre> Genres; 
        public IMongoCollection<PersonMovies> People; 
        public IMongoCollection<FetchStat> FetchStat;

        public DbContextPrev()
        {

            var connectionString = ConfigurationSettings.AppSettings["MongoDumpDBConectionString"];
            var mongoUrl = new MongoUrl(connectionString);
            var client = new MongoClient(mongoUrl);
            var db = client.GetDatabase(mongoUrl.DatabaseName);
            Database = db;
            Movies= db.GetCollection<MoviePrev>("Movie");
            Genres = db.GetCollection<MovieGenre>("Genres");
            People = db.GetCollection<PersonMovies>("People");
            FetchStat = db.GetCollection<FetchStat>("FetchStat");

        }

    }
}
