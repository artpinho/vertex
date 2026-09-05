using Microsoft.AspNetCore.Mvc;
using Vertex.Application.MachineTypes.Commands.AlterarStatusTipoMaquina;
using Vertex.Application.MachineTypes.Commands.AtualizarTipoMaquina;
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
        private readonly AtualizarTipoMaquinaHandler _atualizarHandler;
        private readonly AlterarStatusTipoMaquinaHandler _alterarStatusHandler;

        public TiposMaquinaController(
            CriarTipoMaquinaHandler criarHandler,
            ListarTiposMaquinaHandler listarHandler,
            ObterTipoMaquinaHandler obterHandler,
            AtualizarTipoMaquinaHandler atualizarHandler,
            AlterarStatusTipoMaquinaHandler alterarStatusHandler)
        {
            _criarHandler = criarHandler;
            _listarHandler = listarHandler;
            _obterHandler = obterHandler;
            _atualizarHandler = atualizarHandler;
            _alterarStatusHandler = alterarStatusHandler;
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

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<AtualizarTipoMaquinaResponse>> Atualizar(
            Guid id,
            [FromBody] AtualizarTipoMaquinaCommand command,
            CancellationToken cancellationToken)
        {
            if (id != command.TipoMaquinaId)
            {
                return BadRequest(new
                {
                    mensagem = "O ID da rota é diferente do ID informado."
                });
            }

            try
            {
                var response = await _atualizarHandler.HandleAsync(
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
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
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

        [HttpPost("{id:guid}/status")]
        public async Task<ActionResult<AlterarStatusTipoMaquinaResponse>> AlterarStatus(
            Guid id,
            [FromBody] AlterarStatusTipoMaquinaCommand command,
            CancellationToken cancellationToken)
        {
            if (id != command.TipoMaquinaId)
            {
                return BadRequest(new
                {
                    mensagem = "O ID da rota é diferente do ID informado."
                });
            }

            try
            {
                var response = await _alterarStatusHandler.HandleAsync(
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
