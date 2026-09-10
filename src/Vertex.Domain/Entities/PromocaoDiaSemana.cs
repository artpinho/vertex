using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Domain.Common;

namespace Vertex.Domain.Entities
{
    public class PromocaoDiaSemana : Entity
    {
        public Guid PromocaoId { get; private set; }
        public int DiaSemana { get; private set; }

        protected PromocaoDiaSemana()
        {
        }

        public PromocaoDiaSemana(
            Guid promocaoId,
            int diaSemana)
        {
            if (promocaoId == Guid.Empty)
                throw new ArgumentException(
                    "A promoção informada é inválida.");

            if (diaSemana < 1 || diaSemana > 7)
                throw new ArgumentException(
                    "O dia da semana deve estar entre 1 e 7.");

            PromocaoId = promocaoId;
            DiaSemana = diaSemana;
        }
    }
}
