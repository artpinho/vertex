using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vertex.Application.TariffTimeBands.Commands.AtualizarFaixaHorarioTarifacao
{
    public sealed record AtualizarFaixaHorarioTarifacaoCommand(
        Guid Id,
        int DiaSemana,
        TimeSpan HoraInicio,
        TimeSpan HoraFim,
        decimal ValorHora);
}
