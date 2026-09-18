using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vertex.Application.Wallet.Commands.RecarregarCarteira;
using Vertex.Domain.Enums;

namespace Vertex.Api.Controllers
{
    [ApiController]
    [Route("api/v1/clientes/{clienteId}/carteira")]
    [Authorize]
    public class CarteiraController : ControllerBase
    {
        private readonly RecarregarCarteiraHandler _recarregarCarteiraHandler;

        public CarteiraController(
            RecarregarCarteiraHandler recarregarCarteiraHandler)
        {
            _recarregarCarteiraHandler = recarregarCarteiraHandler;
        }

        [HttpPost("recarga")]
        public async Task<IActionResult> Recarregar(
            Guid clienteId,
            [FromBody] RecarregarCarteiraRequest request,
            CancellationToken cancellationToken)
        {
            try
            {
                var command = new RecarregarCarteiraCommand(
                    clienteId,
                    request.Valor,
                    request.TipoPagamento,
                    request.Descricao);

                var response =
                    await _recarregarCarteiraHandler.HandleAsync(
                        command,
                        cancellationToken);

                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    mensagem = ex.Message
                });
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
                return Conflict(new
                {
                    mensagem = ex.Message
                });
            }
        }
    }

    public sealed record RecarregarCarteiraRequest(
        decimal Valor,
        TipoPagamento TipoPagamento,
        string? Descricao);
}
