using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vertex.Application.Wallet.Queries.ListarMovimentacoesCarteira
{
    public sealed record ListarMovimentacoesCarteiraQuery(
        Guid ClienteId);
}
