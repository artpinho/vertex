using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Domain.Entities;
using Vertex.Domain.Enums;

namespace Vertex.Domain.Tests.Entities
{
    public class ComputadorTests
    {
        [Fact]
        public void Computador_Deve_Iniciar_Offline()
        {
            // Arrange
            var computador = new Computador("PC-001");

            // Assert
            Assert.Equal(StatusComputador.Offline, computador.Status);
        }

        [Fact]
        public void Heartbeat_Deve_Deixar_Computador_Online()
        {
            // Arrange
            var computador = new Computador("PC-001");

            // Act
            computador.AtualizarHeartbeat(
                "192.168.0.101",
                "00:11:22:33:44:55",
                "Windows 11",
                "1.0.0");

            // Assert
            Assert.Equal(StatusComputador.Online, computador.Status);
            Assert.NotNull(computador.UltimoHeartbeat);

            Assert.Equal(
                "192.168.0.101",
                computador.Ip);

            Assert.Equal(
                "00:11:22:33:44:55",
                computador.MacAddress);

            Assert.Equal(
                "Windows 11",
                computador.SistemaOperacional);

            Assert.Equal(
                "1.0.0",
                computador.ClienteVersao);
        }

        [Fact]
        public void Deve_Permitir_Bloquear_Computador()
        {
            // Arrange
            var computador = new Computador("PC-001");

            // Act
            computador.Bloquear();

            // Assert
            Assert.Equal(StatusComputador.Bloqueado, computador.Status);
        }

        [Fact]
        public void Heartbeat_Deve_Atualizar_UltimoHeartbeat()
        {
            // Arrange
            var computador = new Computador("PC-001");

            var antes = DateTime.UtcNow;

            // Act
            computador.AtualizarHeartbeat(
                "192.168.0.101",
                "00:11:22:33:44:55",
                "Windows 11",
                "1.0.0");

            var depois = DateTime.UtcNow;

            // Assert
            Assert.NotNull(computador.UltimoHeartbeat);

            Assert.InRange(
                computador.UltimoHeartbeat.Value,
                antes,
                depois);
        }
    }
}
