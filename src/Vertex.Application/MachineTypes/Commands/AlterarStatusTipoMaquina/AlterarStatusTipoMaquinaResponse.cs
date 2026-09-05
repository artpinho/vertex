using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vertex.Application.MachineTypes.Commands.AlterarStatusTipoMaquina
{
    public sealed record AlterarStatusTipoMaquinaResponse(
        Guid Id,
        string Nome,
        string? Descricao,
        bool Ativo,
        DateTime DataCadastro);
}
