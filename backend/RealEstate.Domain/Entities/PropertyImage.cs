using MongoDB.Bson.Serialization.Attributes;

namespace RealEstate.Domain.Entities
{
    public class PropertyImage
    {
        [BsonId]
        public string? Id { get; set; }
        
        public string? File { get; set; }
        public bool Enabled { get; set; }
    }
}