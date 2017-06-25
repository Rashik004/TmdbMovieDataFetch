using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication2.Models.OmdbWrapper;

namespace ConsoleApplication2.Models
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
