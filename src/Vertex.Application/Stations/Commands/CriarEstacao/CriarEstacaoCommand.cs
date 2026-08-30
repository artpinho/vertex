using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vertex.Application.Stations.Commands.CriarEstacao
{
    public sealed record CriarEstacaoCommand(
        string Nome,
        int Numero);
}
