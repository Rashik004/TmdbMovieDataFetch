using MongoDB.Bson.Serialization.Attributes;

namespace B2B.Mddb.Models.Models
{
    public class AlternativeTitle
    {
        /// <summary>
        /// A country code, e.g. US
        /// </summary>
        [BsonElement("iso_3166_1")]
        public string Iso_3166_1 { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }
    }
}
