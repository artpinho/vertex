using Microsoft.AspNetCore.Mvc;
using Vertex.Application.Promotions.Commands.AlterarStatusPromocao;
using Vertex.Application.Promotions.Commands.AssociarPromocaoTipoMaquina;
using Vertex.Application.Promotions.Commands.AtualizarPromocao;
using Vertex.Application.Promotions.Commands.CriarPromocao;
using Vertex.Application.Promotions.Commands.RemoverPromocaoTipoMaquina;
using Vertex.Application.Promotions.Queries.ListarPromocoes;
using Vertex.Application.Promotions.Queries.ListarTiposMaquinaPromocao;
using Vertex.Application.Promotions.Queries.ObterPromocao;

namespace Vertex.Api.Controllers;

[ApiController]
[Route("api/v1/promocoes")]
public class PromocoesController : ControllerBase
{
    private readonly CriarPromocaoHandler _criarPromocaoHandler;
    private readonly ListarPromocoesHandler _listarPromocoesHandler;
    private readonly ObterPromocaoHandler _obterPromocaoHandler;
    private readonly AtualizarPromocaoHandler _atualizarPromocaoHandler;
    private readonly AlterarStatusPromocaoHandler _alterarStatusPromocaoHandler;

    private readonly AssociarPromocaoTipoMaquinaHandler
        _associarPromocaoTipoMaquinaHandler;

    private readonly RemoverPromocaoTipoMaquinaHandler
        _removerPromocaoTipoMaquinaHandler;

    private readonly ListarTiposMaquinaPromocaoHandler
        _listarTiposMaquinaPromocaoHandler;

    public PromocoesController(
        CriarPromocaoHandler criarPromocaoHandler,
        ListarPromocoesHandler listarPromocoesHandler,
        ObterPromocaoHandler obterPromocaoHandler,
        AtualizarPromocaoHandler atualizarPromocaoHandler,
        AlterarStatusPromocaoHandler alterarStatusPromocaoHandler,
        AssociarPromocaoTipoMaquinaHandler associarPromocaoTipoMaquinaHandler,
        RemoverPromocaoTipoMaquinaHandler removerPromocaoTipoMaquinaHandler,
        ListarTiposMaquinaPromocaoHandler listarTiposMaquinaPromocaoHandler)
    {
        _criarPromocaoHandler = criarPromocaoHandler;
        _listarPromocoesHandler = listarPromocoesHandler;
        _obterPromocaoHandler = obterPromocaoHandler;
        _atualizarPromocaoHandler = atualizarPromocaoHandler;
        _alterarStatusPromocaoHandler = alterarStatusPromocaoHandler;
        _associarPromocaoTipoMaquinaHandler = associarPromocaoTipoMaquinaHandler;
        _removerPromocaoTipoMaquinaHandler = removerPromocaoTipoMaquinaHandler;
        _listarTiposMaquinaPromocaoHandler = listarTiposMaquinaPromocaoHandler;
    }

    [HttpPost]
    public async Task<IActionResult> Criar(
        [FromBody] CriarPromocaoCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            var response = await _criarPromocaoHandler.HandleAsync(
                command,
                cancellationToken);

            return CreatedAtAction(
                nameof(ObterPorId),
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
    public async Task<IActionResult> Listar(
        CancellationToken cancellationToken)
    {
        var query = new ListarPromocoesQuery();

        var response = await _listarPromocoesHandler.HandleAsync(
            query,
            cancellationToken);

        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> ObterPorId(
        Guid id,
        CancellationToken cancellationToken)
    {
        var query = new ObterPromocaoQuery(id);

        var response = await _obterPromocaoHandler.HandleAsync(
            query,
            cancellationToken);

        if (response is null)
        {
            return NotFound(new
            {
                mensagem = "A promoção informada não foi encontrada."
            });
        }

        return Ok(response);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Atualizar(
        Guid id,
        [FromBody] AtualizarPromocaoCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            if (id != command.Id)
            {
                return BadRequest(new
                {
                    mensagem =
                        "O ID informado na rota é diferente do ID da requisição."
                });
            }

            await _atualizarPromocaoHandler.HandleAsync(
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
        [FromBody] AlterarStatusPromocaoCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            if (id != command.PromocaoId)
            {
                return BadRequest(new
                {
                    mensagem =
                        "O ID informado na rota é diferente do ID da requisição."
                });
            }

            await _alterarStatusPromocaoHandler.HandleAsync(
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

    [HttpPost("{promocaoId:guid}/tipos-maquina/{tipoMaquinaId:guid}")]
    public async Task<IActionResult> AssociarTipoMaquina(
        Guid promocaoId,
        Guid tipoMaquinaId,
        CancellationToken cancellationToken)
    {
        try
        {
            var command = new AssociarPromocaoTipoMaquinaCommand(
                promocaoId,
                tipoMaquinaId);

            await _associarPromocaoTipoMaquinaHandler.HandleAsync(
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

    [HttpDelete("{promocaoId:guid}/tipos-maquina/{tipoMaquinaId:guid}")]
    public async Task<IActionResult> RemoverTipoMaquina(
        Guid promocaoId,
        Guid tipoMaquinaId,
        CancellationToken cancellationToken)
    {
        try
        {
            var command = new RemoverPromocaoTipoMaquinaCommand(
                promocaoId,
                tipoMaquinaId);

            await _removerPromocaoTipoMaquinaHandler.HandleAsync(
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
    }

    [HttpGet("{promocaoId:guid}/tipos-maquina")]
    public async Task<IActionResult> ListarTiposMaquina(
        Guid promocaoId,
        CancellationToken cancellationToken)
    {
        try
        {
            var query = new ListarTiposMaquinaPromocaoQuery(
                promocaoId);

            var response =
                await _listarTiposMaquinaPromocaoHandler.HandleAsync(
                    query,
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
    }
}