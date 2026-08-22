using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Abstractions.Persistence;
using Vertex.Domain.Entities;

namespace Vertex.Application.Tests.Fakes;

public sealed class FakeComputadorRepository : IComputadorRepository
{
    private readonly List<Computador> _computadores = [];

    public IReadOnlyList<Computador> Computadores =>
        _computadores;

    public Task<bool> ExistePorHostNameAsync(
        string hostName,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult(
            _computadores.Any(x =>
                x.HostName.Equals(
                    hostName,
                    StringComparison.OrdinalIgnoreCase)));
    }

    public Task<bool> ExistePorMacAddressAsync(
        string macAddress,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult(
            _computadores.Any(x =>
                x.MacAddress != null &&
                x.MacAddress.Equals(
                    macAddress,
                    StringComparison.OrdinalIgnoreCase)));
    }

    public Task AdicionarAsync(
        Computador computador,
        CancellationToken cancellationToken = default)
    {
        _computadores.Add(computador);

        return Task.CompletedTask;
    }

    public Task<IReadOnlyList<Computador>> ListarAsync(
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult<IReadOnlyList<Computador>>(
            _computadores);
    }

    public Task<Computador?> ObterPorIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult(
            _computadores.FirstOrDefault(x => x.Id == id));
    }

    public Task AtualizarAsync(
        Computador computador,
        CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}