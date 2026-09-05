using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Abstractions.Persistence;

namespace Vertex.Application.MachineTypes.Queries.ObterTipoMaquina
{
    public sealed class ObterTipoMaquinaHandler
    {
        private readonly ITipoMaquinaRepository _repository;

        public ObterTipoMaquinaHandler(
            ITipoMaquinaRepository repository)
        {
            _repository = repository;
        }

        public async Task<ObterTipoMaquinaResponse> HandleAsync(
            ObterTipoMaquinaQuery query,
            CancellationToken cancellationToken = default)
        {
            if (query.Id == Guid.Empty)
                throw new ArgumentException(
                    "O identificador do tipo de máquina é obrigatório.");

            var tipoMaquina = await _repository.ObterPorIdAsync(
                query.Id,
                cancellationToken);

            if (tipoMaquina is null)
                throw new KeyNotFoundException(
                    "Tipo de máquina não encontrado.");

            return new ObterTipoMaquinaResponse(
                tipoMaquina.Id,
                tipoMaquina.Nome,
                tipoMaquina.Descricao,
                tipoMaquina.Ativo,
                tipoMaquina.DataCadastro);
        }
    }
}
