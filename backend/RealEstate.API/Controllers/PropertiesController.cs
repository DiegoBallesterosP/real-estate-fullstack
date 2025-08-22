using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.DTOs;
using RealEstate.Application.UseCases;

namespace RealEstate.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertiesController : ControllerBase
    {
        private readonly GetPropertiesUseCase _getProperties;
        private readonly GetPropertyByIdUseCase _getPropertyById;
        private readonly CreatePropertyUseCase _createProperty;
        private readonly UpdatePropertyUseCase _updateProperty;
        private readonly DeletePropertyUseCase _deleteProperty;

        public PropertiesController(
            GetPropertiesUseCase getProperties,
            GetPropertyByIdUseCase getPropertyById,
            CreatePropertyUseCase createProperty,
            UpdatePropertyUseCase updateProperty,
            DeletePropertyUseCase deleteProperty)
        {
            _getProperties = getProperties;
            _getPropertyById = getPropertyById;
            _createProperty = createProperty;
            _updateProperty = updateProperty;
            _deleteProperty = deleteProperty;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PropertyFilterDto filters)
        {
            try
            {
                var properties = await _getProperties.ExecuteAsync(filters);
                return Ok(properties);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error al obtener propiedades: {ex.Message}" });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var property = await _getPropertyById.ExecuteAsync(id);
            return property == null
                ? NotFound(new { message = "Propiedad no encontrada" })
                : Ok(property);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PropertyDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Name) || dto.Price <= 0)
                return BadRequest(new { message = "Datos inválidos" });

            var created = await _createProperty.ExecuteAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] PropertyDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Name) || dto.Price <= 0)
                return BadRequest(new { message = "Datos inválidos" });

            var updated = await _updateProperty.ExecuteAsync(id, dto);
            return updated == null
                ? NotFound(new { message = "Propiedad no encontrada" })
                : Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var deleted = await _deleteProperty.ExecuteAsync(id);

                return deleted
                    ? NoContent()
                    : NotFound(new { message = "Propiedad no encontrada" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error al eliminar propiedad: {ex.Message}" });
            }
        }
    }
}