using Microsoft.AspNetCore.Mvc;
using Vertex.Application.Interfaces;

namespace Vertex.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SessionsController : ControllerBase
    {
        private readonly ISessionService _sessionService;

        public SessionsController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        [HttpPost("start")]
        public async Task<IActionResult> StartSession(int customerId, int stationId)
        {
            try
            {
                var result = await _sessionService.StartSessionAsync(customerId, stationId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("end")]
        public async Task<IActionResult> EndSession(int sessionId)
        {
            try
            {
                await _sessionService.EndSessionAsync(sessionId);
                return Ok(new { Message = "Sessão finalizada com sucesso." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
