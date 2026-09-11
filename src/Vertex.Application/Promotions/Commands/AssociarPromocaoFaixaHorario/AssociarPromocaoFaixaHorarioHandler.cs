using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Abstractions.Persistence;
using Vertex.Domain.Entities;

namespace Vertex.Application.Promotions.Commands.AssociarPromocaoFaixaHorario
{
    public class AssociarPromocaoFaixaHorarioHandler
    {
        private readonly IPromocaoRepository _promocaoRepository;
        private readonly IPromocaoFaixaHorarioRepository _repository;

        public AssociarPromocaoFaixaHorarioHandler(
            IPromocaoRepository promocaoRepository,
            IPromocaoFaixaHorarioRepository repository)
        {
            _promocaoRepository = promocaoRepository;
            _repository = repository;
        }

        public async Task HandleAsync(
            AssociarPromocaoFaixaHorarioCommand command,
            CancellationToken cancellationToken = default)
        {
            if (command.PromocaoId == Guid.Empty)
                throw new ArgumentException(
                    "A promoção informada é inválida.");

            if (command.HoraInicio >= command.HoraFim)
                throw new ArgumentException(
                    "A hora de início deve ser menor que a hora de fim.");

            var promocao = await _promocaoRepository.ObterPorIdAsync(
                command.PromocaoId,
                cancellationToken);

            if (promocao is null)
                throw new KeyNotFoundException(
                    "A promoção informada não foi encontrada.");

            if (!promocao.Ativo)
                throw new InvalidOperationException(
                    "Não é possível associar uma faixa a uma promoção inativa.");

            var existeSobreposicao =
                await _repository.ExisteSobreposicaoAsync(
                    command.PromocaoId,
                    command.HoraInicio,
                    command.HoraFim,
                    cancellationToken: cancellationToken);

            if (existeSobreposicao)
                throw new InvalidOperationException(
                    "A faixa de horário informada possui sobreposição com outra faixa da promoção.");

            var faixa = new PromocaoFaixaHorario(
                command.PromocaoId,
                command.HoraInicio,
                command.HoraFim);

            await _repository.AdicionarAsync(
                faixa,
                cancellationToken);

            await _repository.SalvarAlteracoesAsync(
                cancellationToken);
        }
    }
}
