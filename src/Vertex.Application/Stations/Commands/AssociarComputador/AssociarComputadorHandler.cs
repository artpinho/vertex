using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Abstractions.Persistence;

namespace Vertex.Application.Stations.Commands.AssociarComputador
{
    public sealed class AssociarComputadorHandler
    {
        private readonly IEstacaoRepository _estacaoRepository;
        private readonly IComputadorRepository _computadorRepository;

        public AssociarComputadorHandler(
            IEstacaoRepository estacaoRepository,
            IComputadorRepository computadorRepository)
        {
            _estacaoRepository = estacaoRepository;
            _computadorRepository = computadorRepository;
        }

        public async Task HandleAsync(
            AssociarComputadorCommand command,
            CancellationToken cancellationToken)
        {
            var estacao =
                await _estacaoRepository.ObterPorIdAsync(
                    command.EstacaoId,
                    cancellationToken);

            if (estacao is null)
            {
                throw new KeyNotFoundException(
                    "Estação não encontrada.");
            }

            var computador =
                await _computadorRepository.ObterPorIdAsync(
                    command.ComputadorId,
                    cancellationToken);

            if (computador is null)
            {
                throw new KeyNotFoundException(
                    "Computador não encontrado.");
            }

            var computadorJaAssociado =
                await _estacaoRepository
                    .ExisteComComputadorAsync(
                        command.ComputadorId,
                        command.EstacaoId,
                        cancellationToken);

            if (computadorJaAssociado)
            {
                throw new InvalidOperationException(
                    "O computador já está associado a outra estação.");
            }

            estacao.AssociarComputador(
                command.ComputadorId);

            await _estacaoRepository
                .SalvarAlteracoesAsync(cancellationToken);
        }
    }
}
