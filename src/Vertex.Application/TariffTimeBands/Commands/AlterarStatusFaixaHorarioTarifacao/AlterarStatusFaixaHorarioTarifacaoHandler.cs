using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Abstractions.Persistence;

namespace Vertex.Application.TariffTimeBands.Commands.AlterarStatusFaixaHorarioTarifacao
{
    public sealed class AlterarStatusFaixaHorarioTarifacaoHandler
    {
        private readonly IFaixaHorarioTarifacaoRepository _repository;

        public AlterarStatusFaixaHorarioTarifacaoHandler(
            IFaixaHorarioTarifacaoRepository repository)
        {
            _repository = repository;
        }

        public async Task HandleAsync(
            AlterarStatusFaixaHorarioTarifacaoCommand command,
            CancellationToken cancellationToken = default)
        {
            if (!Enum.IsDefined(command.Operacao))
                throw new ArgumentException(
                    "Operação de status inválida.");

            var faixa = await _repository.ObterPorIdAsync(
                command.Id,
                cancellationToken);

            if (faixa is null)
                throw new KeyNotFoundException(
                    "A faixa de horário informada não foi encontrada.");

            switch (command.Operacao)
            {
                case OperacaoFaixaHorarioTarifacao.Ativar:
                    faixa.Ativar();
                    break;

                case OperacaoFaixaHorarioTarifacao.Desativar:
                    faixa.Desativar();
                    break;
            }

            await _repository.SalvarAlteracoesAsync(
                cancellationToken);
        }
    }
}
