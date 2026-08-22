using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vertex.Contracts.Computers
{
    public sealed record HeartbeatRequest(
    Guid ComputadorId,
    string HostName,
    string? Ip,
    string? MacAddress,
    string? SistemaOperacional,
    string? ClienteVersao,
    double? CpuUso,
    double? MemoriaUso,
    double? DiscoLivre);
}
