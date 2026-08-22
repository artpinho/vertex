using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vertex.Contracts.Computers
{
    public sealed record HeartbeatResponse(
    Guid ComputadorId,
    DateTime UltimoHeartbeat,
    bool Online);
}
