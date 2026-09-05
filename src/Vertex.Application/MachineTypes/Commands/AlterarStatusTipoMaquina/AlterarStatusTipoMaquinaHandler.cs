using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Abstractions.Persistence;

namespace Vertex.Application.MachineTypes.Commands.AlterarStatusTipoMaquina
{
    public sealed class AlterarStatusTipoMaquinaHandler
    {
        private readonly ITipoMaquinaRepository _repository;

        public AlterarStatusTipoMaquinaHandler(
            ITipoMaquinaRepository repository)
        {
            _repository = repository;
        }

        public async Task<AlterarStatusTipoMaquinaResponse> HandleAsync(
            AlterarStatusTipoMaquinaCommand command,
            CancellationToken cancellationToken = default)
        {
            if (command.TipoMaquinaId == Guid.Empty)
                throw new ArgumentException(
                    "O identificador do tipo de máquina é obrigatório.");

            var tipoMaquina = await _repository.ObterPorIdAsync(
                command.TipoMaquinaId,
                cancellationToken);

            if (tipoMaquina is null)
                throw new KeyNotFoundException(
                    "Tipo de máquina não encontrado.");

            if (command.Ativo)
                tipoMaquina.Ativar();
            else
                tipoMaquina.Desativar();

            await _repository.SalvarAlteracoesAsync(
                cancellationToken);

            return new AlterarStatusTipoMaquinaResponse(
                tipoMaquina.Id,
                tipoMaquina.Nome,
                tipoMaquina.Descricao,
                tipoMaquina.Ativo,
                tipoMaquina.DataCadastro);
        }
    }
}
