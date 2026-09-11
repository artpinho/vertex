using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vertex.Application.Promotions.Queries.ListarFaixasHorarioPromocao
{
    public sealed record ListarFaixasHorarioPromocaoResponse(
        Guid Id,
        TimeSpan HoraInicio,
        TimeSpan HoraFim);
}
