using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Abstractions.Persistence;

namespace Vertex.Application.Wallet.Queries.ListarMovimentacoesCarteira
{
    public sealed class ListarMovimentacoesCarteiraHandler
    {
        private readonly ICarteiraClienteRepository _carteiraRepository;

        public ListarMovimentacoesCarteiraHandler(
            ICarteiraClienteRepository carteiraRepository)
        {
            _carteiraRepository = carteiraRepository;
        }

        public async Task<IReadOnlyList<MovimentacaoCarteiraResponse>> HandleAsync(
            ListarMovimentacoesCarteiraQuery query,
            CancellationToken cancellationToken)
        {
            var carteira =
                await _carteiraRepository.ObterPorClienteIdAsync(
                    query.ClienteId,
                    cancellationToken);

            if (carteira is null)
            {
                throw new KeyNotFoundException(
                    "Carteira do cliente não encontrada.");
            }

            var movimentacoes =
                await _carteiraRepository.ListarMovimentacoesAsync(
                    carteira.Id,
                    cancellationToken);

            return movimentacoes
                .Select(x => new MovimentacaoCarteiraResponse(
                    x.Id,
                    x.Valor,
                    x.Tipo,
                    x.TipoPagamento,
                    x.SessaoId,
                    x.VendaId,
                    x.Data,
                    x.Descricao))
                .ToList();
        }
    }
}
