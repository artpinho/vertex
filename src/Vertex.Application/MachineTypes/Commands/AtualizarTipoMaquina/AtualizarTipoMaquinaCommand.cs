using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vertex.Application.MachineTypes.Commands.AtualizarTipoMaquina
{
    public sealed record AtualizarTipoMaquinaCommand(
        Guid TipoMaquinaId,
        string Nome,
        string? Descricao);
}
