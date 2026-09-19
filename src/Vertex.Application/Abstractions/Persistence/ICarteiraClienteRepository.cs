using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Domain.Entities;

namespace Vertex.Application.Abstractions.Persistence
{
    public interface ICarteiraClienteRepository
    {
        Task<CarteiraCliente?> ObterPorClienteIdAsync(
            Guid clienteId,
            CancellationToken cancellationToken = default);

        Task AdicionarAsync(
            CarteiraCliente carteira,
            CancellationToken cancellationToken = default);

        Task AdicionarMovimentacaoAsync(
            MovimentacaoCarteira movimentacao,
            CancellationToken cancellationToken = default);

        Task SalvarAlteracoesAsync(
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<MovimentacaoCarteira>> ListarMovimentacoesAsync(
            Guid carteiraClienteId,
            CancellationToken cancellationToken = default);
    }
}
