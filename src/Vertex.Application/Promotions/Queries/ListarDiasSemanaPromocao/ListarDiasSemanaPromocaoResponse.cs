using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vertex.Application.Promotions.Queries.ListarDiasSemanaPromocao
{
    public sealed record ListarDiasSemanaPromocaoResponse(
        int DiaSemana,
        string NomeDiaSemana);
}
