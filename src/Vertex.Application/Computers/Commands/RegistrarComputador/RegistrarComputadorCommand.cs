using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vertex.Application.Computers.Commands.RegistrarComputador
{
    public sealed record RegistrarComputadorCommand(

        string Hostname,
        string? Ip,
        string? MacAddress,
        string? SistemaOperacional,
        string? ClienteVersao);
        
}
