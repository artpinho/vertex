using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vertex.Application.Stations.Commands.AssociarComputador
{
    public sealed record AssociarComputadorCommand(
        Guid EstacaoId,
        Guid ComputadorId);
}
