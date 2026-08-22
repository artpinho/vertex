using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Domain.Entities;


namespace Vertex.Application.Abstractions.Persistence
{
    public interface IComputadorRepository
    {
        Task<bool> ExistePorHostNameAsync(
        string hostName,
        CancellationToken cancellationToken = default);

        Task<bool> ExistePorMacAddressAsync(
            string macAddress,
            CancellationToken cancellationToken = default);

        Task AdicionarAsync(
            Computador computador,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Computador>> ListarAsync(
            CancellationToken cancellationToken = default);

        Task<Computador?> ObterPorIdAsync(
            Guid id,
            CancellationToken cancellationToken = default);

        Task AtualizarAsync(
            Computador computador,
            CancellationToken cancellationToken = default);
    }
}
