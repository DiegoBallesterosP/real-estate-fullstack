using RealEstate.Application.DTOs;
using RealEstate.Application.Mappers;
using RealEstate.Domain.Repositories;

namespace RealEstate.Application.UseCases
{
    public class CreatePropertyUseCase
    {
        private readonly IPropertyRepository _propertyRepo;
        private readonly IOwnerRepository _ownerRepo;

        public CreatePropertyUseCase(IPropertyRepository propertyRepo, IOwnerRepository ownerRepo)
        {
            _propertyRepo = propertyRepo;
            _ownerRepo = ownerRepo;
        }

        public async Task<PropertyDto> ExecuteAsync(PropertyDto dto)
        {
            var ownerExists = await _ownerRepo.GetByIdAsync(dto.IdOwner);
            if (ownerExists == null)
                throw new Exception($"El owner con ID {dto.IdOwner} no existe");

            var property = PropertyMapper.ToEntity(dto);
            await _propertyRepo.AddAsync(property);
            return PropertyMapper.ToDto(property);
        }
    }
}