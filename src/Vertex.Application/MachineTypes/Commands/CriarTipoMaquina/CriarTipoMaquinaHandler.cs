using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Abstractions.Persistence;
using Vertex.Domain.Entities;

namespace Vertex.Application.MachineTypes.Commands.CriarTipoMaquina
{
    public sealed class CriarTipoMaquinaHandler
    {
        private readonly ITipoMaquinaRepository _repository;

        public CriarTipoMaquinaHandler(
            ITipoMaquinaRepository repository)
        {
            _repository = repository;
        }

        public async Task<CriarTipoMaquinaResponse> HandleAsync(
            CriarTipoMaquinaCommand command,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(command.Nome))
                throw new ArgumentException(
                    "O nome do tipo de máquina é obrigatório.");

            var nome = command.Nome.Trim();

            if (await _repository.ExistePorNomeAsync(
                    nome,
                    cancellationToken))
            {
                throw new InvalidOperationException(
                    "Já existe um tipo de máquina com esse nome.");
            }

            var tipoMaquina = new TipoMaquina(
                nome,
                command.Descricao);

            await _repository.AdicionarAsync(
                tipoMaquina,
                cancellationToken);

            await _repository.SalvarAlteracoesAsync(
                cancellationToken);

            return new CriarTipoMaquinaResponse(
                tipoMaquina.Id,
                tipoMaquina.Nome,
                tipoMaquina.Descricao,
                tipoMaquina.Ativo,
                tipoMaquina.DataCadastro);
        }
    }
}
