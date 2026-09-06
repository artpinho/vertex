using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Abstractions.Persistence;

namespace Vertex.Application.TariffConfigurations.Queries.ListarConfiguracoesTarifacao
{
    public sealed class ListarConfiguracoesTarifacaoHandler
    {
        private readonly ITarifacaoRepository _repository;

        public ListarConfiguracoesTarifacaoHandler(
            ITarifacaoRepository repository)
        {
            _repository = repository;
        }

        public async Task<
            IReadOnlyList<ListarConfiguracoesTarifacaoResult>> HandleAsync(
            CancellationToken cancellationToken = default)
        {
            var configuracoes = await _repository.ListarAsync(
                cancellationToken);

            return configuracoes
                .Select(x => new ListarConfiguracoesTarifacaoResult(
                    x.Id,
                    x.Nome,
                    x.Descricao,
                    x.TipoMaquinaId,
                    x.ValorHora,
                    x.DataInicio,
                    x.DataFim,
                    x.Prioridade,
                    x.Ativo,
                    x.DataCadastro))
                .ToList();
        }
    }
}
