using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vertex.Application.Promotions.Commands.RemoverPromocaoDiaSemana
{
    public sealed record RemoverPromocaoDiaSemanaCommand(
        Guid PromocaoId,
        int DiaSemana);
}
