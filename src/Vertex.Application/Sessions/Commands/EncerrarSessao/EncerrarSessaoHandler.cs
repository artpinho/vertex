using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Abstractions.Persistence;
using Vertex.Application.Tariffing.Models;
using Vertex.Application.Tariffing.Services;
using Vertex.Domain.Entities;

namespace Vertex.Application.Sessions.Commands.EncerrarSessao
{
    public sealed class EncerrarSessaoHandler
    {
        private readonly ISessaoRepository _sessaoRepository;
        private readonly IEstacaoRepository _estacaoRepository;
        private readonly IComputadorRepository _computadorRepository;
        private readonly IMotorTarifacao _motorTarifacao;
        private readonly IConsumoTarifacaoRepository _consumoTarifacaoRepository;

        public EncerrarSessaoHandler(
            ISessaoRepository sessaoRepository,
            IEstacaoRepository estacaoRepository,
            IComputadorRepository computadorRepository,
            IMotorTarifacao motorTarifacao,
            IConsumoTarifacaoRepository consumoTarifacaoRepository)
        {
            _sessaoRepository = sessaoRepository;
            _estacaoRepository = estacaoRepository;
            _computadorRepository = computadorRepository;
            _motorTarifacao = motorTarifacao;
            _consumoTarifacaoRepository = consumoTarifacaoRepository;
        }

        public async Task<EncerrarSessaoResponse> HandleAsync(
            EncerrarSessaoCommand command,
            CancellationToken cancellationToken)
        {
            var sessao =
                await _sessaoRepository.ObterPorIdAsync(
                    command.SessaoId,
                    cancellationToken);

            if (sessao is null)
            {
                throw new KeyNotFoundException(
                    "Sessão não encontrada.");
            }

            var estacao =
                await _estacaoRepository.ObterPorIdAsync(
                    sessao.EstacaoId,
                    cancellationToken);

            if (estacao is null)
            {
                throw new KeyNotFoundException(
                    "Estação da sessão não encontrada.");
            }

            if (!estacao.ComputadorId.HasValue)
            {
                throw new InvalidOperationException(
                    "A estação da sessão não possui um computador associado.");
            }

            var computador =
                await _computadorRepository.ObterPorIdAsync(
                    estacao.ComputadorId.Value,
                    cancellationToken);

            if (computador is null)
            {
                throw new KeyNotFoundException(
                    "Computador associado à estação não encontrado.");
            }

            if (!computador.TipoMaquinaId.HasValue)
            {
                throw new InvalidOperationException(
                    "O computador associado à estação não possui um tipo de máquina definido.");
            }

            // Encerra a sessão e define o Fim.
            sessao.Encerrar();

            var resultadoTarifacao =
                await _motorTarifacao.CalcularAsync(
                    new ContextoTarifacao(
                        computador.Id,
                        computador.TipoMaquinaId.Value,
                        sessao.Inicio,
                        sessao.Fim!.Value),
                    cancellationToken);

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

            await _consumoTarifacaoRepository.AdicionarListaAsync(
                consumos,
                cancellationToken);

            estacao.Liberar();

            await _sessaoRepository.SalvarAlteracoesAsync(
                cancellationToken);

            return new EncerrarSessaoResponse(
                sessao.Id,
                sessao.ClienteId,
                sessao.EstacaoId,
                sessao.Inicio,
                sessao.Fim!.Value,
                sessao.Duracao,
                sessao.Status);
        }
    }
}
