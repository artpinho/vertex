using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Abstractions.Persistence;

namespace Vertex.Application.TariffConfigurations.Commands.AlterarStatusConfiguracaoTarifacao
{
    public sealed class AlterarStatusConfiguracaoTarifacaoHandler
    {
        private readonly ITarifacaoRepository _repository;

        public AlterarStatusConfiguracaoTarifacaoHandler(
            ITarifacaoRepository repository)
        {
            _repository = repository;
        }

        public async Task HandleAsync(
            AlterarStatusConfiguracaoTarifacaoCommand command,
            CancellationToken cancellationToken = default)
        {
            if (!Enum.IsDefined(command.Operacao))
            {
                throw new ArgumentException(
                    "Operação de status inválida.");
            }

            var configuracao = await _repository.ObterPorIdAsync(
                command.Id,
                cancellationToken);

            if (configuracao is null)
                throw new KeyNotFoundException(
                    "Configuração de tarifação não encontrada.");

            switch (command.Operacao)
            {
                case OperacaoConfiguracaoTarifacao.Ativar:
                    configuracao.Ativar();
                    break;

                case OperacaoConfiguracaoTarifacao.Desativar:
                    configuracao.Desativar();
                    break;

                default:
                    throw new ArgumentException(
                        "Operação de status inválida.");
            }

            await _repository.SalvarAlteracoesAsync(
                cancellationToken);
        }
    }
}
