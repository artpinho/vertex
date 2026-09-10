using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Domain.Entities;

namespace Vertex.Application.Abstractions.Persistence
{
    public interface IPromocaoDiaSemanaRepository
    {
        Task<IReadOnlyList<PromocaoDiaSemana>> ListarAsync(
            Guid promocaoId,
            CancellationToken cancellationToken = default);

        Task<bool> ExisteAsync(
            Guid promocaoId,
            int diaSemana,
            CancellationToken cancellationToken = default);

        Task AdicionarAsync(
            PromocaoDiaSemana promocaoDiaSemana,
            CancellationToken cancellationToken = default);

        Task RemoverAsync(
            PromocaoDiaSemana promocaoDiaSemana,
            CancellationToken cancellationToken = default);

        Task<PromocaoDiaSemana?> ObterAsync(
            Guid promocaoId,
            int diaSemana,
            CancellationToken cancellationToken = default);

        Task SalvarAlteracoesAsync(
            CancellationToken cancellationToken = default);
    }
}
