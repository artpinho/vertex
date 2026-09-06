using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vertex.Application.TariffConfigurations.Commands.AlterarStatusConfiguracaoTarifacao;
using Vertex.Application.TariffConfigurations.Commands.AtualizarConfiguracaoTarifacao;
using Vertex.Application.TariffConfigurations.Commands.CriarConfiguracaoTarifacao;
using Vertex.Application.TariffConfigurations.Queries.ListarConfiguracoesTarifacao;
using Vertex.Application.TariffConfigurations.Queries.ObterConfiguracaoTarifacao;
using Vertex.Contracts.TariffConfigurations;

namespace Vertex.Api.Controllers
{
    [ApiController]
    [Route("api/v1/configuracoes-tarifacao")]
    [Authorize]
    public class ConfiguracoesTarifacaoController : ControllerBase
    {
        private readonly CriarConfiguracaoTarifacaoHandler _criarHandler;
        private readonly ObterConfiguracaoTarifacaoHandler _obterHandler;
        private readonly ListarConfiguracoesTarifacaoHandler _listarHandler;
        private readonly AtualizarConfiguracaoTarifacaoHandler _atualizarHandler;
        private readonly AlterarStatusConfiguracaoTarifacaoHandler _alterarStatusHandler;

        public ConfiguracoesTarifacaoController(
            CriarConfiguracaoTarifacaoHandler criarHandler,
            ObterConfiguracaoTarifacaoHandler obterHandler,
            ListarConfiguracoesTarifacaoHandler listarHandler,
            AtualizarConfiguracaoTarifacaoHandler atualizarHandler,
            AlterarStatusConfiguracaoTarifacaoHandler alterarStatusHandler)
        {
            _criarHandler = criarHandler;
            _obterHandler = obterHandler;
            _listarHandler = listarHandler;
            _atualizarHandler = atualizarHandler;
            _alterarStatusHandler = alterarStatusHandler;
        }

        [HttpPost]
        public async Task<IActionResult> Criar(
            [FromBody] CriarConfiguracaoTarifacaoRequest request,
            CancellationToken cancellationToken)
        {
            try
            {
                var command = new CriarConfiguracaoTarifacaoCommand(
                    request.Nome,
                    request.Descricao,
                    request.TipoMaquinaId,
                    request.ValorHora,
                    request.DataInicio,
                    request.DataFim,
                    request.Prioridade);

                var resultado = await _criarHandler.HandleAsync(
                    command,
                    cancellationToken);

                return CreatedAtAction(
                    nameof(ObterPorId),
                    new { id = resultado.Id },
                    resultado);
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

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> ObterPorId(
                Guid id,
                CancellationToken cancellationToken)
        {
            var resultado = await _obterHandler.HandleAsync(
                id,
                cancellationToken);

            if (resultado is null)
                return NotFound(new
                {
                    mensagem = "Configuração de tarifação não encontrada."
                });

            return Ok(resultado);
        }

        [HttpGet]
        public async Task<IActionResult> Listar(
            CancellationToken cancellationToken)
        {
            var resultado = await _listarHandler.HandleAsync(
                cancellationToken);

            return Ok(resultado);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Atualizar(
            Guid id,
            [FromBody] AtualizarConfiguracaoTarifacaoRequest request,
            CancellationToken cancellationToken)
        {
            try
            {
                var command = new AtualizarConfiguracaoTarifacaoCommand(
                    id,
                    request.Nome,
                    request.Descricao,
                    request.ValorHora,
                    request.DataInicio,
                    request.DataFim,
                    request.Prioridade);

                var resultado = await _atualizarHandler.HandleAsync(
                    command,
                    cancellationToken);

                return Ok(resultado);
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
        public async Task<IActionResult> AlterarStatus(
            Guid id,
            [FromBody] AlterarStatusConfiguracaoTarifacaoRequest request,
            CancellationToken cancellationToken)
        {
            try
            {
                var command = new AlterarStatusConfiguracaoTarifacaoCommand(
                    id,
                    (OperacaoConfiguracaoTarifacao)request.Operacao);

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
