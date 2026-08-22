using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Domain.Entities;
using Vertex.Domain.Enums;

namespace Vertex.Domain.Tests.Entities
{
    public class ComputadorCredentialTests
    {
        [Fact]
        public void Deve_Criar_Credencial_Ativa()
        {
            // Arrange
            var computadorId = Guid.NewGuid();

            // Act
            var credential = new ComputadorCredential(
                computadorId,
                "vertex-pc-001",
                "hash-do-secret");

            // Assert
            Assert.NotEqual(Guid.Empty, credential.Id);
            Assert.Equal(
                computadorId,
                credential.ComputadorId);

            Assert.Equal(
                "vertex-pc-001",
                credential.ClientId);

            Assert.Equal(
                "hash-do-secret",
                credential.SecretHash);

            Assert.Equal(
                StatusCredential.Ativa,
                credential.Status);

            Assert.NotEqual(
                default,
                credential.DataCriacao);

            Assert.Null(
                credential.DataRevogacao);
        }

        [Fact]
        public void Deve_Revogar_Credencial()
        {
            // Arrange
            var credential = new ComputadorCredential(
                Guid.NewGuid(),
                "vertex-pc-001",
                "hash-do-secret");

            // Act
            credential.Revogar();

            // Assert
            Assert.Equal(
                StatusCredential.Revogada,
                credential.Status);

            Assert.NotNull(
                credential.DataRevogacao);

            Assert.False(
                credential.EstaAtiva());
        }

        [Fact]
        public void Nao_Deve_Criar_Sem_Computador()
        {
            Assert.Throws<ArgumentException>(() =>
                new ComputadorCredential(
                    Guid.Empty,
                    "vertex-pc-001",
                    "hash-do-secret"));
        }

        [Fact]
        public void Nao_Deve_Criar_Sem_ClientId()
        {
            Assert.Throws<ArgumentException>(() =>
                new ComputadorCredential(
                    Guid.NewGuid(),
                    "",
                    "hash-do-secret"));
        }

        [Fact]
        public void Nao_Deve_Criar_Sem_SecretHash()
        {
            Assert.Throws<ArgumentException>(() =>
                new ComputadorCredential(
                    Guid.NewGuid(),
                    "vertex-pc-001",
                    ""));
        }
    }
}
