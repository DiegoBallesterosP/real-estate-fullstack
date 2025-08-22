using RealEstate.Application.DTOs;
using RealEstate.Application.Mappers;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Repositories;

namespace RealEstate.Application.UseCases
{
    public class UpdatePropertyUseCase
    {
        private readonly IPropertyRepository _repo;

        public UpdatePropertyUseCase(IPropertyRepository repo)
        {
            _repo = repo;
        }

        public async Task<PropertyDto?> ExecuteAsync(string id, PropertyDto dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return null;

            existing.Name = dto.Name;
            existing.Address = dto.Address;
            existing.Price = dto.Price;
            existing.Images = string.IsNullOrEmpty(dto.Image)
                ? new List<PropertyImage>()
                : new List<PropertyImage> { new PropertyImage { File = dto.Image, Enabled = true } };

            await _repo.UpdateAsync(existing);

            return PropertyMapper.ToDto(existing);
        }
    }
}