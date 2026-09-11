using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Domain.Common;

namespace Vertex.Domain.Entities
{
    public class PromocaoFaixaHorario : Entity
    {
        public Guid PromocaoId { get; private set; }

        public TimeSpan HoraInicio { get; private set; }

        public TimeSpan HoraFim { get; private set; }

        protected PromocaoFaixaHorario()
        {
        }

        public PromocaoFaixaHorario(
            Guid promocaoId,
            TimeSpan horaInicio,
            TimeSpan horaFim)
        {
            if (promocaoId == Guid.Empty)
                throw new ArgumentException(
                    "A promoção informada é inválida.");

            if (horaInicio >= horaFim)
                throw new ArgumentException(
                    "A hora de início deve ser menor que a hora de fim.");

            HoraInicio = horaInicio;
            HoraFim = horaFim;
            PromocaoId = promocaoId;
        }

        public void Atualizar(
            TimeSpan horaInicio,
            TimeSpan horaFim)
        {
            if (horaInicio >= horaFim)
                throw new ArgumentException(
                    "A hora de início deve ser menor que a hora de fim.");

            HoraInicio = horaInicio;
            HoraFim = horaFim;
        }
    }
}
