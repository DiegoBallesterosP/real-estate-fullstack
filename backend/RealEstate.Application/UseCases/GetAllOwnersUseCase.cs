using RealEstate.Application.DTOs;
using RealEstate.Application.Mappers;
using RealEstate.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace RealEstate.Application.UseCases
{
    public class GetAllOwnersUseCase
    {
        private readonly IOwnerRepository _repo;
        private readonly ILogger<GetAllOwnersUseCase> _logger;

        public GetAllOwnersUseCase(IOwnerRepository repo, ILogger<GetAllOwnersUseCase> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<IEnumerable<OwnerDto>> ExecuteAsync()
        {
            var owners = await _repo.GetAllAsync();

            if (owners == null)
            {
                _logger.LogWarning("GetAllOwnersUseCase: El repositorio devolvió NULL.");
                return Enumerable.Empty<OwnerDto>();
            }

            var ownersList = owners.ToList();
            _logger.LogInformation("GetAllOwnersUseCase: Se obtuvieron {Count} owners.", ownersList.Count);

            return ownersList.Select(OwnerMapper.ToDto);
        }
    }
}