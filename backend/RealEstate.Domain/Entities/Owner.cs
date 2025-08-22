using MongoDB.Bson.Serialization.Attributes;

namespace RealEstate.Domain.Entities
{
    public class Owner
    {
        [BsonId]
        public string? Id { get; set; }        
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Photo { get; set; }
        public DateTime Birthday { get; set; }
    }
}