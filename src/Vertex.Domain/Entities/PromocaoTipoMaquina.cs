using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Domain.Common;

namespace Vertex.Domain.Entities
{
    public class PromocaoTipoMaquina : Entity
    {
        public Guid PromocaoId { get; private set; }
        public Guid TipoMaquinaId { get; private set; }

        protected PromocaoTipoMaquina()
        {
        }

        public PromocaoTipoMaquina(
            Guid promocaoId,
            Guid tipoMaquinaId)
        {
            if (promocaoId == Guid.Empty)
                throw new ArgumentException(
                    "A promoção informada é inválida.");

            if (tipoMaquinaId == Guid.Empty)
                throw new ArgumentException(
                    "O tipo de máquina informado é inválido.");

            PromocaoId = promocaoId;
            TipoMaquinaId = tipoMaquinaId;
        }
    }
}
