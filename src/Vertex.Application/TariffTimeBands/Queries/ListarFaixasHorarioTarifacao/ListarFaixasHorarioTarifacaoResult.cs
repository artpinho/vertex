using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vertex.Application.TariffTimeBands.Queries.ListarFaixasHorarioTarifacao
{
    public sealed record ListarFaixasHorarioTarifacaoResult(
        Guid Id,
        Guid ConfiguracaoTarifacaoId,
        int DiaSemana,
        TimeSpan HoraInicio,
        TimeSpan HoraFim,
        decimal ValorHora,
        bool Ativo,
        DateTime DataCadastro);
}
