using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Domain.Enums;

namespace Vertex.Application.Sessions.Queries
{
    public record SessaoResponse(
        Guid Id,
        Guid ClienteId,
        Guid EstacaoId,
        DateTime Inicio,
        DateTime? Fim,
        TimeSpan Duracao,
        StatusSessao Status);
}
