using Microsoft.AspNetCore.Mvc;
using Vertex.Application.DTOs;
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
        public async Task<IActionResult> StartSession([FromBody] StartSessionRequest request)
        {
            try
            {
                var result = await _sessionService.StartSessionAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("end")]
        public async Task<IActionResult> EndSession([FromBody] EndSessionRequest request)
        {
            try
            {
                var result = await _sessionService.EndSessionAsync(request.SessionId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
