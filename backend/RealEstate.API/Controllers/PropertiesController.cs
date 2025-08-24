using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.UseCases;

namespace RealEstate.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertiesController : ControllerBase
    {
        private readonly GetPropertiesUseCase _getProperties;
        private readonly GetPropertyByIdUseCase _getPropertyById;

        public PropertiesController(
            GetPropertiesUseCase getProperties,
            GetPropertyByIdUseCase getPropertyById)
        {
            _getProperties = getProperties;
            _getPropertyById = getPropertyById;
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
    }
}