using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Abstractions.Persistence;

namespace Vertex.Application.Promotions.Commands.RemoverPromocaoTipoMaquina
{
    public class RemoverPromocaoTipoMaquinaHandler
    {
        private readonly IPromocaoTipoMaquinaRepository _repository;

        public RemoverPromocaoTipoMaquinaHandler(
            IPromocaoTipoMaquinaRepository repository)
        {
            _repository = repository;
        }

        public async Task HandleAsync(
            RemoverPromocaoTipoMaquinaCommand command,
            CancellationToken cancellationToken = default)
        {
            var associacao = await _repository.ObterAsync(
                command.PromocaoId,
                command.TipoMaquinaId,
                cancellationToken);

            if (associacao is null)
                throw new KeyNotFoundException(
                    "A associação entre a promoção e o tipo de máquina não foi encontrada.");

            await _repository.RemoverAsync(
                associacao,
                cancellationToken);

            await _repository.SalvarAlteracoesAsync(
                cancellationToken);
        }
    }
}
