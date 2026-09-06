using Vertex.Application.Abstractions.Persistence;
using Vertex.Application.Promotions.DTOs;

namespace Vertex.Application.Promotions.Queries.ObterPromocao;

public class ObterPromocaoHandler
{
    private readonly IPromocaoRepository _repository;

    public ObterPromocaoHandler(
        IPromocaoRepository repository)
    {
        _repository = repository;
    }

    public async Task<PromocaoResponse?> HandleAsync(
        ObterPromocaoQuery query,
        CancellationToken cancellationToken = default)
    {
        var promocao = await _repository.ObterPorIdAsync(
            query.Id,
            cancellationToken);

        if (promocao is null)
            return null;

        return new PromocaoResponse(
            promocao.Id,
            promocao.Nome,
            promocao.Descricao,
            promocao.PercentualDesconto,
            promocao.ValorDescontoHora,
            promocao.DataInicio,
            promocao.DataFim,
            promocao.Prioridade,
            promocao.TodosTiposMaquina,
            promocao.Ativo,
            promocao.DataCadastro);
    }
}