using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vertex.Application.Stations.Commands.AlterarStatus
{
    public sealed record AlterarStatusEstacaoCommand(
    Guid EstacaoId,
    OperacaoEstacao Operacao);
}
