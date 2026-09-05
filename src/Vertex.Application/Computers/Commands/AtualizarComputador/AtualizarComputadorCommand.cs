using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vertex.Application.Computers.Commands.AtualizarComputador
{
    public record AtualizarComputadorCommand(
        Guid ComputadorId,
        string? Ip,
        string? MacAddress,
        string? SistemaOperacional,
        string? ClienteVersao);
    }
