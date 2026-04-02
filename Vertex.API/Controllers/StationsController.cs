using Microsoft.AspNetCore.Mvc;
using Vertex.Application.DTOs;
using Vertex.Application.Interfaces;

namespace Vertex.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StationsController : ControllerBase
    {
        private readonly IStationService _stationService;

        public StationsController(IStationService stationService)
        {
            _stationService = stationService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateStationRequest request)
        {
            try
            {
                var result = await _stationService.CreateAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _stationService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _stationService.GetByIdAsync(id);
                if (result == null)
                    return NotFound("Estação não encontrada");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
