using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vertex.Application.TariffTimeBands.Queries.ListarFaixasHorarioTarifacao
{
    public sealed record ListarFaixasHorarioTarifacaoQuery(
        Guid ConfiguracaoTarifacaoId);
}
