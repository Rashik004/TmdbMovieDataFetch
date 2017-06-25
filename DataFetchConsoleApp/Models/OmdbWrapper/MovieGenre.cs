using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using TMDbLib.Objects.General;

namespace ConsoleApplication2.Models.OmdbWrapper
{
    public class MovieGenre
    {
        public ObjectId MovieId { get; set; }

        public IList<Genre> Genres { get; set; }
    }
}
