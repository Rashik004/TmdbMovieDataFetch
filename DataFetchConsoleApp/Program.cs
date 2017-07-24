using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using ConsoleApplication2.DBHandler;
using ConsoleApplication2.Models;
using ConsoleApplication2.Models.DBSchema;
using ConsoleApplication2.Models.DBSchemas;
using MongoDB.Bson;
using MongoDB.Driver;
using TMDbLib.Client;
using TMDbLib.Objects.Movies;
using Newtonsoft.Json;
using TMDbLib.Objects.Discover;
using TMDbLib.Objects.People;
using Cast = TMDbLib.Objects.Movies.Cast;
using Crew = TMDbLib.Objects.General.Crew;
using FetchStat = ConsoleApplication2.Models.FetchStat;
using Movie = ConsoleApplication2.Models.DBSchemas.Movie;
using MovieGenre = ConsoleApplication2.Models.OmdbWrapper.MovieGenre;
using Person = TMDbLib.Objects.People.Person;
using PersonMovies = ConsoleApplication2.Models.PersonMovies;


namespace ConsoleApplication2
{
    class Program
    {
        public static string posterRquestFormat = "p/{0}/{1}";
        public static string OmdbRequestFormat = "?i={0}&plot={1}&apikey={2}";
        static void Main(string[] args)
        {
            //GetDataFromApi();
           // GetDataFromDumbDb();
            FixDbStructure();
        }

        private static void FixDbStructure()
        {
            var dumpCtx = new DbContextPrev();
            var ctx = new MddbBaseModel();

            var tmdbkey = ConfigurationSettings.AppSettings["TmdbKey"];
            var client = new TMDbClient(tmdbkey);
            var test = client.GetMovieCreditsAsync(374430).Result;
            var movies = ctx.Movies.Find(m => true).ToList();
            foreach (var movie in movies)
            {
                var creds = client.GetMovieCreditsAsync(Int32.Parse(movie.TmdbData.MovieId)).Result;
                Credit credit = ConvertToDbo(creds);

            }

        }

        private static Credit ConvertToDbo(Credits arg)
        {
            var credit=new Credit();
            credit.Casts = arg.Cast.Select(ConvertToDbo).ToList();
            return credit;
        }

        private static void GetDataFromDumbDb()
        {
            var dumpCtx=new DbContextPrev();
            var ctx = new MddbBaseModel();
            //var dumpPersonList = dumpCtx.People.Find(p => true).ToList();
            //var dbPersonList = dumpPersonList.Select(ConvertToDbo).ToList();
            //var insert=new List<Models.DBSchema.PersonMovies>();
            //var taken = 0;
            //var gap = 1000;
            //while (taken<dbPersonList.Count)
            //{
            //     insert=dbPersonList.Skip(taken).Take(gap).ToList();
            //    taken += gap;
            //    ctx.People.InsertMany(insert);
            //}
            var movieList = dumpCtx.Movies.Find(m => true).ToList();
            var completed = 0;
            foreach (var movie in movieList)
            {
                AddNewMovie(movie);
                completed++;
                Console.WriteLine(completed.ToString()+" "+DateTime.Now.ToString());
            }

        }

        private static Models.DBSchema.PersonMovies ConvertToDbo(PersonMovies arg)
        {
            return new Models.DBSchema.PersonMovies()
            {
                PersonId= arg.PersonId,
                Name = arg.Person.Name,
                TmdbId = arg.Person.Id,
                Adult = arg.Person.Adult,
                Homepage = arg.Person.Homepage,
                Images = arg.Person.Images,
                AlsoKnownAs = arg.Person.AlsoKnownAs,
                Biography = arg.Person.Biography,
                Birthday = arg.Person.Birthday,
                Deathday = arg.Person.Deathday,
                Gender = arg.Person.Gender,
                ImdbId = arg.Person.ImdbId,
                MovieIds = arg.MovieIds,
                PlaceOfBirth = arg.Person.PlaceOfBirth,
                Popularity = arg.Person.Popularity,
                ProfilePath = arg.Person.ProfilePath
            };
        }

        private static void AddNewMovie(MoviePrev movieDump)
        {
            var movie = ConvertToDbo(movieDump);
            var ctx = new MddbBaseModel();

            if (movieDump.Credits!=null)
            {
                InsertCredits(movieDump, ctx);
            }

            if (movieDump.Genres != null)
            {
                InsertGenreAndMovieGenre(movieDump, ctx);
            }
            ctx.Movies.InsertOneAsync(movie);
        }

        private static void InsertCredits(MoviePrev movieDump, MddbBaseModel ctx)
        {
            var credit = new Credit()
            {
                MovieId = movieDump.MovieId,
                Casts = movieDump.Credits.Cast.Select(ConvertToDbo).ToList(),
                Crew = movieDump.Credits.Crew.Select(ConvertToDbo).ToList()
            };
            
            ctx.Credits.InsertOneAsync(credit);
        }

        private static Models.Crew ConvertToDbo(Crew arg)
        {
            var ctx=new MddbBaseModel();
            var exsisting = GetExisting(arg.Id, ctx);

            return new Models.Crew()
            {
                ProfilePath = arg.ProfilePath,
                PersonId = exsisting != null?exsisting.PersonId:ObjectId.Empty,
                Name = arg.Name,
                Department = arg.Department,
                Job = arg.Job
            };
        }

        private static Models.Cast ConvertToDbo(Cast arg)
        {
            var ctx = new MddbBaseModel();
            var exsisting = GetExisting(arg.Id, ctx);
            return new Models.Cast()
            {
                Character = arg.Character,
                PersonId = exsisting.PersonId,
                Name = arg.Name,
                Order = arg.Order,
                ProfilePath = arg.ProfilePath,
                
            };
        }

        private static Models.DBSchema.PersonMovies GetExisting(int tmdbId, MddbBaseModel ctx)
        {
            var exsisting = ctx.People.Find(p => p.TmdbId == tmdbId).FirstOrDefault();
            if (exsisting != null) return exsisting;
            exsisting = new Models.DBSchema.PersonMovies()
            {
                PersonId = ObjectId.GenerateNewId()
            };
            var fetchLater = new FetchLater()
            {
                EntityObjectId = exsisting.PersonId,
                EntityType = EntityType.Person,
                EntityTmdbId = tmdbId
            };
            ctx.FetchLater.InsertOneAsync(fetchLater);
            return exsisting;
        }

        private static void InsertGenreAndMovieGenre(MoviePrev movieDump, MddbBaseModel ctx)
        {
            var dbGenres = new List<Genre>();
            foreach (var genre in movieDump.Genres)
            {
                var exsisting = ctx.Genres.Find(g => g.TmdbId == genre.Id).FirstOrDefault();
                //Genre exsisting = new Genre();
                if (exsisting == null)
                {
                    exsisting = new Genre()
                    {
                        GenreId = ObjectId.GenerateNewId(),
                        TmdbId = genre.Id,
                        GenreName = genre.Name
                    };
                    ctx.Genres.InsertOneAsync(exsisting);
                }

                dbGenres.Add(new Genre()
                {
                    TmdbId = genre.Id,
                    GenreName = genre.Name,
                    GenreId = exsisting.GenreId
                });
            }
            var movieGenres = new Models.DBSchema.MovieGenre()
            {
                MovieId = movieDump.MovieId,
                Genres = dbGenres
            };
            ctx.MovieGenres.InsertOneAsync(movieGenres);
        }

        private static Movie ConvertToDbo(MoviePrev movie)
        {
            return new Movie
            {
                Genres = movie.Genres,
                Adult = movie.Adult,
                AlternativeTitles = movie.AlternativeTitles,
                Budget = movie.Budget,
                Homepage = movie.Homepage,
                ImdbData = movie.ImdbData,
                TmdbData = movie.TmdbData,
                Images = movie.Images,
                MovieId = movie.MovieId,
                OriginalLanguage = movie.OriginalLanguage,
                OriginalTitle = movie.OriginalTitle,
                Overview = movie.Overview,
                Rated = movie.Rated,
                ReleaseDate = movie.ReleaseDate,
                Revenue = movie.Revenue,
                Runtime = movie.Runtime,
                SpokenLanguages = movie.SpokenLanguages,
                Status = movie.Status,
                Tagline = movie.Tagline,
                Title = movie.Title,
                TmdbVoteAverage = movie.TmdbVoteAverage
            };
        }
        private static void GetDataFromApi()
        {
            var tmdbkey = ConfigurationSettings.AppSettings["TmdbKey"];
            var client = new TMDbClient(tmdbkey);
            var asd=client.DiscoverMoviesAsync().IncludeAdultMovies().OrderBy(DiscoverMovieSortBy.ReleaseDateDesc).Query().Result;
            var maxCount = 30;
            var startTime = DateTime.Now;
            var db = new DbContextPrev();
            var offsetpageNo = db.FetchStat.Find(a => true).FirstOrDefault().LastFetchedPage;
            var topMovies = new List<MoviePrev>();
            double duration;
            try
            {
                topMovies = FetchTopMovies(offsetpageNo, maxCount, client);
                UpdateFetchStat(maxCount);
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
                //db.Movies.InsertMany(topMovies);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }
            //var topMovies = db.Movies.Find(m => true).ToListAsync().Result;
            var movieCasts = new List<MoviePerson>();
            foreach (var top in topMovies)
            {
                movieCasts.AddRange(top.Credits.Cast.Select(c => new MoviePerson()
                {
                    MovieId = top.MovieId,
                    PersonId = c.Id
                }));
                //break;
            }
            movieCasts = movieCasts.Distinct().ToList();
            startTime = DateTime.Now;
            InsertPerson(movieCasts, client, db);
            duration = DateTime.Now.Subtract(startTime).TotalSeconds;
            Console.WriteLine("\n************--------***********");
            Console.WriteLine(String.Format("{0} Added {1} Person's data {2} seconds", DateTime.Now.ToString(),
                movieCasts.Count,
                duration));
            Console.WriteLine("\n************--------***********\n\n");
            Console.ReadKey();
        }

        private static void UpdateFetchStat(int maxCount)
        {
            var db=new DbContextPrev();
            //db.FetchStat.InsertOne(new FetchStat() {LastFetchedPage = 2});
            var filterBuilder = Builders<FetchStat>.Filter;
            var filter = filterBuilder.Where(s => true);
            var update = Builders<FetchStat>.Update.Inc("LastFetchedPage", maxCount);
            db.FetchStat.FindOneAndUpdate(filter, update);
        }

        private static List<TMDbLib.Objects.People.Person> InsertPerson(List<MoviePerson> movieCasts, TMDbClient client, DbContextPrev db)
        {
            var people = new List<Person>();
            int count = 0;
            var a = DateTime.Now;
            try
            {
                DateTime intervalStart=DateTime.Now;
                foreach (var movieCast in movieCasts)
                {
                    if (count != 0 && count%100 == 0)
                    {
                        Console.WriteLine(String.Format("****Time taken to insert last 100 data {0} seconds", DateTime.Now.Subtract(intervalStart).TotalSeconds));
                        Console.WriteLine(String.Format("****Time taken to insert {0} data {1} seconds",count, DateTime.Now.Subtract(a).TotalSeconds));
                        intervalStart=DateTime.Now;
                    }
                        
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


        private static List<MoviePrev> FetchTopMovies(int offsetPage, int totalpageCount, TMDbClient client)
        {
            offsetPage = 0;
            var totalCount = 0;
            var currentPageNo = offsetPage+1;
            var lastPageNo = totalpageCount + offsetPage;
            //var lastPage = 1;
            var topMovies = new List<MoviePrev>();

            while (currentPageNo <= lastPageNo)
            {
                var mvList = client.GetMovieTopRatedListAsync(null, currentPageNo++);
                var currentPage = new List<MoviePrev>();

                Console.WriteLine(String.Format("Fetching data of page no {0}", currentPageNo - 1));
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

                        currentPage.Add(topMovie);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(String.Format("Exception For movie {0}\n", movie.Title) + ex);
                    }

                }
                topMovies.AddRange(currentPage);
                var ctx = new DbContextPrev();
                ctx.Movies.InsertManyAsync(currentPage);
                Console.WriteLine("Total Movies Saved {0}", totalCount);
            }
            return topMovies;
        }


        private static OmdbMovie FetchOmdbData(string imdbId, string omdbKey)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://www.omdbapi.com/");
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
                    return JsonConvert.DeserializeObject<OmdbMovie>(omdbresponse);


                }
                return null;
            }
        }

        private string DownloadPoster(string baseUrl, string posterPath)
        {

            return null;
        }

        private static MoviePrev ConvertToDbo(TMDbLib.Objects.Movies.Movie arg, OmdbMovie omdbData)
        {
            var movie= new MoviePrev()
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
