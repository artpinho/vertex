using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vertex.Application.Promotions.Commands.AtualizarPromocaoFaixaHorario
{
    public sealed record AtualizarPromocaoFaixaHorarioCommand(
        Guid Id,
        Guid PromocaoId,
        TimeSpan HoraInicio,
        TimeSpan HoraFim);
}
