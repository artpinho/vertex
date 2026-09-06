using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vertex.Contracts.TariffConfigurations
{
    public sealed record AtualizarConfiguracaoTarifacaoRequest(
        string Nome,
        string? Descricao,
        decimal ValorHora,
        DateTime DataInicio,
        DateTime? DataFim,
        int Prioridade);
}
