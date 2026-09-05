using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vertex.Application.MachineTypes.Commands.CriarTipoMaquina
{
    public sealed record CriarTipoMaquinaResponse(
        Guid Id,
        string Nome,
        string? Descricao,
        bool Ativo,
        DateTime DataCadastro);
}
