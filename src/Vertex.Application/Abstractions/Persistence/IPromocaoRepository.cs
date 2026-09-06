using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Domain.Entities;

namespace Vertex.Application.Abstractions.Persistence
{
    public interface IPromocaoRepository
    {
        Task<Promocao?> ObterPorIdAsync(
            Guid id,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Promocao>> ListarAsync(
            CancellationToken cancellationToken = default);

        Task AdicionarAsync(
            Promocao promocao,
            CancellationToken cancellationToken = default);

        Task SalvarAlteracoesAsync(
            CancellationToken cancellationToken = default);
    }
}
