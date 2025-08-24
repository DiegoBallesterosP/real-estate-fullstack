using RealEstate.Application.Mappers;
using RealEstate.Domain.Repositories;

namespace RealEstate.Application.UseCases
{
    public class GetPropertyByIdUseCase
    {
        private readonly IPropertyRepository _repository;

        public GetPropertyByIdUseCase(IPropertyRepository repository)
        {
            _repository = repository;
        }

        public async Task<PropertyDto?> ExecuteAsync(string id)
        {
            var property = await _repository.GetByIdAsync(id);
            return property is null ? null : PropertyMapper.ToDto(property);
        }
    }
}