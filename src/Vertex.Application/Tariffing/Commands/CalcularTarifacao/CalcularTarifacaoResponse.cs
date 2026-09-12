using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vertex.Application.Tariffing.Commands.CalcularTarifacao
{
    public sealed record CalcularTarifacaoResponse(
        decimal ValorTotal,
        IReadOnlyList<SegmentoTarifacaoResponse> Segmentos);

    public sealed record SegmentoTarifacaoResponse(
        DateTime Inicio,
        DateTime Fim,
        Guid ConfiguracaoTarifacaoId,
        Guid? PromocaoId,
        decimal ValorHora,
        decimal Desconto,
        decimal Valor);
}
