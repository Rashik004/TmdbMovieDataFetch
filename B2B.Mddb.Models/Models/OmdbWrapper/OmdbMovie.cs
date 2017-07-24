using System.Collections.Generic;

namespace B2B.Mddb.Models.Models.OmdbWrapper
{
    public class OmdbMovie
    {
        public string Rated { get; set; }

        public string Director { get; set; }

        public string Awards { get; set; }

        public string imdbRating { get; set; }

        public string imdbVotes { get; set; }


        public List<OmdbRating> Ratings { get; set; }

    }
}
