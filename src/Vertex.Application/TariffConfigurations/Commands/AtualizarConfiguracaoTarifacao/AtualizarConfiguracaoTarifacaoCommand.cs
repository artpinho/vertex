using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vertex.Application.TariffConfigurations.Commands.AtualizarConfiguracaoTarifacao
{
    public sealed record AtualizarConfiguracaoTarifacaoCommand(
        Guid Id,
        string Nome,
        string? Descricao,
        decimal ValorHora,
        DateTime DataInicio,
        DateTime? DataFim,
        int Prioridade);
}
