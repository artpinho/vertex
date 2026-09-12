using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vertex.Application.Sessions.DTOs
{
    public sealed record ConsumosSessaoResponse(
        Guid SessaoId,
        decimal ValorTotal,
        IReadOnlyList<ConsumoTarifacaoResponse> Consumos);
}
