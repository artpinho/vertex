using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vertex.Application.Promotions.DTOs
{
    public sealed record PromocaoResponse(
        Guid Id,
        string Nome,
        string? Descricao,
        decimal? PercentualDesconto,
        decimal? ValorDescontoHora,
        DateTime DataInicio,
        DateTime? DataFim,
        int Prioridade,
        bool TodosTiposMaquina,
        bool Ativo,
        DateTime DataCadastro);
}
