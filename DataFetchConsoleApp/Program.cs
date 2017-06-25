using System;
using System.Collections.Generic;
using System.Configuration;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication2.DBHandler;
using ConsoleApplication2.Models;
using ConsoleApplication2.Models.OmdbWrapper;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Linq;
using TMDbLib.Client;
using TMDbLib.Objects.Lists;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Search;
using Movie = ConsoleApplication2.Models.Movie;
using Newtonsoft.Json;
using TMDbLib.Objects.People;


namespace ConsoleApplication2
{
    class Program
    {
        public static string posterRquestFormat = "p/{0}/{1}";
        public static string OmdbRequestFormat = "?i={0}&plot={1}&apikey={2}";
        static void Main(string[] args)
        {
            //FetchImdbData("tt0111161", "6f27242");
            //return;
            var tmdbkey= ConfigurationSettings.AppSettings["TmdbKey"];
            var client = new TMDbClient(tmdbkey);

            var maxCount = 1000;
            var startTime = DateTime.Now;
            var db = new DbContext();
            var topMovies = new List<Movie>();
            double duration;
            try
            {
                topMovies = FetchTopMovies(maxCount, client);
                var genres = topMovies.Select(tm => new MovieGenre()
                {
                    Genres = tm.Genres,
                    MovieId = tm.MovieId
                });
                duration = DateTime.Now.Subtract(startTime).TotalSeconds;
                Console.WriteLine("\n************--------***********");
                Console.WriteLine(String.Format("{0} Added {1} movie Data in {2} seconds", DateTime.Now.ToString(), maxCount,
                    duration));
                Console.WriteLine("\n************--------***********\n\n");

                db.Genres.InsertMany(genres);
                db.Movies.InsertMany(topMovies);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }
            //var topMovies = db.Movies.Find(m => true).ToListAsync().Result;
            var movieCasts = new List<MoviePerson>();
            foreach (var top in topMovies)
            {
                movieCasts.AddRange(top.Credits.Cast.Select(c=>new MoviePerson()
                {
                    MovieId = top.MovieId,
                    PersonId = c.Id
                }));
                //break;
            }
            movieCasts = movieCasts.Distinct().ToList();
            startTime=DateTime.Now;
            InsertPerson(movieCasts, client, db);
            duration = DateTime.Now.Subtract(startTime).TotalSeconds;
            Console.WriteLine("\n************--------***********");
            Console.WriteLine(String.Format("{0} Added {1} Person's data {2} seconds", DateTime.Now.ToString(),
                movieCasts.Count,
                duration));
            Console.WriteLine("\n************--------***********\n\n");
            Console.ReadKey();
        }

        private static List<Person> InsertPerson(List<MoviePerson> movieCasts, TMDbClient client, DbContext db)
        {
            var people = new List<Person>();
            int count = 0;
            var a = DateTime.Now;
            a.ToString();
            try
            {
                foreach (var movieCast in movieCasts)
                {
                    //var current= db.People.Find(p => p.Id == person.Id).FirstOrDefault();
                    var dbPerson = db.People.Find(p => p.Person.Id == movieCast.PersonId).FirstOrDefault();
                    if (dbPerson == null)
                    {
                        var person = client.GetPersonAsync(movieCast.PersonId).Result;
                        Console.WriteLine(String.Format("{0}. {1} Inserting data of {2}", ++count, DateTime.Now.ToString(), person.Name ));
                        db.People.InsertOne(new PersonMovies()
                        {
                            Person = person,
                            MovieIds = new List<ObjectId>() { movieCast.MovieId}
                        });
                    }
                    else
                    {
                        Console.WriteLine(String.Format("{0}. {1} Updating data of {2}", ++count, DateTime.Now.ToString(), dbPerson.Person.Name));
                        var filterBuilder = Builders<PersonMovies>.Filter;
                        var filter = filterBuilder.Eq("_id", dbPerson.PersonId) & !filterBuilder.AnyEq("MovieIds", movieCast.MovieId);
                        var update = Builders<PersonMovies>.Update.Push("MovieIds", movieCast.MovieId);
                        db.People.FindOneAndUpdateAsync(filter, update);
                    }
                }

            }
            catch (Exception ex)
            {
                throw;
            }
            return people;
        }

        private static OmdbMovie FetchOmdbData(string imdbId, string omdbKey)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress= new Uri("http://www.omdbapi.com/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res;
                var requestString = String.Format(OmdbRequestFormat, imdbId, "full", omdbKey);
                try
                {
                    res = client.GetAsync(requestString).Result;
                }
                catch (Exception ex)
                {

                    throw;
                }
                if (res.IsSuccessStatusCode)
                {
                    var omdbresponse = res.Content.ReadAsStringAsync().Result;
                    return  JsonConvert.DeserializeObject<OmdbMovie>(omdbresponse);


                }
                return null;
            }
        }

        private static List<Movie> FetchTopMovies(int maxCount, TMDbClient client)
        {
            var totalCount = 0;
            var lastPage = 1;
            var topMovies = new List<Movie>();

            while (totalCount < maxCount)
            {
                var mvList = client.GetMovieTopRatedListAsync(null, lastPage++);
                Console.WriteLine(String.Format("Fetching data of page no {0}", lastPage-1));
                if (mvList.Result.Results.Count == 0)
                    break;
                foreach (var movie in mvList.Result.Results)
                {
                    totalCount++;
                    try
                    {
                        Console.WriteLine(string.Format("\t{0}. {1} Fetching data of movie {2}-{3}"
                                        , totalCount,
                                        DateTime.Now.ToString(),
                                        movie.Title,
                                        movie.ReleaseDate.HasValue ? movie.ReleaseDate.Value.Year.ToString() : "Unknown Year"));

                        var moveieDetails = client.GetMovieAsync(movie.Id,
                            MovieMethods.AlternativeTitles |
                            MovieMethods.Keywords |
                            //MovieMethods.ReleaseDates |
                            MovieMethods.Credits).Result;


                        OmdbMovie omdbData = null;
                        var topMovie = ConvertToDbo(moveieDetails, omdbData);

                        topMovies.Add(topMovie);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(String.Format("Exception For movie {0}\n", movie.Title) + ex);
                    }
                }
                Console.WriteLine("Total Movies Saved {0}", totalCount);
            }
            return topMovies;
        }

        private string DownloadPoster(string baseUrl, string posterPath)
        {

            return null;
        }

        private static Movie ConvertToDbo(TMDbLib.Objects.Movies.Movie arg, OmdbMovie omdbData)
        {
            var movie= new Movie()
            {
                MovieId = ObjectId.GenerateNewId(),
                TmdbData = new VendorMovieData()
                {
                    MovieId = arg.Id.ToString(),
                    BackdropPath = arg.BackdropPath,
                    PosterPath = arg.PosterPath,
                    Rating = new Rating()
                    {
                        DataSourceName = "The Movie Database",
                        AveragePoint = arg.VoteAverage,
                        FullPoint = 10,
                        TotalVote = arg.VoteCount
                    }
                },
                ImdbData = new VendorMovieData()
                {
                    MovieId = arg.ImdbId,
                },
                Credits = arg.Credits,
                Adult = arg.Adult,
                Budget = arg.Budget,
                Homepage = arg.Homepage,
                OriginalLanguage = arg.OriginalLanguage,
                OriginalTitle = arg.OriginalTitle,
                Overview = arg.Overview,
                ReleaseDate = arg.ReleaseDate,
                Revenue = arg.Revenue,
                Runtime = arg.Runtime,
                Status = arg.Status,
                Tagline = arg.Tagline,
                Title = arg.Title,
                Video = arg.Video,
                Genres = arg.Genres
            };

            if (omdbData!=null)
            {
                movie.ImdbData.
                    Rating = new Rating()
                    {
                        AveragePoint = double.Parse(omdbData.imdbRating),
                        DataSourceName = "Internet Movie Database",
                        FullPoint = 10,
                        TotalVote = int.Parse(omdbData.imdbVotes.Trim(','))
                    };
                movie.Awards = omdbData.Awards;
            }

            if (arg.AlternativeTitles!=null)
            {
                movie.AlternativeTitles = new List<AlternativeTitle>();
                foreach (var title in arg.AlternativeTitles.Titles)
                {
                    movie.AlternativeTitles.Add(ConvertToDbo(title));
                } 
            }

            if (arg.Keywords!=null)
            {
                movie.Keywords = new List<Keyword>();
                foreach (var keyword in arg.Keywords.Keywords)
                {
                    movie.Keywords.Add(ConvertToDbo(keyword));
                } 
            }

            return movie;
        }

        private static Keyword ConvertToDbo(TMDbLib.Objects.General.Keyword arg)
        {
            return new Keyword()
            {
                KeywordId= arg.Id,
                Name = arg.Name
            };
        }

        private static AlternativeTitle ConvertToDbo(TMDbLib.Objects.General.AlternativeTitle arg)
        {
            return new AlternativeTitle()
            {
                Title = arg.Title,
                Iso_3166_1 = arg.Iso_3166_1
            };
        }
    }
}
