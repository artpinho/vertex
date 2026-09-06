using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vertex.Application.TariffTimeBands.Commands.CriarFaixaHorarioTarifacao
{
    public sealed record CriarFaixaHorarioTarifacaoCommand(
        Guid ConfiguracaoTarifacaoId,
        int DiaSemana,
        TimeSpan HoraInicio,
        TimeSpan HoraFim,
        decimal ValorHora);
}
