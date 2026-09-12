using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vertex.Application.Tariffing.Commands.CalcularTarifacao;

namespace Vertex.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/v1/tarifacao")]
    public class TariffingController : ControllerBase
    {
        private readonly CalcularTarifacaoHandler _handler;

        public TariffingController(CalcularTarifacaoHandler handler)
        {
            _handler = handler;
        }

        [HttpPost("calcular")]
        public async Task<ActionResult<CalcularTarifacaoResponse>> Calcular(
            [FromBody] CalcularTarifacaoCommand command,
            CancellationToken cancellationToken)
        {
            try
            {
                var response = await _handler.HandleAsync(
                    command,
                    cancellationToken);

                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    mensagem = ex.Message
                });
            }
            catch (InvalidOperationException ex)
            {
                return UnprocessableEntity(new
                {
                    mensagem = ex.Message
                });
            }
        }
    }
}
