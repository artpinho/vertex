using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Domain.Entities;

namespace Vertex.Domain.Tests.Entities
{
    public class ClienteTests
    {
        [Fact]
        public void Deve_Criar_Cliente_Ativo()
        {
            // Arrange
            var cliente = new Cliente("João da Silva");

            // Assert
            Assert.NotEqual(Guid.Empty, cliente.Id);
            Assert.Equal("João da Silva", cliente.Nome);
            Assert.True(cliente.Ativo);
        }

        [Fact]
        public void Nao_Deve_Criar_Cliente_Sem_Nome()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(
                () => new Cliente(""));
        }

        [Fact]
        public void Deve_Desativar_Cliente()
        {
            // Arrange
            var cliente = new Cliente("João da Silva");

            // Act
            cliente.Desativar();

            // Assert
            Assert.False(cliente.Ativo);
        }
    }
}
