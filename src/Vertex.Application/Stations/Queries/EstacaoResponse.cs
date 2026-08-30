using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Domain.Enums;

namespace Vertex.Application.Stations.Queries
{
    public sealed record EstacaoResponse(
        Guid Id,
        string Nome,
        int Numero,
        StatusEstacao Status,
        bool Ativa,
        Guid? ComputadorId);
}
