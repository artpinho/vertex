using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Abstractions.Persistence;
using Vertex.Application.Abstractions.Security;
using Vertex.Domain.Entities;

namespace Vertex.Application.Computers.Commands.ProvisionarCredential;

public sealed class ProvisionarComputadorCredentialHandler
{
    private readonly IComputadorRepository _computadorRepository;
    private readonly IComputadorCredentialRepository _credentialRepository;
    private readonly IComputerCredentialGenerator _generator;

    public ProvisionarComputadorCredentialHandler(
        IComputadorRepository computadorRepository,
        IComputadorCredentialRepository credentialRepository,
        IComputerCredentialGenerator generator)
    {
        _computadorRepository = computadorRepository;
        _credentialRepository = credentialRepository;
        _generator = generator;
    }

    public async Task<ProvisionarComputadorCredentialResponse> HandleAsync(
        ProvisionarComputadorCredentialCommand command,
        CancellationToken cancellationToken = default)
    {
        var computador = await _computadorRepository.ObterPorIdAsync(
            command.ComputadorId,
            cancellationToken);

        if (computador is null)
        {
            throw new KeyNotFoundException(
                "Computador não encontrado.");
        }

        var credentialExistente =
            await _credentialRepository.ObterPorComputadorIdAsync(
                command.ComputadorId,
                cancellationToken);

        if (credentialExistente is not null)
        {
            throw new InvalidOperationException(
                "O computador já possui uma credencial.");
        }

        var clientId = _generator.GenerateClientId();
        var clientSecret = _generator.GenerateClientSecret();
        var secretHash = _generator.HashSecret(clientSecret);

        var credential = new ComputadorCredential(
            computador.Id,
            clientId,
            secretHash);

        computador.AssociarCredential(credential);

        await _credentialRepository.AdicionarAsync(
            credential,
            cancellationToken);

        return new ProvisionarComputadorCredentialResponse(
            computador.Id,
            clientId,
            clientSecret);
    }
}
