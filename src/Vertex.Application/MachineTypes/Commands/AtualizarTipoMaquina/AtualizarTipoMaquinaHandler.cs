using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Abstractions.Persistence;

namespace Vertex.Application.MachineTypes.Commands.AtualizarTipoMaquina
{
    public sealed class AtualizarTipoMaquinaHandler
    {
        private readonly ITipoMaquinaRepository _repository;

        public AtualizarTipoMaquinaHandler(
            ITipoMaquinaRepository repository)
        {
            _repository = repository;
        }

        public async Task<AtualizarTipoMaquinaResponse> HandleAsync(
            AtualizarTipoMaquinaCommand command,
            CancellationToken cancellationToken = default)
        {
            if (command.TipoMaquinaId == Guid.Empty)
                throw new ArgumentException(
                    "O identificador do tipo de máquina é obrigatório.");

            if (string.IsNullOrWhiteSpace(command.Nome))
                throw new ArgumentException(
                    "O nome do tipo de máquina é obrigatório.");

            var tipoMaquina = await _repository.ObterPorIdAsync(
                command.TipoMaquinaId,
                cancellationToken);

            if (tipoMaquina is null)
                throw new KeyNotFoundException(
                    "Tipo de máquina não encontrado.");

            var nome = command.Nome.Trim();

            if (!string.Equals(
                    tipoMaquina.Nome,
                    nome,
                    StringComparison.OrdinalIgnoreCase))
            {
                if (await _repository.ExistePorNomeAsync(
                        nome,
                        cancellationToken))
                {
                    throw new InvalidOperationException(
                        "Já existe um tipo de máquina com esse nome.");
                }
            }

            tipoMaquina.Atualizar(
                nome,
                command.Descricao);

            await _repository.SalvarAlteracoesAsync(
                cancellationToken);

            return new AtualizarTipoMaquinaResponse(
                tipoMaquina.Id,
                tipoMaquina.Nome,
                tipoMaquina.Descricao,
                tipoMaquina.Ativo,
                tipoMaquina.DataCadastro);
        }
    }
}
