using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vertex.Application.Computers.Commands.AlterarStatus
{
    public record AlterarStatusComputadorCommand(
        Guid ComputadorId,
        OperacaoComputador Operacao);
}
