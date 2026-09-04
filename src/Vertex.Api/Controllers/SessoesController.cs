using Microsoft.AspNetCore.Mvc;
using Vertex.Application.Sessions.Commands.EncerrarSessao;
using Vertex.Application.Sessions.Commands.IniciarSessao;
using Vertex.Application.Sessions.Queries;

namespace Vertex.Api.Controllers
{
    [ApiController]
    [Route("api/v1/sessoes")]
    public class SessoesController : ControllerBase
    {
        private readonly IniciarSessaoHandler _iniciarSessaoHandler;
        private readonly EncerrarSessaoHandler _encerrarSessaoHandler;
        private readonly ListarSessoesHandler _listarSessoesHandler;
        private readonly ObterSessaoHandler _obterSessaoHandler;

        public SessoesController(
            IniciarSessaoHandler iniciarSessaoHandler,
            EncerrarSessaoHandler encerrarSessaoHandler,
            ListarSessoesHandler listarSessoesHandler,
            ObterSessaoHandler obterSessaoHandler)
        {
            _iniciarSessaoHandler = iniciarSessaoHandler;
            _encerrarSessaoHandler = encerrarSessaoHandler;
            _listarSessoesHandler = listarSessoesHandler;
            _obterSessaoHandler = obterSessaoHandler;
        }

        [HttpPost]
        [ProducesResponseType(
            typeof(IniciarSessaoResponse),
            StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IniciarSessaoResponse>> Iniciar(
            [FromBody] IniciarSessaoCommand command,
            CancellationToken cancellationToken)
        {
            try
            {
                var response =
                    await _iniciarSessaoHandler.HandleAsync(
                        command,
                        cancellationToken);

                return StatusCode(
                    StatusCodes.Status201Created,
                    response);
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

        [HttpPost("{id:guid}/encerrar")]
        [ProducesResponseType(
            typeof(EncerrarSessaoResponse),
            StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EncerrarSessaoResponse>> Encerrar(
            Guid id,
            CancellationToken cancellationToken)
        {
            try
            {
                var response =
                    await _encerrarSessaoHandler.HandleAsync(
                        new EncerrarSessaoCommand(id),
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
            catch (InvalidOperationException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Listar(
            CancellationToken cancellationToken)
        {
            var resultado = await _listarSessoesHandler.HandleAsync(
                cancellationToken);

            return Ok(resultado);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Obter(
            Guid id,
            CancellationToken cancellationToken)
        {
            var resultado = await _obterSessaoHandler.HandleAsync(
                id,
                cancellationToken);

            if (resultado is null)
                return NotFound();

            return Ok(resultado);
        }
    }
}
