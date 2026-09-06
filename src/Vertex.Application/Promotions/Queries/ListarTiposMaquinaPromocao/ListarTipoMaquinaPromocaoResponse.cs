using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vertex.Application.Promotions.Queries.ListarTiposMaquinaPromocao
{
    public record ListarTipoMaquinaPromocaoResponse(
        Guid TipoMaquinaId,
        string Nome,
        string? Descricao,
        bool Ativo);
}
