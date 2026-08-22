using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Computers.Commands.RegistrarComputador;
using Vertex.Application.Tests.Fakes;

namespace Vertex.Application.Tests.Computers;

public class RegistrarComputadorHandlerTests
{
    [Fact]
    public async Task Deve_Registrar_Computador()
    {
        // Arrange
        var repository = new FakeComputadorRepository();

        var handler = new RegistrarComputadorHandler(
            repository);

        var command = new RegistrarComputadorCommand(
            "PC-001",
            "192.168.0.101",
            "00:11:22:33:44:55",
            "Windows 11",
            "1.0.0");

        // Act
        var response = await handler.HandleAsync(command);

        // Assert
        Assert.NotEqual(Guid.Empty, response.Id);
        Assert.Equal("PC-001", response.HostName);
        Assert.Equal("192.168.0.101", response.Ip);

        Assert.Single(repository.Computadores);
    }

    [Fact]
    public async Task Nao_Deve_Registrar_HostName_Duplicado()
    {
        // Arrange
        var repository = new FakeComputadorRepository();

        var primeiroHandler =
            new RegistrarComputadorHandler(repository);

        await primeiroHandler.HandleAsync(
            new RegistrarComputadorCommand(
                "PC-001",
                "192.168.0.101",
                "00:11:22:33:44:55",
                "Windows 11",
                "1.0.0"));

        var segundoHandler =
            new RegistrarComputadorHandler(repository);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(
            () =>
                segundoHandler.HandleAsync(
                    new RegistrarComputadorCommand(
                        "PC-001",
                        "192.168.0.102",
                        "00:11:22:33:44:56",
                        "Windows 11",
                        "1.0.0")));
    }

    [Fact]
    public async Task Nao_Deve_Registrar_Mac_Duplicado()
    {
        // Arrange
        var repository = new FakeComputadorRepository();

        var handler =
            new RegistrarComputadorHandler(repository);

        await handler.HandleAsync(
            new RegistrarComputadorCommand(
                "PC-001",
                "192.168.0.101",
                "00:11:22:33:44:55",
                "Windows 11",
                "1.0.0"));

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(
            () =>
                handler.HandleAsync(
                    new RegistrarComputadorCommand(
                        "PC-002",
                        "192.168.0.102",
                        "00:11:22:33:44:55",
                        "Windows 11",
                        "1.0.0")));
    }

    [Fact]
    public async Task Nao_Deve_Registrar_Sem_HostName()
    {
        // Arrange
        var repository = new FakeComputadorRepository();

        var handler =
            new RegistrarComputadorHandler(repository);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(
            () =>
                handler.HandleAsync(
                    new RegistrarComputadorCommand(
                        "",
                        null,
                        null,
                        null,
                        null)));
    }
}