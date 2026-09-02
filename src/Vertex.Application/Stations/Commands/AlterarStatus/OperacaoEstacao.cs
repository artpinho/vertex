using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vertex.Application.Stations.Commands.AlterarStatus
{
    public enum OperacaoEstacao
    {
        Liberar = 1,
        ColocarEmUso = 2,
        Bloquear = 3,
        ColocarEmManutencao = 4,
        Ativar = 5,
        Desativar = 6
    }
}
