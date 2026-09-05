using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Abstractions.Persistence;

namespace Vertex.Application.Computers.Commands.AlterarStatus
{
    public class AlterarStatusComputadorHandler
    {
        private readonly IComputadorRepository _computadorRepository;

        public AlterarStatusComputadorHandler(
            IComputadorRepository computadorRepository)
        {
            _computadorRepository = computadorRepository;
        }

        public async Task HandleAsync(
            AlterarStatusComputadorCommand command,
            CancellationToken cancellationToken)
        {
            var computador = await _computadorRepository.ObterPorIdAsync(
                command.ComputadorId,
                cancellationToken);

            if (computador is null)
                throw new KeyNotFoundException(
                    "Computador não encontrado.");

            switch (command.Operacao)
            {
                case OperacaoComputador.MarcarOffline:
                    computador.MarcarOffline();
                    break;

                case OperacaoComputador.Bloquear:
                    computador.Bloquear();
                    break;

                case OperacaoComputador.ColocarEmManutencao:
                    computador.ColocarEmManutencao();
                    break;

                default:
                    throw new ArgumentOutOfRangeException(
                        nameof(command.Operacao),
                        "Operação de status inválida.");
            }

            await _computadorRepository.SalvarAlteracoesAsync(
                cancellationToken);
        }
    }
}
