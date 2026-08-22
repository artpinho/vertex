using Microsoft.AspNetCore.Mvc;
using Vertex.Application.Computers.Commands.ProcessarHeartbeat;
using Vertex.Application.Computers.Commands.RegistrarComputador;
using Vertex.Application.Computers.DTOs;
using Vertex.Application.Computers.Queries;
using Vertex.Contracts.Computers;

namespace Vertex.Api.Controllers
{
    [ApiController]
    [Route("api/v1/computadores")]
    public class ComputadoresController : ControllerBase
    {
        private readonly RegistrarComputadorHandler _registrarHandler;
        private readonly ListarComputadoresHandler _listarHandler;
        private readonly ObterComputadorHandler _obterHandler;
        private readonly ProcessarHeartbeatHandler _heartbeatHandler;

        public ComputadoresController(
        RegistrarComputadorHandler registrarHandler,
        ListarComputadoresHandler listarHandler,
        ObterComputadorHandler obterHandler,
        ProcessarHeartbeatHandler heartbeatHandler)
        {
            _registrarHandler = registrarHandler;
            _listarHandler = listarHandler;
            _obterHandler = obterHandler;
            _heartbeatHandler = heartbeatHandler;
        }

        [HttpPost]
        [ProducesResponseType(
        typeof(ComputadorResponse),
        StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<ComputadorResponse>> Registrar(
        [FromBody] RegistrarComputadorCommand command,
        CancellationToken cancellationToken)
        {
            try
            {
                var response = await _registrarHandler.HandleAsync(
                    command,
                    cancellationToken);

                return Created(
                    $"/api/v1/computadores/{response.Id}",
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

        [HttpGet]
        [ProducesResponseType(
            typeof(IReadOnlyList<ComputadorResponse>),
            StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<ComputadorResponse>>> Listar(
            CancellationToken cancellationToken)
        {
            var response = await _listarHandler.HandleAsync(
                new ListarComputadoresQuery(),
                cancellationToken);

            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(
            typeof(ComputadorResponse),
            StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ComputadorResponse>> Obter(
            Guid id,
            CancellationToken cancellationToken)
        {
            var response = await _obterHandler.HandleAsync(
                new ObterComputadorQuery(id),
                cancellationToken);

            if (response is null)
                return NotFound();

            return Ok(response);
        }

        [HttpPost("{id:guid}/heartbeat")]
        [ProducesResponseType(
            typeof(HeartbeatResponse),
            StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<HeartbeatResponse>> Heartbeat(
            Guid id,
            [FromBody] HeartbeatRequest request,
            CancellationToken cancellationToken)
        {
            if (id != request.ComputadorId)
            {
                return BadRequest(new
                {
                    message = "O ID da rota não corresponde ao ID do computador."
                });
            }

            try
            {
                var ultimoHeartbeat =
                    await _heartbeatHandler.HandleAsync(
                        new ProcessarHeartbeatCommand(
                            request.ComputadorId,
                            request.HostName,
                            request.Ip,
                            request.MacAddress,
                            request.SistemaOperacional,
                            request.ClienteVersao,
                            request.CpuUso,
                            request.MemoriaUso,
                            request.DiscoLivre),
                        cancellationToken);

                return Ok(new HeartbeatResponse(
                    request.ComputadorId,
                    ultimoHeartbeat,
                    true));
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
