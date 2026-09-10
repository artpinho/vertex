using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Abstractions.Persistence;
using Vertex.Domain.Entities;

namespace Vertex.Application.Promotions.Commands.AssociarPromocaoDiaSemana
{
    public class AssociarPromocaoDiaSemanaHandler
    {
        private readonly IPromocaoRepository _promocaoRepository;
        private readonly IPromocaoDiaSemanaRepository _repository;

        public AssociarPromocaoDiaSemanaHandler(
            IPromocaoRepository promocaoRepository,
            IPromocaoDiaSemanaRepository repository)
        {
            _promocaoRepository = promocaoRepository;
            _repository = repository;
        }

        public async Task HandleAsync(
            AssociarPromocaoDiaSemanaCommand command,
            CancellationToken cancellationToken = default)
        {
            if (command.PromocaoId == Guid.Empty)
                throw new ArgumentException(
                    "A promoção informada é inválida.");

            if (command.DiaSemana < 1 || command.DiaSemana > 7)
                throw new ArgumentException(
                    "O dia da semana deve estar entre 1 e 7.");

            var promocao = await _promocaoRepository.ObterPorIdAsync(
                command.PromocaoId,
                cancellationToken);

            if (promocao is null)
                throw new KeyNotFoundException(
                    "A promoção informada não foi encontrada.");

            if (!promocao.Ativo)
                throw new InvalidOperationException(
                    "Não é possível associar um dia a uma promoção inativa.");

            var existe = await _repository.ExisteAsync(
                command.PromocaoId,
                command.DiaSemana,
                cancellationToken);

            if (existe)
                throw new InvalidOperationException(
                    "O dia da semana já está associado à promoção.");

            var promocaoDiaSemana = new PromocaoDiaSemana(
                command.PromocaoId,
                command.DiaSemana);

            await _repository.AdicionarAsync(
                promocaoDiaSemana,
                cancellationToken);

            await _repository.SalvarAlteracoesAsync(
                cancellationToken);
        }
    }
}
