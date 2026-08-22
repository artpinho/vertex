using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Abstractions.Persistence;
using Vertex.Application.Computers.DTOs;

namespace Vertex.Application.Computers.Queries
{
    public sealed class ListarComputadoresHandler
    {
        private readonly IComputadorRepository _repository;

        public ListarComputadoresHandler(
            IComputadorRepository repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyList<ComputadorResponse>> HandleAsync(
            ListarComputadoresQuery query,
            CancellationToken cancellationToken = default)
        {
            var computadores = await _repository.ListarAsync(
                cancellationToken);

            return computadores
                .Select(x => new ComputadorResponse(
                    x.Id,
                    x.HostName,
                    x.Ip,
                    x.MacAddress,
                    x.SistemaOperacional,
                    x.ClienteVersao,
                    x.UltimoHeartbeat,
                    x.Status))
                .ToList();
        }
    }
}
