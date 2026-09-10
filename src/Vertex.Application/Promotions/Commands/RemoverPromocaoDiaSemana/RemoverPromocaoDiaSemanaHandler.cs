using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Abstractions.Persistence;

namespace Vertex.Application.Promotions.Commands.RemoverPromocaoDiaSemana
{
    public class RemoverPromocaoDiaSemanaHandler
    {
        private readonly IPromocaoDiaSemanaRepository _repository;

        public RemoverPromocaoDiaSemanaHandler(
            IPromocaoDiaSemanaRepository repository)
        {
            _repository = repository;
        }

        public async Task HandleAsync(
            RemoverPromocaoDiaSemanaCommand command,
            CancellationToken cancellationToken = default)
        {
            if (command.PromocaoId == Guid.Empty)
                throw new ArgumentException(
                    "A promoção informada é inválida.");

            if (command.DiaSemana < 1 || command.DiaSemana > 7)
                throw new ArgumentException(
                    "O dia da semana deve estar entre 1 e 7.");

            var promocaoDiaSemana = await _repository.ObterAsync(
                command.PromocaoId,
                command.DiaSemana,
                cancellationToken);

            if (promocaoDiaSemana is null)
                throw new KeyNotFoundException(
                    "O dia da semana não está associado à promoção.");

            await _repository.RemoverAsync(
                promocaoDiaSemana,
                cancellationToken);

            await _repository.SalvarAlteracoesAsync(
                cancellationToken);
        }
    }
}
