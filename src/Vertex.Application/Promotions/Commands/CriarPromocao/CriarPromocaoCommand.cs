using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vertex.Application.Promotions.Commands.CriarPromocao
{
    public record CriarPromocaoCommand(
        string Nome,
        DateTime DataInicio,
        DateTime? DataFim,
        string? Descricao,
        decimal? PercentualDesconto,
        decimal? ValorDescontoHora,
        int Prioridade,
        bool TodosTiposMaquina);
}
