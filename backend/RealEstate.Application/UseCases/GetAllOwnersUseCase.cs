using RealEstate.Application.DTOs;
using RealEstate.Application.Mappers;
using RealEstate.Domain.Repositories;


namespace RealEstate.Application.UseCases
{
    public class GetAllOwnersUseCase
    {
        private readonly IOwnerRepository _repo;

        public GetAllOwnersUseCase(IOwnerRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<OwnerDto>> ExecuteAsync()
        {
            var owners = await _repo.GetAllAsync();
            return owners.Select(OwnerMapper.ToDto);
        }
    }
}