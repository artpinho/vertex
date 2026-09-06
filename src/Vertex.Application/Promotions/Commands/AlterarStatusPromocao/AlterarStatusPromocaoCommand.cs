using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vertex.Application.Promotions.Commands.AlterarStatusPromocao
{
    public record AlterarStatusPromocaoCommand(
        Guid PromocaoId,
        OperacaoPromocao Operacao);


}
