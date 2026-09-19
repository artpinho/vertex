using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Domain.Enums;

namespace Vertex.Application.Wallet.Queries.ListarMovimentacoesCarteira
{
    public sealed record MovimentacaoCarteiraResponse(
        Guid Id,
        decimal Valor,
        TipoMovimentacaoCarteira Tipo,
        TipoPagamento? TipoPagamento,
        Guid? SessaoId,
        Guid? VendaId,
        DateTime Data,
        string? Descricao);
}
