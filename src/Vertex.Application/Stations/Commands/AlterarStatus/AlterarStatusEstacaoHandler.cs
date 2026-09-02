using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Abstractions.Persistence;

namespace Vertex.Application.Stations.Commands.AlterarStatus
{
    public sealed class AlterarStatusEstacaoHandler
    {
        private readonly IEstacaoRepository _repository;

        public AlterarStatusEstacaoHandler(
            IEstacaoRepository repository)
        {
            _repository = repository;
        }

        public async Task HandleAsync(
            AlterarStatusEstacaoCommand command,
            CancellationToken cancellationToken)
        {
            var estacao =
                await _repository.ObterPorIdAsync(
                    command.EstacaoId,
                    cancellationToken);

            if (estacao is null)
            {
                throw new KeyNotFoundException(
                    "Estação não encontrada.");
            }

            switch (command.Operacao)
            {
                case OperacaoEstacao.Liberar:
                    estacao.Liberar();
                    break;

                case OperacaoEstacao.ColocarEmUso:
                    estacao.ColocarEmUso();
                    break;

                case OperacaoEstacao.Bloquear:
                    estacao.Bloquear();
                    break;

                case OperacaoEstacao.ColocarEmManutencao:
                    estacao.ColocarEmManutencao();
                    break;

                case OperacaoEstacao.Ativar:
                    estacao.Ativar();
                    break;

                case OperacaoEstacao.Desativar:
                    estacao.Desativar();
                    break;

                default:
                    throw new ArgumentOutOfRangeException(
                        nameof(command.Operacao),
                        "Operação de estação inválida.");
            }

            await _repository.SalvarAlteracoesAsync(
                cancellationToken);
        }
    }
}
