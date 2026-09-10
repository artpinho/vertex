using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vertex.Application.Promotions.Commands.AssociarPromocaoDiaSemana
{
    public sealed record AssociarPromocaoDiaSemanaCommand(
        Guid PromocaoId,
        int DiaSemana);
}
