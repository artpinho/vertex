using Microsoft.AspNetCore.Mvc;
using Vertex.Application.Stations.Commands.AlterarStatus;
using Vertex.Application.Stations.Commands.AssociarComputador;
using Vertex.Application.Stations.Commands.CriarEstacao;
using Vertex.Application.Stations.Queries;

namespace Vertex.Api.Controllers
{
    [ApiController]
    [Route("api/v1/estacoes")]
    public class EstacoesController : ControllerBase
    {
        private readonly CriarEstacaoHandler _criarEstacaoHandler;
        private readonly ListarEstacoesHandler _listarEstacoesHandler;
        private readonly ObterEstacaoHandler _obterEstacaoHandler;
        private readonly AssociarComputadorHandler _associarComputadorHandler;
        private readonly AlterarStatusEstacaoHandler _alterarStatusEstacaoHandler;

        public EstacoesController(
            CriarEstacaoHandler criarEstacaoHandler,
            ListarEstacoesHandler listarEstacoesHandler,
            ObterEstacaoHandler obterEstacaoHandler,
            AssociarComputadorHandler associarComputadorHandler,
            AlterarStatusEstacaoHandler alterarStatusEstacaoHandler)
        {
            _criarEstacaoHandler = criarEstacaoHandler;
            _listarEstacoesHandler = listarEstacoesHandler;
            _obterEstacaoHandler = obterEstacaoHandler;
            _associarComputadorHandler = associarComputadorHandler;
            _alterarStatusEstacaoHandler = alterarStatusEstacaoHandler;
        }

        [HttpPost]
        [ProducesResponseType(
            typeof(CriarEstacaoResponse),
            StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<CriarEstacaoResponse>> Criar(
            [FromBody] CriarEstacaoCommand command,
            CancellationToken cancellationToken)
        {
            try
            {
                var response =
                    await _criarEstacaoHandler.HandleAsync(
                        command,
                        cancellationToken);

                return CreatedAtAction(
                    nameof(Obter),
                    new { id = response.Id },
                    response);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new
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
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(
            typeof(EstacaoResponse),
            StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EstacaoResponse>> Obter(
            Guid id,
            CancellationToken cancellationToken)
        {
            var response =
                await _obterEstacaoHandler.HandleAsync(
                    id,
                    cancellationToken);

            if (response is null)
            {
                return NotFound(new
                {
                    message = "Estação não encontrada."
                });
            }

            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(
            typeof(IReadOnlyList<EstacaoResponse>),
            StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<EstacaoResponse>>> Listar(
            CancellationToken cancellationToken)
        {
            var response =
                await _listarEstacoesHandler.HandleAsync(
                    cancellationToken);

            return Ok(response);
        }

        [HttpPost("{id:guid}/associar-computador")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> AssociarComputador(
            Guid id,
        [FromBody] AssociarComputadorCommand command,
            CancellationToken cancellationToken)
        {
            if (id != command.EstacaoId)
            {
                return BadRequest(new
                {
                    message =
                        "O ID da rota não corresponde ao ID da estação."
                });
            }

            try
            {
                await _associarComputadorHandler.HandleAsync(
                    command,
                    cancellationToken);

                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
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

        [HttpPost("{id:guid}/status")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AlterarStatus(
            Guid id,
            [FromBody] AlterarStatusEstacaoCommand command,
            CancellationToken cancellationToken)
        {
            if (id != command.EstacaoId)
            {
                return BadRequest(new
                {
                    message =
                        "O ID da rota não corresponde ao ID da estação."
                });
            }

            try
            {
                await _alterarStatusEstacaoHandler.HandleAsync(
                    command,
                    cancellationToken);

                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    message = ex.Message
                });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }
    }
}
