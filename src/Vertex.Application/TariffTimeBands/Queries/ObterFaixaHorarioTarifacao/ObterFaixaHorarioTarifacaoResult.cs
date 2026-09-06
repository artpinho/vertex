using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vertex.Application.TariffTimeBands.Queries.ObterFaixaHorarioTarifacao
{
    public sealed record ObterFaixaHorarioTarifacaoResult(
        Guid Id,
        Guid ConfiguracaoTarifacaoId,
        int DiaSemana,
        TimeSpan HoraInicio,
        TimeSpan HoraFim,
        decimal ValorHora,
        bool Ativo,
        DateTime DataCadastro);
}
