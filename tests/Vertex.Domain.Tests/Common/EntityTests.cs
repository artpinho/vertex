using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Domain.Common;

namespace Vertex.Domain.Tests.Common
{
    public class EntityTests
    {
        private sealed class TestEntity : Entity
        {
        }

        [Fact]
        public void Deve_Gerar_Id_Automaticamente()
        {
            // Arrange & Act
            var entity = new TestEntity();
            // Assert
            Assert.NotEqual(Guid.Empty, entity.Id);
        }
        [Fact]
        public void Cada_Entidade_Deve_Possuir_Id_Unico()
        {
            // Arrange
            var entity1 = new TestEntity();
            var entity2 = new TestEntity();
            // Act & Assert
            Assert.NotEqual(entity1.Id, entity2.Id);
        }
    }
}
