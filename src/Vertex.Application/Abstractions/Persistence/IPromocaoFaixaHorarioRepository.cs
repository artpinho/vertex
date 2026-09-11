using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Domain.Entities;

namespace Vertex.Application.Abstractions.Persistence
{
    public interface IPromocaoFaixaHorarioRepository
    {
        Task<IReadOnlyList<PromocaoFaixaHorario>> ListarAsync(
            Guid promocaoId,
            CancellationToken cancellationToken = default);

        Task<PromocaoFaixaHorario?> ObterPorIdAsync(
            Guid id,
            CancellationToken cancellationToken = default);

        Task<bool> ExisteSobreposicaoAsync(
            Guid promocaoId,
            TimeSpan horaInicio,
            TimeSpan horaFim,
            Guid? idIgnorar = null,
            CancellationToken cancellationToken = default);

        Task AdicionarAsync(
            PromocaoFaixaHorario faixa,
            CancellationToken cancellationToken = default);

        Task RemoverAsync(
            PromocaoFaixaHorario faixa,
            CancellationToken cancellationToken = default);

        Task SalvarAlteracoesAsync(
            CancellationToken cancellationToken = default);
    }
}
