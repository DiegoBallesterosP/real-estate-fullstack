using RealEstate.Domain.Repositories;

namespace RealEstate.Application.UseCases
{
    public class DeletePropertyUseCase
    {
        private readonly IPropertyRepository _propertyRepository;

        public DeletePropertyUseCase(IPropertyRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }

        public async Task<bool> ExecuteAsync(string id)
        {
            var property = await _propertyRepository.GetByIdAsync(id);
            if (property == null)
                return false;

            await _propertyRepository.DeleteAsync(id);
            return true;
        }
    }
}