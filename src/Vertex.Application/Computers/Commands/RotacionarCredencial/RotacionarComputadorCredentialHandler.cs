using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Abstractions.Persistence;
using Vertex.Application.Abstractions.Security;
using Vertex.Domain.Entities;

namespace Vertex.Application.Computers.Commands.RotacionarCredential;

public sealed class RotacionarComputadorCredentialHandler
{
    private readonly IComputadorRepository _computadorRepository;
    private readonly IComputadorCredentialRepository _credentialRepository;
    private readonly IComputerCredentialGenerator _generator;

    public RotacionarComputadorCredentialHandler(
        IComputadorRepository computadorRepository,
        IComputadorCredentialRepository credentialRepository,
        IComputerCredentialGenerator generator)
    {
        _computadorRepository = computadorRepository;
        _credentialRepository = credentialRepository;
        _generator = generator;
    }

    public async Task<RotacionarComputadorCredentialResponse> HandleAsync(
        RotacionarComputadorCredentialCommand command,
        CancellationToken cancellationToken = default)
    {
        var computador =
            await _computadorRepository.ObterPorIdAsync(
                command.ComputadorId,
                cancellationToken);

        if (computador is null)
        {
            throw new KeyNotFoundException(
                "Computador não encontrado.");
        }

        var credentialAtual =
            await _credentialRepository
                .ObterAtivaPorComputadorIdAsync(
                    computador.Id,
                    cancellationToken);

        if (credentialAtual is null)
        {
            throw new InvalidOperationException(
                "O computador não possui uma credencial ativa.");
        }

        credentialAtual.Revogar();

        var clientId = _generator.GenerateClientId();
        var clientSecret = _generator.GenerateClientSecret();
        var secretHash = _generator.HashSecret(clientSecret);

        var novaCredential = new ComputadorCredential(
            computador.Id,
            clientId,
            secretHash);

        await _credentialRepository.SalvarRotacaoAsync(
            credentialAtual,
            novaCredential,
            cancellationToken);

        return new RotacionarComputadorCredentialResponse(
            computador.Id,
            clientId,
            clientSecret);
    }
}
