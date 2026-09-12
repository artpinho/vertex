using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vertex.Application.Tariffing.Models
{
    public sealed record SegmentoTarifacao(
        DateTime Inicio,
        DateTime Fim,
        Guid ConfiguracaoTarifacaoId,
        Guid? PromocaoId,
        decimal ValorHora,
        decimal Desconto,
        decimal Valor);
}
