using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vertex.Application.Sessions.Commands.IniciarSessao
{
    public sealed record IniciarSessaoCommand(
        Guid ClienteId,
        Guid EstacaoId);
}
