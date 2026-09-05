using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vertex.Application.MachineTypes.Commands.CriarTipoMaquina
{
    public sealed record CriarTipoMaquinaCommand(
        string Nome,
        string? Descricao);
}
