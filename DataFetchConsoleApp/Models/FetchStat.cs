using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace ConsoleApplication2.Models
{
    [BsonIgnoreExtraElements]
    public class FetchStat
    {
        public int LastFetchedPage { get; set; }
    }
}
