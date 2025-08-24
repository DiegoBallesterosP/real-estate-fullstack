using RealEstate.Application.Mappers;
using RealEstate.Domain.Repositories;

public class GetPropertiesUseCase
{
    private readonly IPropertyRepository _propertyRepository;

    public GetPropertiesUseCase(IPropertyRepository propertyRepository)
    {
        _propertyRepository = propertyRepository;
    }

    public async Task<IEnumerable<PropertyDto>> ExecuteAsync(PropertyFilterDto? filter)
    {
        var properties = await _propertyRepository.GetFilteredAsync(
            filter?.Name,
            filter?.Address,
            filter?.MinPrice,
            filter?.MaxPrice
        );

        return properties.Select(PropertyMapper.ToDto);
    }
}