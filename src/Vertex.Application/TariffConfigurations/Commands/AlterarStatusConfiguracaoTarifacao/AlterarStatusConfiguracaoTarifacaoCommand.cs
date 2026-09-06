using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vertex.Application.TariffConfigurations.Commands.AlterarStatusConfiguracaoTarifacao
{
    public sealed record AlterarStatusConfiguracaoTarifacaoCommand(
        Guid Id,
        OperacaoConfiguracaoTarifacao Operacao);
}
