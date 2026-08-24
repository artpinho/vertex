using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Abstractions.Persistence;
using Vertex.Application.Abstractions.Security;

namespace Vertex.Infrastructure.Security;

public sealed class ComputerAuthenticator
    : IComputerAuthenticator
{
    private readonly IComputadorCredentialRepository _repository;
    private readonly IComputerCredentialGenerator _generator;

    public ComputerAuthenticator(
        IComputadorCredentialRepository repository,
        IComputerCredentialGenerator generator)
    {
        _repository = repository;
        _generator = generator;
    }

    public async Task<Guid?> AuthenticateAsync(
        string clientId,
        string clientSecret,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(clientId) ||
            string.IsNullOrWhiteSpace(clientSecret))
        {
            return null;
        }

        var credential =
            await _repository.ObterPorClientIdAsync(
                clientId,
                cancellationToken);

        if (credential is null)
            return null;

        if (!credential.EstaAtiva())
            return null;

        var valido = _generator.VerifySecret(
            clientSecret,
            credential.SecretHash);

        if (!valido)
            return null;

        return credential.ComputadorId;
    }
}