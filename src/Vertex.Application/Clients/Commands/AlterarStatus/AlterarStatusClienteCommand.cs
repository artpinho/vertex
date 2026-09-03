using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vertex.Application.Clients.Commands.AlterarStatus
{
    public sealed record AlterarStatusClienteCommand(
    Guid ClienteId,
    bool Ativo);
}
