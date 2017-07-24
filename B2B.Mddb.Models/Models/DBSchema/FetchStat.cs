using MongoDB.Bson.Serialization.Attributes;

namespace B2B.Mddb.Models.Models.DBSchema
{
    [BsonIgnoreExtraElements]
    public class FetchStat
    {
        public int LastFetchedPage { get; set; }
    }
}
