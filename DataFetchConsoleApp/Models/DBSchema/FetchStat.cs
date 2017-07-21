using MongoDB.Bson.Serialization.Attributes;

namespace ConsoleApplication2.Models.DBSchemas
{
    [BsonIgnoreExtraElements]
    public class FetchStat
    {
        public int LastFetchedPage { get; set; }
    }
}
