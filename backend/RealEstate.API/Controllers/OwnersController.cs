using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.DTOs;
using RealEstate.Application.Mappers;
using RealEstate.Application.UseCases;
using RealEstate.Domain.Repositories;

namespace RealEstate.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OwnersController : ControllerBase
    {
        private readonly GetAllOwnersUseCase _getAll;
        private readonly IOwnerRepository _repo;

        public OwnersController(
            GetAllOwnersUseCase getAll,
            IOwnerRepository repo)
        {
            _getAll = getAll;
            _repo   = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _getAll.ExecuteAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var owner = await _repo.GetByIdAsync(id);
            if (owner == null) return NotFound(new { error = "Dueño no encontrado" });
            return Ok(OwnerMapper.ToDto(owner));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OwnerDto dto)
        {
            dto.IdOwner = Guid.NewGuid().ToString();
            await _repo.AddAsync(OwnerMapper.ToEntity(dto));
            return CreatedAtAction(nameof(GetById), new { id = dto.IdOwner }, dto);
        }
    }
}