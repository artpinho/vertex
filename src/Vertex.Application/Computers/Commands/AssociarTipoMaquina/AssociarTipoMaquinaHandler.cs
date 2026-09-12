using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Abstractions.Persistence;

namespace Vertex.Application.Computers.Commands.AssociarTipoMaquina
{
    public sealed class AssociarTipoMaquinaHandler
    {
        private readonly IComputadorRepository _computadorRepository;
        private readonly ITipoMaquinaRepository _tipoMaquinaRepository;

        public AssociarTipoMaquinaHandler(
            IComputadorRepository computadorRepository,
            ITipoMaquinaRepository tipoMaquinaRepository)
        {
            _computadorRepository = computadorRepository;
            _tipoMaquinaRepository = tipoMaquinaRepository;
        }

        public async Task HandleAsync(
            AssociarTipoMaquinaCommand command,
            CancellationToken cancellationToken = default)
        {
            var computador = await _computadorRepository.ObterPorIdAsync(
                command.ComputadorId,
                cancellationToken);

            if (computador is null)
                throw new KeyNotFoundException(
                    "Computador não encontrado.");

            var tipoMaquina = await _tipoMaquinaRepository.ObterPorIdAsync(
                command.TipoMaquinaId,
                cancellationToken);

            if (tipoMaquina is null)
                throw new KeyNotFoundException(
                    "Tipo de máquina não encontrado.");

            if (!tipoMaquina.Ativo)
                throw new InvalidOperationException(
                    "Não é possível associar um tipo de máquina inativo.");

            computador.AssociarTipoMaquina(command.TipoMaquinaId);

            await _computadorRepository.SalvarAlteracoesAsync(
                cancellationToken);
        }
    }
}
