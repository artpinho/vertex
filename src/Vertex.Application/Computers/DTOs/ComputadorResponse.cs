using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Domain.Enums;

namespace Vertex.Application.Computers.DTOs
{
    public sealed record ComputadorResponse(

        Guid Id,
        string HostName,
        string? Ip,
        string? MacAddress,
        string? SistemaOperacional,
        string? ClienteVersao,
        DateTime? UltimoHeartbeat,
        StatusComputador Status);
}
