using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Abstractions.Persistence;
using Vertex.Application.Tariffing.Models;
using Vertex.Application.Tariffing.Services;
using Vertex.Domain.Entities;
using Vertex.Domain.Enums;

namespace Vertex.Application.Sessions.Services
{
    public sealed class ProcessadorSessaoPrePaga
    {
        private readonly ISessaoRepository _sessaoRepository;
        private readonly IEstacaoRepository _estacaoRepository;
        private readonly IComputadorRepository _computadorRepository;
        private readonly ICarteiraClienteRepository _carteiraRepository;
        private readonly IConsumoTarifacaoRepository _consumoRepository;
        private readonly IMotorTarifacao _motorTarifacao;
        private readonly ILimiteSessaoPrePaga _limiteSessaoPrePaga;

        public ProcessadorSessaoPrePaga(
            ISessaoRepository sessaoRepository,
            IEstacaoRepository estacaoRepository,
            IComputadorRepository computadorRepository,
            ICarteiraClienteRepository carteiraRepository,
            IConsumoTarifacaoRepository consumoRepository,
            IMotorTarifacao motorTarifacao,
            ILimiteSessaoPrePaga limiteSessaoPrePaga)
        {
            _sessaoRepository = sessaoRepository;
            _estacaoRepository = estacaoRepository;
            _computadorRepository = computadorRepository;
            _carteiraRepository = carteiraRepository;
            _consumoRepository = consumoRepository;
            _motorTarifacao = motorTarifacao;
            _limiteSessaoPrePaga = limiteSessaoPrePaga;
        }

        public async Task ProcessarAsync(
            Sessao sessao,
            CancellationToken cancellationToken)
        {
            if (sessao.Status != StatusSessao.Ativa)
                return;

            var carteira =
                await _carteiraRepository.ObterPorClienteIdAsync(
                    sessao.ClienteId,
                    cancellationToken);

            if (carteira is null)
                return;

            if (carteira.Saldo <= 0)
            {
                await EncerrarPorSaldoAsync(
                    sessao,
                    carteira,
                    sessao.Inicio,
                    cancellationToken);

                return;
            }

            var estacao =
                await _estacaoRepository.ObterPorIdAsync(
                    sessao.EstacaoId,
                    cancellationToken);

            if (estacao is null)
                return;

            if (!estacao.ComputadorId.HasValue)
                return;

            var computador =
                await _computadorRepository.ObterPorIdAsync(
                    estacao.ComputadorId.Value,
                    cancellationToken);

            if (computador is null ||
                !computador.TipoMaquinaId.HasValue)
            {
                return;
            }

            var fimPermitido =
                await _limiteSessaoPrePaga.CalcularFimPermitidoAsync(
                    computador.Id,
                    computador.TipoMaquinaId.Value,
                    sessao.Inicio,
                    carteira.Saldo,
                    cancellationToken);

            if (!fimPermitido.HasValue)
                return;

            if (DateTime.UtcNow < fimPermitido.Value)
                return;

            await EncerrarPorSaldoAsync(
                sessao,
                carteira,
                fimPermitido.Value,
                cancellationToken);
        }

        private async Task EncerrarPorSaldoAsync(
            Sessao sessao,
            CarteiraCliente carteira,
            DateTime fim,
            CancellationToken cancellationToken)
        {
            var estacao =
                await _estacaoRepository.ObterPorIdAsync(
                    sessao.EstacaoId,
                    cancellationToken);

            if (estacao is null)
                return;

            if (!estacao.ComputadorId.HasValue)
                return;

            var computador =
                await _computadorRepository.ObterPorIdAsync(
                    estacao.ComputadorId.Value,
                    cancellationToken);

            if (computador is null ||
                !computador.TipoMaquinaId.HasValue)
            {
                return;
            }

            sessao.Encerrar(fim);

            var resultadoTarifacao =
                await _motorTarifacao.CalcularAsync(
                    new ContextoTarifacao(
                        computador.Id,
                        computador.TipoMaquinaId.Value,
                        sessao.Inicio,
                        sessao.Fim!.Value),
                    cancellationToken);

            var valorConsumo =
                resultadoTarifacao.ValorTotal;

            if (valorConsumo > carteira.Saldo)
            {
                valorConsumo = carteira.Saldo;
            }

            if (valorConsumo > 0)
            {
                carteira.Debitar(valorConsumo);

                var consumos = resultadoTarifacao.Segmentos
                    .Select(segmento =>
                        new ConsumoTarifacao(
                            sessao.Id,
                            segmento.Inicio,
                            segmento.Fim,
                            segmento.ConfiguracaoTarifacaoId,
                            segmento.PromocaoId,
                            segmento.ValorHora,
                            segmento.Desconto,
                            segmento.Valor))
                    .ToList();

                await _consumoRepository.AdicionarListaAsync(
                    consumos,
                    cancellationToken);

                var movimentacao = new MovimentacaoCarteira(
                    carteira.Id,
                    valorConsumo,
                    TipoMovimentacaoCarteira.Debito,
                    sessaoId: sessao.Id,
                    descricao:
                        $"Consumo automático da sessão {sessao.Id}");

                await _carteiraRepository.AdicionarMovimentacaoAsync(
                    movimentacao,
                    cancellationToken);
            }

            estacao.Liberar();

            await _sessaoRepository.SalvarAlteracoesAsync(
                cancellationToken);
        }
    }
}
