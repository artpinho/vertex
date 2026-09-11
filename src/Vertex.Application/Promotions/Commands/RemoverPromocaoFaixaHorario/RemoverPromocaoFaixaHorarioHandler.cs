using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Abstractions.Persistence;

namespace Vertex.Application.Promotions.Commands.RemoverPromocaoFaixaHorario
{
    public class RemoverPromocaoFaixaHorarioHandler
    {
        private readonly IPromocaoFaixaHorarioRepository _repository;

        public RemoverPromocaoFaixaHorarioHandler(
            IPromocaoFaixaHorarioRepository repository)
        {
            _repository = repository;
        }

        public async Task HandleAsync(
            RemoverPromocaoFaixaHorarioCommand command,
            CancellationToken cancellationToken = default)
        {
            if (command.PromocaoId == Guid.Empty)
                throw new ArgumentException(
                    "A promoção informada é inválida.");

            if (command.FaixaHorarioId == Guid.Empty)
                throw new ArgumentException(
                    "A faixa de horário informada é inválida.");

            var faixa = await _repository.ObterPorIdAsync(
                command.FaixaHorarioId,
                cancellationToken);

            if (faixa is null ||
                faixa.PromocaoId != command.PromocaoId)
            {
                throw new KeyNotFoundException(
                    "A faixa de horário informada não foi encontrada para esta promoção.");
            }

            await _repository.RemoverAsync(
                faixa,
                cancellationToken);

            await _repository.SalvarAlteracoesAsync(
                cancellationToken);
        }
    }
}
