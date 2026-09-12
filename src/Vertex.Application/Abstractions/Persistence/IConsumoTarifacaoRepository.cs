using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Domain.Entities;

namespace Vertex.Application.Abstractions.Persistence
{
    public interface IConsumoTarifacaoRepository
    {
        Task AdicionarAsync(
            ConsumoTarifacao consumo,
            CancellationToken cancellationToken = default);

        Task AdicionarListaAsync(
            IReadOnlyList<ConsumoTarifacao> consumos,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<ConsumoTarifacao>> ListarPorSessaoAsync(
            Guid sessaoId,
            CancellationToken cancellationToken = default);

        Task SalvarAlteracoesAsync(
            CancellationToken cancellationToken = default);
    }
}
