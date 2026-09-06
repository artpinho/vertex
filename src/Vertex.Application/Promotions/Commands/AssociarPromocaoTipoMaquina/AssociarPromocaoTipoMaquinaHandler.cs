using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Abstractions.Persistence;
using Vertex.Domain.Entities;

namespace Vertex.Application.Promotions.Commands.AssociarPromocaoTipoMaquina
{
    public class AssociarPromocaoTipoMaquinaHandler
    {
        private readonly IPromocaoRepository _promocaoRepository;
        private readonly ITipoMaquinaRepository _tipoMaquinaRepository;
        private readonly IPromocaoTipoMaquinaRepository _repository;

        public AssociarPromocaoTipoMaquinaHandler(
            IPromocaoRepository promocaoRepository,
            ITipoMaquinaRepository tipoMaquinaRepository,
            IPromocaoTipoMaquinaRepository repository)
        {
            _promocaoRepository = promocaoRepository;
            _tipoMaquinaRepository = tipoMaquinaRepository;
            _repository = repository;
        }

        public async Task HandleAsync(
            AssociarPromocaoTipoMaquinaCommand command,
            CancellationToken cancellationToken = default)
        {
            var promocao = await _promocaoRepository.ObterPorIdAsync(
                command.PromocaoId,
                cancellationToken);

            if (promocao is null)
                throw new KeyNotFoundException(
                    "A promoção informada não foi encontrada.");

            if (!promocao.Ativo)
                throw new InvalidOperationException(
                    "Não é possível associar um tipo de máquina a uma promoção inativa.");

            var tipoMaquina = await _tipoMaquinaRepository.ObterPorIdAsync(
                command.TipoMaquinaId,
                cancellationToken);

            if (tipoMaquina is null)
                throw new KeyNotFoundException(
                    "O tipo de máquina informado não foi encontrado.");

            if (!tipoMaquina.Ativo)
                throw new InvalidOperationException(
                    "Não é possível associar um tipo de máquina inativo a uma promoção.");

            if (promocao.TodosTiposMaquina)
                throw new InvalidOperationException(
                    "A promoção está configurada para todos os tipos de máquina.");

            var existe = await _repository.ExisteAsync(
                command.PromocaoId,
                command.TipoMaquinaId,
                cancellationToken);

            if (existe)
                throw new InvalidOperationException(
                    "O tipo de máquina já está associado à promoção.");

            var associacao = new PromocaoTipoMaquina(
                command.PromocaoId,
                command.TipoMaquinaId);

            await _repository.AdicionarAsync(
                associacao,
                cancellationToken);

            await _repository.SalvarAlteracoesAsync(cancellationToken);
        }
    }
}
