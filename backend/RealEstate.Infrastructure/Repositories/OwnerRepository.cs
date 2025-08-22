using RealEstate.Domain.Entities;
using RealEstate.Domain.Repositories;
using RealEstate.Infrastructure.Persistence;
using MongoDB.Driver;

namespace RealEstate.Infrastructure.Repositories
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly IMongoCollection<Owner> _owners;

        public OwnerRepository(MongoDbContext context)
        {
            _owners = context.Owners;
        }

        public async Task<Owner?> GetByIdAsync(string id) =>
            await _owners.Find(o => o.Id == id).FirstOrDefaultAsync();

        public async Task<IEnumerable<Owner>> GetAllAsync() =>
            await _owners.Find(_ => true).ToListAsync();

        public async Task AddAsync(Owner owner) =>
            await _owners.InsertOneAsync(owner);

        public async Task UpdateAsync(Owner owner) =>
            await _owners.ReplaceOneAsync(o => o.Id == owner.Id, owner);

        public async Task DeleteAsync(string id) =>
            await _owners.DeleteOneAsync(o => o.Id == id);
    }
}