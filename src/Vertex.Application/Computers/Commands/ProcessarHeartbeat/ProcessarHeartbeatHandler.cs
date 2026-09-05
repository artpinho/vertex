using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Abstractions.Persistence;
using Vertex.Domain.Entities;

namespace Vertex.Application.Computers.Commands.ProcessarHeartbeat
{
    public sealed class ProcessarHeartbeatHandler
    {
        private readonly IComputadorRepository _repository;

        public ProcessarHeartbeatHandler(
            IComputadorRepository repository)
        {
            _repository = repository;
        }

        public async Task<DateTime> HandleAsync(
            ProcessarHeartbeatCommand command,
            CancellationToken cancellationToken = default)
        {
            if (command.ComputadorId == Guid.Empty)
                throw new ArgumentException(
                    "O identificador do computador é obrigatório.");

            var computador = await _repository.ObterPorIdAsync(
                command.ComputadorId,
                cancellationToken);

            if (computador is null)
                throw new KeyNotFoundException(
                    "Computador não encontrado.");

            computador.AtualizarHeartbeat(
                command.Ip,
                command.MacAddress,
                command.SistemaOperacional,
                command.ClienteVersao);

            await _repository.SalvarAlteracoesAsync(
                cancellationToken);

            return computador.UltimoHeartbeat!.Value;
        }
    }
}
