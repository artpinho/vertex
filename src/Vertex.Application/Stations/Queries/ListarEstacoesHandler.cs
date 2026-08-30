using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Abstractions.Persistence;

namespace Vertex.Application.Stations.Queries
{
    public sealed class ListarEstacoesHandler
    {
        private readonly IEstacaoRepository _repository;

        public ListarEstacoesHandler(
            IEstacaoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyList<EstacaoResponse>> HandleAsync(
            CancellationToken cancellationToken)
        {
            var estacoes =
                await _repository.ListarAsync(
                    cancellationToken);

            return estacoes
                .Select(estacao => new EstacaoResponse(
                    estacao.Id,
                    estacao.Nome,
                    estacao.Numero,
                    estacao.Status,
                    estacao.Ativa,
                    estacao.ComputadorId))
                .ToList();
        }
    }
}
