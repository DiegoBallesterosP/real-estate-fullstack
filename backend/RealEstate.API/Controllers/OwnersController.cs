using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.UseCases;

namespace RealEstate.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OwnersController : ControllerBase
    {
        private readonly GetAllOwnersUseCase _getAll;

        public OwnersController(GetAllOwnersUseCase getAll)
        {
            _getAll = getAll;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _getAll.ExecuteAsync();
            return Ok(list);
        }
    }
}