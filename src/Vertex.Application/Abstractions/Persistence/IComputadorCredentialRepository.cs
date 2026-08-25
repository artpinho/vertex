using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Domain.Entities;

namespace Vertex.Application.Abstractions.Persistence;

public interface IComputadorCredentialRepository
{
    Task<ComputadorCredential?> ObterAtivaPorComputadorIdAsync(
        Guid computadorId,
        CancellationToken cancellationToken = default);

    Task<ComputadorCredential?> ObterPorClientIdAsync(
        string clientId,
        CancellationToken cancellationToken = default);

    Task AdicionarAsync(
        ComputadorCredential credential,
        CancellationToken cancellationToken = default);

    Task SalvarRotacaoAsync(
            ComputadorCredential credentialAtual,
            ComputadorCredential novaCredential,
            CancellationToken cancellationToken = default);
}