using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Domain.Entities;

namespace Vertex.Application.Abstractions.Persistence
{
    public interface ISessaoRepository
    {
        Task<Sessao?> ObterPorIdAsync(
            Guid id,
            CancellationToken cancellationToken);

        Task<IReadOnlyList<Sessao>> ListarAsync(
            CancellationToken cancellationToken);

        Task<bool> ExisteSessaoAtivaPorClienteAsync(
            Guid clienteId,
            CancellationToken cancellationToken);

        Task<bool> ExisteSessaoAtivaPorEstacaoAsync(
            Guid estacaoId,
            CancellationToken cancellationToken);

        Task AdicionarAsync(
            Sessao sessao,
            CancellationToken cancellationToken);

        Task SalvarAlteracoesAsync(
            CancellationToken cancellationToken);
    }
}
