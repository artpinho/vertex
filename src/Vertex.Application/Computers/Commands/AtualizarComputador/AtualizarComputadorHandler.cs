using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Abstractions.Persistence;
using Vertex.Application.Computers.DTOs;

namespace Vertex.Application.Computers.Commands.AtualizarComputador
{
    public class AtualizarComputadorHandler
    {
        private readonly IComputadorRepository _computadorRepository;

        public AtualizarComputadorHandler(
            IComputadorRepository computadorRepository)
        {
            _computadorRepository = computadorRepository;
        }

        public async Task<ComputadorResponse?> HandleAsync(
            AtualizarComputadorCommand command,
            CancellationToken cancellationToken)
        {
            var computador = await _computadorRepository.ObterPorIdAsync(
                command.ComputadorId,
                cancellationToken);

            if (computador is null)
                return null;

            computador.AtualizarInformacoes(
                command.Ip,
                command.MacAddress,
                command.SistemaOperacional,
                command.ClienteVersao);

            await _computadorRepository.SalvarAlteracoesAsync(
                cancellationToken);

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
