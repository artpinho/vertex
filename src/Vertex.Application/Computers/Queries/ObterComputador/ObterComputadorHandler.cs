using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Abstractions.Persistence;
using Vertex.Application.Computers.DTOs;

namespace Vertex.Application.Computers.Queries.ObterComputador
{
    public sealed class ObterComputadorHandler
    {
        private readonly IComputadorRepository _repository;

        public ObterComputadorHandler(
            IComputadorRepository repository)
        {
            _repository = repository;
        }

        public async Task<ComputadorResponse?> HandleAsync(
            ObterComputadorQuery query,
            CancellationToken cancellationToken = default)
        {
            var computador = await _repository.ObterPorIdAsync(
                query.Id,
                cancellationToken);

            if (computador is null)
                return null;

            return new ComputadorResponse(
                computador.Id,
                computador.HostName,
                computador.Ip,
                computador.MacAddress,
                computador.SistemaOperacional,
                computador.ClienteVersao,
                computador.UltimoHeartbeat,
                computador.Status);
        }
    }
}
