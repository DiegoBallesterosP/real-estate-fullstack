using MongoDB.Driver;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Repositories;
using RealEstate.Infrastructure.Persistence;

namespace RealEstate.Infrastructure.Repositories
{
    public class MongoPropertyRepository : IPropertyRepository
    {
        private readonly IMongoCollection<Property> _properties;

        public MongoPropertyRepository(MongoDbContext context)
        {
            _properties = context.Properties;
        }

        public async Task<IEnumerable<Property>> GetFilteredAsync(string? name, string? address, decimal? minPrice, decimal? maxPrice)
        {
            var builder = Builders<Property>.Filter;
            var filters = new List<FilterDefinition<Property>>();

            if (!string.IsNullOrWhiteSpace(name))
                filters.Add(builder.Regex(p => p.Name, new MongoDB.Bson.BsonRegularExpression(name, "i")));

            if (!string.IsNullOrWhiteSpace(address))
                filters.Add(builder.Regex(p => p.Address, new MongoDB.Bson.BsonRegularExpression(address, "i")));

            if (minPrice.HasValue)
                filters.Add(builder.Gte(p => p.Price, minPrice.Value));

            if (maxPrice.HasValue)
                filters.Add(builder.Lte(p => p.Price, maxPrice.Value));

            var finalFilter = filters.Count > 0 ? builder.And(filters) : FilterDefinition<Property>.Empty;
            return await _properties.Find(finalFilter).ToListAsync();
        }

        public async Task<Property?> GetByIdAsync(string id) =>
            await _properties.Find(p => p.Id == id).FirstOrDefaultAsync();

        public async Task AddAsync(Property property) =>
            await _properties.InsertOneAsync(property);

        public async Task UpdateAsync(Property property) =>
            await _properties.ReplaceOneAsync(p => p.Id == property.Id, property);

        public async Task DeleteAsync(string id) =>
            await _properties.DeleteOneAsync(p => p.Id == id);
    }
}