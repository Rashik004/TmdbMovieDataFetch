using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace B2B.Mddb.Models.Models
{
    public class ReleaseDatesContainer
    {
        /// <summary>
        /// A country code, e.g. US
        /// </summary>
        [BsonElement("iso_3166_1")]
        public string Iso_3166_1 { get; set; }

        [BsonElement("release_dates")]
        public List<ReleaseDateItem> ReleaseDates { get; set; }
    }

}
