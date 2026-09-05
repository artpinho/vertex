using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vertex.Application.MachineTypes.Commands.AlterarStatusTipoMaquina
{
    public sealed record AlterarStatusTipoMaquinaCommand(
        Guid TipoMaquinaId,
        bool Ativo);
}
