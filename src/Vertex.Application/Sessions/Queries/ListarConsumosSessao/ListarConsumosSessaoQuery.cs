using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vertex.Application.Sessions.Queries.ListarConsumosSessao
{
    public sealed record ListarConsumosSessaoQuery(
        Guid SessaoId);
}
