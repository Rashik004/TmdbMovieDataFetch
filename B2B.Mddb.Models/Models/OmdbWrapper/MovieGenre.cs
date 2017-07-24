using System.Collections.Generic;
using B2B.Mddb.Models.Models.DBSchema;
using MongoDB.Bson;

namespace B2B.Mddb.Models.Models.OmdbWrapper
{
    public class MovieGenre
    {
        public ObjectId MovieId { get; set; }

        public IList<Genre> Genres { get; set; }
    }
}
