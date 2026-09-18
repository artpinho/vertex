using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Abstractions.Persistence;
using Vertex.Domain.Entities;
using Vertex.Domain.Enums;

namespace Vertex.Application.Wallet.Commands.RecarregarCarteira
{
    public sealed class RecarregarCarteiraHandler
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly ICarteiraClienteRepository _carteiraRepository;

        public RecarregarCarteiraHandler(
            IClienteRepository clienteRepository,
            ICarteiraClienteRepository carteiraRepository)
        {
            _clienteRepository = clienteRepository;
            _carteiraRepository = carteiraRepository;
        }

        public async Task<RecarregarCarteiraResponse> HandleAsync(
            RecarregarCarteiraCommand command,
            CancellationToken cancellationToken)
        {
            var cliente = await _clienteRepository.ObterPorIdAsync(
                command.ClienteId,
                cancellationToken);

            if (cliente is null)
                throw new KeyNotFoundException(
                    "Cliente não encontrado.");

            if (!cliente.Ativo)
                throw new InvalidOperationException(
                    "Não é possível recarregar a carteira de um cliente inativo.");

            if (command.Valor <= 0)
                throw new ArgumentException(
                    "O valor da recarga deve ser maior que zero.");

            var carteira =
                await _carteiraRepository.ObterPorClienteIdAsync(
                    command.ClienteId,
                    cancellationToken);

            if (carteira is null)
            {
                carteira = new CarteiraCliente(command.ClienteId);

                await _carteiraRepository.AdicionarAsync(
                    carteira,
                    cancellationToken);
            }

            var saldoAnterior = carteira.Saldo;

            carteira.Creditar(command.Valor);

            var movimentacao = new MovimentacaoCarteira(
                carteira.Id,
                command.Valor,
                TipoMovimentacaoCarteira.Credito,
                command.TipoPagamento,
                descricao: command.Descricao);

            await _carteiraRepository.AdicionarMovimentacaoAsync(
                movimentacao,
                cancellationToken);

            await _carteiraRepository.SalvarAlteracoesAsync(
                cancellationToken);

            return new RecarregarCarteiraResponse(
                carteira.Id,
                carteira.ClienteId,
                saldoAnterior,
                command.Valor,
                carteira.Saldo,
                movimentacao.Id,
                movimentacao.Data);
        }
    }
}
