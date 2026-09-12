using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vertex.Application.Tariffing.Commands.CalcularTarifacao
{
    public sealed record CalcularTarifacaoCommand(
        Guid ComputadorId,
        Guid TipoMaquinaId,
        DateTime Inicio,
        DateTime Fim);
}
