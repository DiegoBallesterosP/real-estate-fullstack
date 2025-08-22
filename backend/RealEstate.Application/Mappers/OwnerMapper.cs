using RealEstate.Application.DTOs;
using RealEstate.Domain.Entities;

namespace RealEstate.Application.Mappers
{
    public static class OwnerMapper
    {
        public static OwnerDto ToDto(Owner owner) => new()
        {
            IdOwner = owner.Id,
            Name    = owner.Name,
            Address = owner.Address,
            Photo   = owner.Photo,
            Birthday= owner.Birthday
        };

        public static Owner ToEntity(OwnerDto dto) => new()
        {
            Id = dto.IdOwner,
            Name    = dto.Name,
            Address = dto.Address,
            Photo   = dto.Photo,
            Birthday= dto.Birthday
        };
    }
}