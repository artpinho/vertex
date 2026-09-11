using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Abstractions.Persistence;

namespace Vertex.Application.Promotions.Commands.AtualizarPromocaoFaixaHorario
{
    public class AtualizarPromocaoFaixaHorarioHandler
    {
        private readonly IPromocaoRepository _promocaoRepository;
        private readonly IPromocaoFaixaHorarioRepository _repository;

        public AtualizarPromocaoFaixaHorarioHandler(
            IPromocaoRepository promocaoRepository,
            IPromocaoFaixaHorarioRepository repository)
        {
            _promocaoRepository = promocaoRepository;
            _repository = repository;
        }

        public async Task HandleAsync(
            AtualizarPromocaoFaixaHorarioCommand command,
            CancellationToken cancellationToken = default)
        {
            if (command.Id == Guid.Empty)
                throw new ArgumentException(
                    "A faixa de horário informada é inválida.");

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
                    "Não é possível alterar uma faixa de uma promoção inativa.");

            var faixa = await _repository.ObterPorIdAsync(
                command.Id,
                cancellationToken);

            if (faixa is null ||
                faixa.PromocaoId != command.PromocaoId)
            {
                throw new KeyNotFoundException(
                    "A faixa de horário informada não foi encontrada para esta promoção.");
            }

            var existeSobreposicao =
                await _repository.ExisteSobreposicaoAsync(
                    command.PromocaoId,
                    command.HoraInicio,
                    command.HoraFim,
                    command.Id,
                    cancellationToken);

            if (existeSobreposicao)
                throw new InvalidOperationException(
                    "A faixa de horário informada possui sobreposição com outra faixa da promoção.");

            faixa.Atualizar(
                command.HoraInicio,
                command.HoraFim);

            await _repository.SalvarAlteracoesAsync(
                cancellationToken);
        }
    }
}
