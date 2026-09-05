using Microsoft.AspNetCore.Mvc;
using Vertex.Application.MachineTypes.Commands.CriarTipoMaquina;
using Vertex.Application.MachineTypes.Queries.ListarTiposMaquina;
using Vertex.Application.MachineTypes.Queries.ObterTipoMaquina;

namespace Vertex.Api.Controllers
{
    [ApiController]
    [Route("api/v1/tipos-maquina")]
    public class TiposMaquinaController : ControllerBase
    {
        private readonly CriarTipoMaquinaHandler _criarHandler;
        private readonly ListarTiposMaquinaHandler _listarHandler;
        private readonly ObterTipoMaquinaHandler _obterHandler;

        public TiposMaquinaController(
            CriarTipoMaquinaHandler criarHandler,
            ListarTiposMaquinaHandler listarHandler,
            ObterTipoMaquinaHandler obterHandler)
        {
            _criarHandler = criarHandler;
            _listarHandler = listarHandler;
            _obterHandler = obterHandler;
        }

        [HttpPost]
        public async Task<ActionResult<CriarTipoMaquinaResponse>> Criar(
            [FromBody] CriarTipoMaquinaCommand command,
            CancellationToken cancellationToken)
        {
            try
            {
                var response = await _criarHandler.HandleAsync(
                    command,
                    cancellationToken);

                return CreatedAtAction(
                    nameof(Criar),
                    new { id = response.Id },
                    response);
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

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ListarTiposMaquinaResponse>>> Listar(
            CancellationToken cancellationToken)
        {
            var response = await _listarHandler.HandleAsync(
                cancellationToken);

            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ObterTipoMaquinaResponse>> ObterPorId(
            Guid id,
            CancellationToken cancellationToken)
        {
            try
            {
                var response = await _obterHandler.HandleAsync(
                    new ObterTipoMaquinaQuery(id),
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
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    mensagem = ex.Message
                });
            }
        }
    }
}
