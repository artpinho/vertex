using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vertex.Application.Promotions.Commands.AssociarPromocaoFaixaHorario
{
    public sealed record AssociarPromocaoFaixaHorarioCommand(
        Guid PromocaoId,
        TimeSpan HoraInicio,
        TimeSpan HoraFim);
}
