using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace B2B.Mddb.Models.Models
{
    public class Images
    {
        [BsonElement("backdrops")]
        public List<ImageData> Backdrops { get; set; }

        [BsonElement("posters")]
        public List<ImageData> Posters { get; set; }
    }

}
