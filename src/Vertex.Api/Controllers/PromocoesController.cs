using Microsoft.AspNetCore.Mvc;
using Vertex.Application.Promotions.Commands.AlterarStatusPromocao;
using Vertex.Application.Promotions.Commands.AssociarPromocaoDiaSemana;
using Vertex.Application.Promotions.Commands.AssociarPromocaoFaixaHorario;
using Vertex.Application.Promotions.Commands.AssociarPromocaoTipoMaquina;
using Vertex.Application.Promotions.Commands.AtualizarPromocao;
using Vertex.Application.Promotions.Commands.AtualizarPromocaoFaixaHorario;
using Vertex.Application.Promotions.Commands.CriarPromocao;
using Vertex.Application.Promotions.Commands.RemoverPromocaoDiaSemana;
using Vertex.Application.Promotions.Commands.RemoverPromocaoFaixaHorario;
using Vertex.Application.Promotions.Commands.RemoverPromocaoTipoMaquina;
using Vertex.Application.Promotions.Queries.ListarDiasSemanaPromocao;
using Vertex.Application.Promotions.Queries.ListarFaixasHorarioPromocao;
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
    private readonly AssociarPromocaoTipoMaquinaHandler _associarPromocaoTipoMaquinaHandler;
    private readonly RemoverPromocaoTipoMaquinaHandler _removerPromocaoTipoMaquinaHandler;
    private readonly ListarTiposMaquinaPromocaoHandler _listarTiposMaquinaPromocaoHandler;
    private readonly AssociarPromocaoDiaSemanaHandler _associarPromocaoDiaSemanaHandler;
    private readonly RemoverPromocaoDiaSemanaHandler _removerPromocaoDiaSemanaHandler;
    private readonly ListarDiasSemanaPromocaoHandler _listarDiasSemanaPromocaoHandler;
    private readonly AssociarPromocaoFaixaHorarioHandler _associarPromocaoFaixaHorarioHandler;
    private readonly AtualizarPromocaoFaixaHorarioHandler _atualizarPromocaoFaixaHorarioHandler;
    private readonly RemoverPromocaoFaixaHorarioHandler _removerPromocaoFaixaHorarioHandler;
    private readonly ListarFaixasHorarioPromocaoHandler _listarFaixasHorarioPromocaoHandler;

    public PromocoesController(
        CriarPromocaoHandler criarPromocaoHandler,
        ListarPromocoesHandler listarPromocoesHandler,
        ObterPromocaoHandler obterPromocaoHandler,
        AtualizarPromocaoHandler atualizarPromocaoHandler,
        AlterarStatusPromocaoHandler alterarStatusPromocaoHandler,
        AssociarPromocaoTipoMaquinaHandler associarPromocaoTipoMaquinaHandler,
        RemoverPromocaoTipoMaquinaHandler removerPromocaoTipoMaquinaHandler,
        ListarTiposMaquinaPromocaoHandler listarTiposMaquinaPromocaoHandler,
        AssociarPromocaoDiaSemanaHandler associarPromocaoDiaSemanaHandler,
        RemoverPromocaoDiaSemanaHandler removerPromocaoDiaSemanaHandler,
        ListarDiasSemanaPromocaoHandler listarDiasSemanaPromocaoHandler,
        AssociarPromocaoFaixaHorarioHandler associarPromocaoFaixaHorarioHandler,
        AtualizarPromocaoFaixaHorarioHandler atualizarPromocaoFaixaHorarioHandler,
        RemoverPromocaoFaixaHorarioHandler removerPromocaoFaixaHorarioHandler,
        ListarFaixasHorarioPromocaoHandler listarFaixasHorarioPromocaoHandler)
    {
        _criarPromocaoHandler = criarPromocaoHandler;
        _listarPromocoesHandler = listarPromocoesHandler;
        _obterPromocaoHandler = obterPromocaoHandler;
        _atualizarPromocaoHandler = atualizarPromocaoHandler;
        _alterarStatusPromocaoHandler = alterarStatusPromocaoHandler;
        _associarPromocaoTipoMaquinaHandler = associarPromocaoTipoMaquinaHandler;
        _removerPromocaoTipoMaquinaHandler = removerPromocaoTipoMaquinaHandler;
        _listarTiposMaquinaPromocaoHandler = listarTiposMaquinaPromocaoHandler;
        _associarPromocaoDiaSemanaHandler = associarPromocaoDiaSemanaHandler;
        _removerPromocaoDiaSemanaHandler =  removerPromocaoDiaSemanaHandler;
        _listarDiasSemanaPromocaoHandler = listarDiasSemanaPromocaoHandler;
        _associarPromocaoFaixaHorarioHandler = associarPromocaoFaixaHorarioHandler;
        _atualizarPromocaoFaixaHorarioHandler = atualizarPromocaoFaixaHorarioHandler;
        _removerPromocaoFaixaHorarioHandler = removerPromocaoFaixaHorarioHandler;
        _listarFaixasHorarioPromocaoHandler = listarFaixasHorarioPromocaoHandler;
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

    [HttpPost("{promocaoId:guid}/dias-semana/{diaSemana:int}")]
    public async Task<IActionResult> AssociarDiaSemana(
    Guid promocaoId,
    int diaSemana,
    CancellationToken cancellationToken)
    {
        try
        {
            var command = new AssociarPromocaoDiaSemanaCommand(
                promocaoId,
                diaSemana);

            await _associarPromocaoDiaSemanaHandler.HandleAsync(
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

    [HttpDelete("{promocaoId:guid}/dias-semana/{diaSemana:int}")]
    public async Task<IActionResult> RemoverDiaSemana(
    Guid promocaoId,
    int diaSemana,
    CancellationToken cancellationToken)
    {
        try
        {
            var command = new RemoverPromocaoDiaSemanaCommand(
                promocaoId,
                diaSemana);

            await _removerPromocaoDiaSemanaHandler.HandleAsync(
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

    [HttpGet("{promocaoId:guid}/dias-semana")]
    public async Task<IActionResult> ListarDiasSemana(
    Guid promocaoId,
    CancellationToken cancellationToken)
    {
        try
        {
            var query = new ListarDiasSemanaPromocaoQuery(
                promocaoId);

            var response =
                await _listarDiasSemanaPromocaoHandler.HandleAsync(
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

    [HttpPost("{promocaoId:guid}/faixas-horario")]
    public async Task<IActionResult> AssociarFaixaHorario(
    Guid promocaoId,
    [FromBody] AssociarPromocaoFaixaHorarioCommand command,
    CancellationToken cancellationToken)
    {
        try
        {
            if (promocaoId != command.PromocaoId)
            {
                return BadRequest(new
                {
                    mensagem =
                        "O ID da promoção informado na rota é diferente do ID da requisição."
                });
            }

            await _associarPromocaoFaixaHorarioHandler.HandleAsync(
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

    [HttpGet("{promocaoId:guid}/faixas-horario")]
    public async Task<IActionResult> ListarFaixasHorario(
    Guid promocaoId,
    CancellationToken cancellationToken)
    {
        try
        {
            var query = new ListarFaixasHorarioPromocaoQuery(
                promocaoId);

            var response =
                await _listarFaixasHorarioPromocaoHandler.HandleAsync(
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

    [HttpPut("{promocaoId:guid}/faixas-horario/{id:guid}")]
    public async Task<IActionResult> AtualizarFaixaHorario(
    Guid promocaoId,
    Guid id,
    [FromBody] AtualizarPromocaoFaixaHorarioCommand command,
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

            if (promocaoId != command.PromocaoId)
            {
                return BadRequest(new
                {
                    mensagem =
                        "O ID da promoção informado na rota é diferente do ID da requisição."
                });
            }

            await _atualizarPromocaoFaixaHorarioHandler.HandleAsync(
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

    [HttpDelete("{promocaoId:guid}/faixas-horario/{id:guid}")]
    public async Task<IActionResult> RemoverFaixaHorario(
    Guid promocaoId,
    Guid id,
    CancellationToken cancellationToken)
    {
        try
        {
            var command = new RemoverPromocaoFaixaHorarioCommand(
                promocaoId,
                id);

            await _removerPromocaoFaixaHorarioHandler.HandleAsync(
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