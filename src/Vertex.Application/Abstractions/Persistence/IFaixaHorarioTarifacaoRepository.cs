using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Domain.Entities;

namespace Vertex.Application.Abstractions.Persistence
{
    public interface IFaixaHorarioTarifacaoRepository
    {
        Task<FaixaHorarioTarifacao?> ObterPorIdAsync(
            Guid id,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<FaixaHorarioTarifacao>> ListarAsync(
            Guid configuracaoTarifacaoId,
            CancellationToken cancellationToken = default);

        Task<bool> ExisteFaixaAsync(
            Guid configuracaoTarifacaoId,
            int diaSemana,
            TimeSpan horaInicio,
            TimeSpan horaFim,
            Guid? idIgnorar = null,
            CancellationToken cancellationToken = default);

        Task AdicionarAsync(
            FaixaHorarioTarifacao faixa,
            CancellationToken cancellationToken = default);

        Task RemoverAsync(
            FaixaHorarioTarifacao faixa,
            CancellationToken cancellationToken = default);

        Task SalvarAlteracoesAsync(
            CancellationToken cancellationToken = default);
    }
}
