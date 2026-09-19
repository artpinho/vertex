using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Abstractions.Persistence;

namespace Vertex.Application.Wallet.Queries.ObterCarteira
{
    public sealed class ObterCarteiraHandler
    {
        private readonly ICarteiraClienteRepository _carteiraRepository;

        public ObterCarteiraHandler(
            ICarteiraClienteRepository carteiraRepository)
        {
            _carteiraRepository = carteiraRepository;
        }

        public async Task<ObterCarteiraResponse> HandleAsync(
            ObterCarteiraQuery query,
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

            return new ObterCarteiraResponse(
                carteira.Id,
                carteira.ClienteId,
                carteira.Saldo);
        }
    }
}
