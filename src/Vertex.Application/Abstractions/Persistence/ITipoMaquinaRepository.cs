using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Domain.Entities;

namespace Vertex.Application.Abstractions.Persistence
{
    public interface ITipoMaquinaRepository
    {
        Task<TipoMaquina?> ObterPorIdAsync(
            Guid id,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<TipoMaquina>> ListarAsync(
            CancellationToken cancellationToken = default);

        Task<bool> ExistePorNomeAsync(
            string nome,
            CancellationToken cancellationToken = default);

        Task AdicionarAsync(
            TipoMaquina tipoMaquina,
            CancellationToken cancellationToken = default);

        Task SalvarAlteracoesAsync(
            CancellationToken cancellationToken = default);
    }
}
