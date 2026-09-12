using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vertex.Application.Tariffing.Models
{
    public sealed record ContextoTarifacao(
        Guid ComputadorId,
        Guid TipoMaquinaId,
        DateTime Inicio,
        DateTime Fim);
}
