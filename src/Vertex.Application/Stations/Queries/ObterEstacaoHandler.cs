using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Abstractions.Persistence;

namespace Vertex.Application.Stations.Queries
{
    public sealed class ObterEstacaoHandler
    {
        private readonly IEstacaoRepository _repository;

        public ObterEstacaoHandler(
            IEstacaoRepository repository)
        {
            _repository = repository;
        }

        public async Task<EstacaoResponse?> HandleAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            var estacao =
                await _repository.ObterPorIdAsync(
                    id,
                    cancellationToken);

            if (estacao is null)
                return null;

            return new EstacaoResponse(
                estacao.Id,
                estacao.Nome,
                estacao.Numero,
                estacao.Status,
                estacao.Ativa,
                estacao.ComputadorId);
        }
    }
}
