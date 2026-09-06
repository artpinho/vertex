using Microsoft.AspNetCore.Mvc;
using Vertex.Application.TariffTimeBands.Commands.AlterarStatusFaixaHorarioTarifacao;
using Vertex.Application.TariffTimeBands.Commands.AtualizarFaixaHorarioTarifacao;
using Vertex.Application.TariffTimeBands.Commands.CriarFaixaHorarioTarifacao;
using Vertex.Application.TariffTimeBands.Queries.ListarFaixasHorarioTarifacao;
using Vertex.Application.TariffTimeBands.Queries.ObterFaixaHorarioTarifacao;
using Vertex.Contracts.TariffTimeBands;

namespace Vertex.Api.Controllers
{
    [ApiController]
    [Route("api/v1/faixas-horario-tarifacao")]
    public class FaixasHorarioTarifacaoController : ControllerBase
    {
        private readonly CriarFaixaHorarioTarifacaoHandler _criarHandler;
        private readonly ListarFaixasHorarioTarifacaoHandler _listarHandler;
        private readonly ObterFaixaHorarioTarifacaoHandler _obterHandler;
        private readonly AtualizarFaixaHorarioTarifacaoHandler _atualizarHandler;
        private readonly AlterarStatusFaixaHorarioTarifacaoHandler _alterarStatusHandler;

        public FaixasHorarioTarifacaoController(
            CriarFaixaHorarioTarifacaoHandler criarHandler,
            ListarFaixasHorarioTarifacaoHandler listarHandler,
            ObterFaixaHorarioTarifacaoHandler obterHandler,
            AtualizarFaixaHorarioTarifacaoHandler atualizarHandler,
            AlterarStatusFaixaHorarioTarifacaoHandler alterarStatusHandler)
        {
            _criarHandler = criarHandler;
            _listarHandler = listarHandler;
            _obterHandler = obterHandler;
            _atualizarHandler = atualizarHandler;
            _alterarStatusHandler = alterarStatusHandler;
        }

        [HttpPost]
        public async Task<IActionResult> Criar(
            [FromBody] CriarFaixaHorarioTarifacaoRequest request,
            CancellationToken cancellationToken)
        {
            try
            {
                var command = new CriarFaixaHorarioTarifacaoCommand(
                    request.ConfiguracaoTarifacaoId,
                    request.DiaSemana,
                    request.HoraInicio,
                    request.HoraFim,
                    request.ValorHora);

                var result = await _criarHandler.HandleAsync(
                    command,
                    cancellationToken);

                return CreatedAtAction(
                    nameof(Criar),
                    new { id = result.Id },
                    result);
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

        [HttpGet]
        public async Task<IActionResult> Listar(
            [FromQuery] Guid configuracaoTarifacaoId,
            CancellationToken cancellationToken)
        {
            var query = new ListarFaixasHorarioTarifacaoQuery(
                configuracaoTarifacaoId);

            var result = await _listarHandler.HandleAsync(
                query,
                cancellationToken);

            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Obter(
            Guid id,
            CancellationToken cancellationToken)
        {
            var query = new ObterFaixaHorarioTarifacaoQuery(id);

            var result = await _obterHandler.HandleAsync(
                query,
                cancellationToken);

            if (result is null)
                return NotFound(new
                {
                    mensagem = "A faixa de horário informada não foi encontrada."
                });

            return Ok(result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Atualizar(
            Guid id,
            [FromBody] AtualizarFaixaHorarioTarifacaoRequest request,
            CancellationToken cancellationToken)
        {
            try
            {
                var command = new AtualizarFaixaHorarioTarifacaoCommand(
                    id,
                    request.DiaSemana,
                    request.HoraInicio,
                    request.HoraFim,
                    request.ValorHora);

                await _atualizarHandler.HandleAsync(
                    command,
                    cancellationToken);

                return NoContent();
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

        [HttpPost("{id:guid}/status")]
        public async Task<IActionResult> AlterarStatus(
            Guid id,
            [FromBody] AlterarStatusFaixaHorarioTarifacaoRequest request,
            CancellationToken cancellationToken)
        {
            try
            {
                var command = new AlterarStatusFaixaHorarioTarifacaoCommand(
                    id,
                    (OperacaoFaixaHorarioTarifacao)request.Operacao);

                await _alterarStatusHandler.HandleAsync(
                    command,
                    cancellationToken);

                return NoContent();
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
        }
    }
}
