using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Domain.Enums;

namespace Vertex.Application.Wallet.Commands.RecarregarCarteira
{
    public sealed record RecarregarCarteiraCommand(
        Guid ClienteId,
        decimal Valor,
        TipoPagamento TipoPagamento,
        string? Descricao);
}
