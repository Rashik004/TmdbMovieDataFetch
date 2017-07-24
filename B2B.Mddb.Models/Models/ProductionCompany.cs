using MongoDB.Bson.Serialization.Attributes;

namespace B2B.Mddb.Models.Models
{
    public class ProductionCompany
    {
        [BsonId]
        public int ProductionCompanyId { get; set; }

        [BsonElement("name")]
        public string ProductionCompanyName { get; set; }
    }
}
