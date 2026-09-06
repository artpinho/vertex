using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vertex.Application.TariffConfigurations.Queries.ObterConfiguracaoTarifacao
{
    public sealed record ObterConfiguracaoTarifacaoResult(
        Guid Id,
        string Nome,
        string? Descricao,
        Guid TipoMaquinaId,
        decimal ValorHora,
        DateTime DataInicio,
        DateTime? DataFim,
        int Prioridade,
        bool Ativo,
        DateTime DataCadastro);
}
