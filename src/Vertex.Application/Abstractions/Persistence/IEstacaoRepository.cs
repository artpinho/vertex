using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Domain.Entities;

namespace Vertex.Application.Abstractions.Persistence;

public interface IEstacaoRepository
{
    Task<Estacao?> ObterPorIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<IReadOnlyList<Estacao>> ListarAsync(
        CancellationToken cancellationToken);

    Task<bool> ExistePorNumeroAsync(
        int numero,
        CancellationToken cancellationToken);

    Task AdicionarAsync(
        Estacao estacao,
        CancellationToken cancellationToken);

    Task SalvarAlteracoesAsync(
        CancellationToken cancellationToken);

    Task<bool> ExisteComComputadorAsync(
        Guid computadorId,
        Guid estacaoId,
        CancellationToken cancellationToken);
}