using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2.Models
{
    public class VendorMovieData
    {
        public string MovieId { get; set; }

        public string BackdropPath { get; set; }

        public string PosterPath { get; set; }

        public Rating Rating { get; set; }

    }
}
