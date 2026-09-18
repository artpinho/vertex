using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vertex.Application.Wallet.Commands.RecarregarCarteira
{
    public sealed record RecarregarCarteiraResponse(
        Guid CarteiraId,
        Guid ClienteId,
        decimal SaldoAnterior,
        decimal ValorRecarga,
        decimal SaldoAtual,
        Guid MovimentacaoId,
        DateTime Data);
}
