using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Domain.Enums;

namespace Vertex.Application.Sessions.Commands.IniciarSessao
{
    public sealed record IniciarSessaoResponse(
        Guid Id,
        Guid ClienteId,
        Guid EstacaoId,
        DateTime Inicio,
        StatusSessao Status);
}
