using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vertex.Application.TariffTimeBands.Commands.AlterarStatusFaixaHorarioTarifacao
{
    public sealed record AlterarStatusFaixaHorarioTarifacaoCommand(
        Guid Id,
        OperacaoFaixaHorarioTarifacao Operacao);
}
