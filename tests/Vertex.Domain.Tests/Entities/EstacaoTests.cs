using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Domain.Entities;
using Vertex.Domain.Enums;

namespace Vertex.Domain.Tests.Entities
{
    public class EstacaoTests
    {
        [Fact]
        public void Estacao_Deve_Iniciar_Livre()
        {
            // Arrange
            var estacao = new Estacao("Estação 01", 1);

            // Assert
            Assert.Equal(StatusEstacao.Livre, estacao.Status);
            Assert.True(estacao.Ativa);
        }

        [Fact]
        public void Deve_Colocar_Estacao_Em_Uso()
        {
            // Arrange
            var estacao = new Estacao("Estação 01", 1);

            // Act
            estacao.ColocarEmUso();

            // Assert
            Assert.Equal(StatusEstacao.EmUso, estacao.Status);
        }

        [Fact]
        public void Nao_Deve_Utilizar_Estacao_Inativa()
        {
            // Arrange
            var estacao = new Estacao("Estação 01", 1);

            estacao.Desativar();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(
                () => estacao.ColocarEmUso());
        }
    }
}
