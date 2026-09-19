using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vertex.Application.Wallet.Queries.ObterCarteira
{
    public sealed record ObterCarteiraResponse(
        Guid CarteiraId,
        Guid ClienteId,
        decimal Saldo);
}
