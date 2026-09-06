using Vertex.Application.Abstractions.Persistence;
using Vertex.Application.Promotions.DTOs;

namespace Vertex.Application.Promotions.Queries.ListarPromocoes;

public class ListarPromocoesHandler
{
    private readonly IPromocaoRepository _repository;

    public ListarPromocoesHandler(
        IPromocaoRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<PromocaoResponse>> HandleAsync(
        ListarPromocoesQuery query,
        CancellationToken cancellationToken = default)
    {
        var promocoes = await _repository.ListarAsync(
            cancellationToken);

        return promocoes
            .Select(x => new PromocaoResponse(
                x.Id,
                x.Nome,
                x.Descricao,
                x.PercentualDesconto,
                x.ValorDescontoHora,
                x.DataInicio,
                x.DataFim,
                x.Prioridade,
                x.TodosTiposMaquina,
                x.Ativo,
                x.DataCadastro))
            .ToList();
    }
}