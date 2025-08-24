using RealEstate.Domain.Entities;

namespace RealEstate.Application.Mappers
{
    public static class PropertyMapper
    {
        public static PropertyDto ToDto(Property property)
        {
            return new PropertyDto
            {
                Id = property.Id,
                IdOwner = property.OwnerId,
                Name = property.Name,
                Address = property.Address,
                Price = property.Price,
                Image = property.Images.FirstOrDefault(i => i.Enabled)?.File,
                Traces = (property.Traces ?? new List<PropertyTrace>())
                    .Select(t => new PropertyTraceDto
                    {
                        Id = t.Id,
                        DateSale = t.DateSale,
                        Name = t.Name,
                        Value = t.Value,
                        Tax = t.Tax
                    })
                    .ToList()
            };
        }


        public static Property ToEntity(PropertyDto dto)
        {
            return new Property
            {
                Id = Guid.NewGuid().ToString(),
                OwnerId = dto.IdOwner,
                Name = dto.Name,
                Address = dto.Address,
                Price = dto.Price,
                Images = string.IsNullOrEmpty(dto.Image)
                    ? new List<PropertyImage>()
                    : new List<PropertyImage> { new PropertyImage { File = dto.Image, Enabled = true } }
            };
        }
    }
}