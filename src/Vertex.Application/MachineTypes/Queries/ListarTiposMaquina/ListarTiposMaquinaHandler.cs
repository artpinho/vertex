using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Abstractions.Persistence;

namespace Vertex.Application.MachineTypes.Queries.ListarTiposMaquina
{
    public sealed class ListarTiposMaquinaHandler
    {
        private readonly ITipoMaquinaRepository _repository;

        public ListarTiposMaquinaHandler(
            ITipoMaquinaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyList<ListarTiposMaquinaResponse>> HandleAsync(
            CancellationToken cancellationToken = default)
        {
            var tipos = await _repository.ListarAsync(
                cancellationToken);

            return tipos
                .Select(x => new ListarTiposMaquinaResponse(
                    x.Id,
                    x.Nome,
                    x.Descricao,
                    x.Ativo,
                    x.DataCadastro))
                .ToList();
        }
    }
}
