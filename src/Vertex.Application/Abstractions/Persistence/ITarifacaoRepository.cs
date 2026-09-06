using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Domain.Entities;

namespace Vertex.Application.Abstractions.Persistence
{
    public interface ITarifacaoRepository
    {
        Task<ConfiguracaoTarifacao?> ObterPorIdAsync(
            Guid id,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<ConfiguracaoTarifacao>> ListarAsync(
            CancellationToken cancellationToken = default);

        Task<bool> ExistePorNomeAsync(
            string nome,
            Guid tipoMaquinaId,
            CancellationToken cancellationToken = default);

        Task AdicionarAsync(
            ConfiguracaoTarifacao configuracao,
            CancellationToken cancellationToken = default);

        Task SalvarAlteracoesAsync(
            CancellationToken cancellationToken = default);
    }
}
