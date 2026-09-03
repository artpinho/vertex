using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vertex.Application.Sessions.Commands.EncerrarSessao
{
    public sealed record EncerrarSessaoCommand(
        Guid SessaoId);
}
