using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Domain.Entities;
using Vertex.Domain.Enums;

namespace Vertex.Domain.Tests.Entities
{
    public class SessaoTests
    {
        [Fact]
        public void Sessao_Deve_Iniciar_Ativa()
        {
            // Arrange
            var clienteId = Guid.NewGuid();
            var estacaoId = Guid.NewGuid();

            // Act
            var sessao = new Sessao(clienteId, estacaoId);

            // Assert
            Assert.Equal(StatusSessao.Ativa, sessao.Status);
            Assert.Equal(clienteId, sessao.ClienteId);
            Assert.Equal(estacaoId, sessao.EstacaoId);
            Assert.Null(sessao.Fim);
        }

        [Fact]
        public void Deve_Encerrar_Sessao_Ativa()
        {
            // Arrange
            var sessao = new Sessao(
                Guid.NewGuid(),
                Guid.NewGuid());

            // Act
            sessao.Encerrar();

            // Assert
            Assert.Equal(StatusSessao.Encerrada, sessao.Status);
            Assert.NotNull(sessao.Fim);
        }

        [Fact]
        public void Nao_Deve_Encerrar_Sessao_Ja_Encerrada()
        {
            // Arrange
            var sessao = new Sessao(
                Guid.NewGuid(),
                Guid.NewGuid());

            sessao.Encerrar();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(
                () => sessao.Encerrar());
        }

        [Fact]
        public void Deve_Cancelar_Sessao_Ativa()
        {
            // Arrange
            var sessao = new Sessao(
                Guid.NewGuid(),
                Guid.NewGuid());

            // Act
            sessao.Cancelar();

            // Assert
            Assert.Equal(StatusSessao.Cancelada, sessao.Status);
            Assert.NotNull(sessao.Fim);
        }

        [Fact]
        public void Nao_Deve_Cancelar_Sessao_Ja_Encerrada()
        {
            // Arrange
            var sessao = new Sessao(
                Guid.NewGuid(),
                Guid.NewGuid());

            sessao.Encerrar();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(
                () => sessao.Cancelar());
        }
    }
}
