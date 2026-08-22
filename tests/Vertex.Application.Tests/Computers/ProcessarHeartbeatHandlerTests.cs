using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Computers.Commands.ProcessarHeartbeat;
using Vertex.Application.Computers.Commands.RegistrarComputador;
using Vertex.Application.Tests.Fakes;
using Vertex.Domain.Enums;

namespace Vertex.Application.Tests.Computers;

public class ProcessarHeartbeatHandlerTests
{
    [Fact]
    public async Task Deve_Processar_Heartbeat()
    {
        // Arrange
        var repository = new FakeComputadorRepository();

        var computadorHandler =
            new RegistrarComputadorHandler(repository);

        var computador =
            await computadorHandler.HandleAsync(
                new RegistrarComputadorCommand(
                    "PC-001",
                    "192.168.0.101",
                    "00:11:22:33:44:55",
                    "Windows 11",
                    "1.0.0"));

        var heartbeatHandler =
            new ProcessarHeartbeatHandler(repository);

        // Act
        var heartbeat =
            await heartbeatHandler.HandleAsync(
                new ProcessarHeartbeatCommand(
                    computador.Id,
                    "PC-001",
                    "192.168.0.101",
                    "00:11:22:33:44:55",
                    "Windows 11",
                    "1.0.1",
                    30,
                    50,
                    70));

        // Assert
        Assert.NotEqual(default, heartbeat);

        var entidade =
            await repository.ObterPorIdAsync(computador.Id);

        Assert.NotNull(entidade);

        Assert.Equal(
            StatusComputador.Online,
            entidade.Status);

        Assert.NotNull(
            entidade.UltimoHeartbeat);

        Assert.Equal(
            "1.0.1",
            entidade.ClienteVersao);
    }

    [Fact]
    public async Task Deve_Falhar_Quando_Computador_Nao_Existe()
    {
        // Arrange
        var repository = new FakeComputadorRepository();

        var handler =
            new ProcessarHeartbeatHandler(repository);

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(
            () =>
                handler.HandleAsync(
                    new ProcessarHeartbeatCommand(
                        Guid.NewGuid(),
                        "PC-001",
                        "192.168.0.101",
                        null,
                        "Windows 11",
                        "1.0.0",
                        null,
                        null,
                        null)));
    }
}