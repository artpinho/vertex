using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vertex.Application.Wallet.Commands.RecarregarCarteira;
using Vertex.Application.Wallet.Queries.ListarMovimentacoesCarteira;
using Vertex.Application.Wallet.Queries.ObterCarteira;
using Vertex.Domain.Enums;

namespace Vertex.Api.Controllers
{
    [ApiController]
    [Route("api/v1/clientes/{clienteId}/carteira")]
    [Authorize]
    public class CarteiraController : ControllerBase
    {
        private readonly RecarregarCarteiraHandler _recarregarCarteiraHandler;
        private readonly ObterCarteiraHandler _obterCarteiraHandler;
        private readonly ListarMovimentacoesCarteiraHandler
            _listarMovimentacoesCarteiraHandler;

        public CarteiraController(
            RecarregarCarteiraHandler recarregarCarteiraHandler,
            ObterCarteiraHandler obterCarteiraHandler,
            ListarMovimentacoesCarteiraHandler listarMovimentacoesCarteiraHandler)
        {
            _recarregarCarteiraHandler = recarregarCarteiraHandler;
            _obterCarteiraHandler = obterCarteiraHandler;
            _listarMovimentacoesCarteiraHandler = listarMovimentacoesCarteiraHandler;
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
                    message = ex.Message
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> ObterCarteira(
            Guid clienteId,
            CancellationToken cancellationToken)
        {
            try
            {
                var response =
                    await _obterCarteiraHandler.HandleAsync(
                        new ObterCarteiraQuery(clienteId),
                        cancellationToken);

                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpGet("movimentacoes")]
        public async Task<IActionResult> ListarMovimentacoes(
            Guid clienteId,
            CancellationToken cancellationToken)
        {
            try
            {
                var response =
                    await _listarMovimentacoesCarteiraHandler.HandleAsync(
                        new ListarMovimentacoesCarteiraQuery(clienteId),
                        cancellationToken);

                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    message = ex.Message
                });
            }
        }
    }

    public sealed record RecarregarCarteiraRequest(
        decimal Valor,
        TipoPagamento TipoPagamento,
        string? Descricao);
}
