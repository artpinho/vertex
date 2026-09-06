using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vertex.Contracts.TariffTimeBands
{
    public sealed record CriarFaixaHorarioTarifacaoRequest(
        Guid ConfiguracaoTarifacaoId,
        int DiaSemana,
        TimeSpan HoraInicio,
        TimeSpan HoraFim,
        decimal ValorHora);
}
