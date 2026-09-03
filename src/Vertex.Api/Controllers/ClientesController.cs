using Microsoft.AspNetCore.Mvc;
using Vertex.Application.Clients.Commands.AlterarStatus;
using Vertex.Application.Clients.Commands.AtualizarCliente;
using Vertex.Application.Clients.Commands.CriarCliente;
using Vertex.Application.Clients.Queries;

namespace Vertex.Api.Controllers
{
    [ApiController]
    [Route("api/v1/clientes")]
    public class ClientesController : ControllerBase
    {
        private readonly CriarClienteHandler _criarClienteHandler;
        private readonly ListarClientesHandler _listarClientesHandler;
        private readonly ObterClienteHandler _obterClienteHandler;
        private readonly AtualizarClienteHandler _atualizarClienteHandler;
        private readonly AlterarStatusClienteHandler _alterarStatusClienteHandler;

        public ClientesController(
            CriarClienteHandler criarClienteHandler,
            ListarClientesHandler listarClientesHandler,
            ObterClienteHandler obterClienteHandler,
            AtualizarClienteHandler atualizarClienteHandler,
            AlterarStatusClienteHandler alterarStatusClienteHandler)
        {
            _criarClienteHandler = criarClienteHandler;
            _listarClientesHandler = listarClientesHandler;
            _obterClienteHandler = obterClienteHandler;
            _atualizarClienteHandler = atualizarClienteHandler;
            _alterarStatusClienteHandler = alterarStatusClienteHandler;
        }

        [HttpPost]
        [ProducesResponseType(
            typeof(CriarClienteResponse),
            StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CriarClienteResponse>> Criar(
            [FromBody] CriarClienteCommand command,
            CancellationToken cancellationToken)
        {
            try
            {
                var response =
                    await _criarClienteHandler.HandleAsync(
                        command,
                        cancellationToken);

                return StatusCode(
                    StatusCodes.Status201Created,
                    response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpGet]
        [ProducesResponseType(
            typeof(IReadOnlyList<ClienteResponse>),
            StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<ClienteResponse>>> Listar(
            CancellationToken cancellationToken)
        {
            var response =
                await _listarClientesHandler.HandleAsync(
                    cancellationToken);

            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(
            typeof(ClienteResponse),
            StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ClienteResponse>> Obter(
            Guid id,
            CancellationToken cancellationToken)
        {
            var response =
                await _obterClienteHandler.HandleAsync(
                    id,
                    cancellationToken);

            if (response is null)
            {
                return NotFound(new
                {
                    message = "Cliente não encontrado."
                });
            }

            return Ok(response);
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(
            typeof(AtualizarClienteResponse),
            StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AtualizarClienteResponse>> Atualizar(
            Guid id,
        [FromBody] AtualizarClienteCommand command,
            CancellationToken cancellationToken)
        {
            if (id != command.ClienteId)
            {
                return BadRequest(new
                {
                    message =
                        "O ID da rota não corresponde ao ID do cliente."
                });
            }

            try
            {
                var response =
                    await _atualizarClienteHandler.HandleAsync(
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
        }

        [HttpPost("{id:guid}/status")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AlterarStatus(
            Guid id,
        [FromBody] AlterarStatusClienteCommand command,
            CancellationToken cancellationToken)
        {
            if (id != command.ClienteId)
            {
                return BadRequest(new
                {
                    message =
                        "O ID da rota não corresponde ao ID do cliente."
                });
            }

            try
            {
                await _alterarStatusClienteHandler.HandleAsync(
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
        }
    }
}
