using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Domain.Entities;

namespace Vertex.Application.Abstractions.Persistence
{
    public interface IPromocaoTipoMaquinaRepository
    {
        Task<bool> ExisteAsync(
            Guid promocaoId,
            Guid tipoMaquinaId,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<PromocaoTipoMaquina>> ListarAsync(
            Guid promocaoId,
            CancellationToken cancellationToken = default);

        Task<PromocaoTipoMaquina?> ObterAsync(
            Guid promocaoId,
            Guid tipoMaquinaId,
            CancellationToken cancellationToken = default);

        Task AdicionarAsync(
            PromocaoTipoMaquina associacao,
            CancellationToken cancellationToken = default);

        Task RemoverAsync(
            PromocaoTipoMaquina associacao,
            CancellationToken cancellationToken = default);

        Task SalvarAlteracoesAsync(
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<PromocaoTipoMaquina>> ListarComTipoMaquinaAsync(
            Guid promocaoId,
            CancellationToken cancellationToken = default);
    }
}
