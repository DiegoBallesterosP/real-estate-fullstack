using RealEstate.Domain.Entities;

namespace RealEstate.Domain.Repositories
{
    public interface IPropertyRepository
    {
        Task<IEnumerable<Property>> GetFilteredAsync(string? name, string? address, decimal? minPrice, decimal? maxPrice);
        Task<Property?> GetByIdAsync(string id);
        Task AddAsync(Property property);
        Task UpdateAsync(Property property);
        Task DeleteAsync(string id);
    }
}
