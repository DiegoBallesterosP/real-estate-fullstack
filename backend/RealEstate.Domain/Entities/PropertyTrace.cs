using MongoDB.Bson.Serialization.Attributes;

namespace RealEstate.Domain.Entities
{
    public class PropertyTrace
    {
        [BsonId]
        public string? Id { get; set; }
        
        public DateTime DateSale { get; set; }
        public string? Name { get; set; }
        public decimal Value { get; set; }
        public decimal Tax { get; set; }
    }
}