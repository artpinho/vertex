using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vertex.Application.Tariffing.Models
{
    public sealed record ConfiguracaoTarifacaoAplicavel(
        Guid ConfiguracaoTarifacaoId,
        decimal ValorHora,
        int Prioridade);
}
