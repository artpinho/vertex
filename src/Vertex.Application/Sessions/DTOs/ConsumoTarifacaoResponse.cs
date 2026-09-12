using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vertex.Application.Sessions.DTOs
{
    public sealed record ConsumoTarifacaoResponse(
        Guid Id,
        Guid SessaoId,
        DateTime Inicio,
        DateTime Fim,
        Guid ConfiguracaoTarifacaoId,
        Guid? PromocaoId,
        decimal ValorHora,
        decimal Desconto,
        decimal Valor);
}
