using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vertex.Application.Interfaces;

namespace Vertex.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _dashboardService.GetAsync();
            return Ok(result);
        }
    }
}
