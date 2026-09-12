using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Tariffing.Models;

namespace Vertex.Application.Tariffing.Services
{
    public interface ITarifacaoProvider
    {
        Task<ConfiguracaoTarifacaoAplicavel?> ObterConfiguracaoAsync(
            Guid tipoMaquinaId,
            DateTime momento,
            CancellationToken cancellationToken = default);

        Task<decimal?> ObterValorHoraFaixaAsync(
            Guid configuracaoTarifacaoId,
            int diaSemana,
            TimeSpan horario,
            CancellationToken cancellationToken = default);

        Task<RegraTarifacao?> ObterPromocaoAsync(
            Guid tipoMaquinaId,
            DateTime momento,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<DateTime>> ObterPontosDeQuebraAsync(
            Guid tipoMaquinaId,
            DateTime inicio,
            DateTime fim,
            CancellationToken cancellationToken = default);
    }
}
