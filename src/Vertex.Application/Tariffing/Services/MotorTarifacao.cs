using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Tariffing.Models;

namespace Vertex.Application.Tariffing.Services
{
    public sealed class MotorTarifacao : IMotorTarifacao
    {
        private readonly ITarifacaoProvider _provider;

        public MotorTarifacao(ITarifacaoProvider provider)
        {
            _provider = provider;
        }

        public async Task<ResultadoTarifacao> CalcularAsync(
            ContextoTarifacao contexto,
            CancellationToken cancellationToken = default)
        {
            ValidarContexto(contexto);

            var pontos = await ObterPontosDeQuebraAsync(
                contexto,
                cancellationToken);

            var segmentos = new List<SegmentoTarifacao>();

            for (var i = 0; i < pontos.Count - 1; i++)
            {
                var inicio = pontos[i];
                var fim = pontos[i + 1];

                if (fim <= inicio)
                    continue;

                var regra = await _provider.ObterConfiguracaoAsync(
                    contexto.TipoMaquinaId,
                    inicio,
                    cancellationToken);

                if (regra is null)
                    throw new InvalidOperationException(
                        $"Nenhuma configuração de tarifação encontrada para {inicio}.");

                var valorHoraFaixa =
                    await _provider.ObterValorHoraFaixaAsync(
                        regra.ConfiguracaoTarifacaoId,
                        ConverterDiaSemana(inicio.DayOfWeek),
                        inicio.TimeOfDay,
                        cancellationToken);

                var valorHora = valorHoraFaixa ?? regra.ValorHora;

                var promocao = await _provider.ObterPromocaoAsync(
                    contexto.TipoMaquinaId,
                    inicio,
                    cancellationToken);

                var duracaoHoras =
                    (decimal)(fim - inicio).TotalSeconds / 3600m;

                var valorBruto = valorHora * duracaoHoras;

                var desconto = 0m;

                if (promocao?.PercentualDesconto is > 0)
                {
                    desconto = valorBruto *
                               (promocao.PercentualDesconto.Value / 100m);
                }
                else if (promocao?.ValorDescontoHora is > 0)
                {
                    desconto = promocao.ValorDescontoHora.Value *
                               duracaoHoras;
                }

                if (desconto > valorBruto)
                    desconto = valorBruto;

                var valor = valorBruto - desconto;

                segmentos.Add(
                    new SegmentoTarifacao(
                        inicio,
                        fim,
                        regra.ConfiguracaoTarifacaoId,
                        promocao?.PromocaoId,
                        valorHora,
                        desconto,
                        valor));
            }

            var valorTotal = segmentos.Sum(x => x.Valor);

            return new ResultadoTarifacao(
                decimal.Round(valorTotal, 2, MidpointRounding.AwayFromZero),
                segmentos);
        }

        private async Task<IReadOnlyList<DateTime>> ObterPontosDeQuebraAsync(
    ContextoTarifacao contexto,
    CancellationToken cancellationToken)
        {
            var pontos = new SortedSet<DateTime>
    {
        contexto.Inicio,
        contexto.Fim
    };

            /*
             * Meia-noite.
             */
            var dataAtual = contexto.Inicio.Date;

            while (dataAtual < contexto.Fim.Date)
            {
                var meiaNoite = dataAtual.AddDays(1);

                if (meiaNoite > contexto.Inicio &&
                    meiaNoite < contexto.Fim)
                {
                    pontos.Add(meiaNoite);
                }

                dataAtual = meiaNoite;
            }

            /*
             * Faixas de tarifação e promoção.
             * O Provider já retorna DateTime completos,
             * considerando o dia correto.
             */
            var pontosProvider = await _provider.ObterPontosDeQuebraAsync(
                contexto.TipoMaquinaId,
                contexto.Inicio,
                contexto.Fim,
                cancellationToken);

            foreach (var ponto in pontosProvider)
            {
                if (ponto > contexto.Inicio &&
                    ponto < contexto.Fim)
                {
                    pontos.Add(ponto);
                }
            }

            return pontos.ToList();
        }

        private static void ValidarContexto(ContextoTarifacao contexto)
        {
            ArgumentNullException.ThrowIfNull(contexto);

            if (contexto.ComputadorId == Guid.Empty)
                throw new ArgumentException(
                    "O computador informado é inválido.");

            if (contexto.TipoMaquinaId == Guid.Empty)
                throw new ArgumentException(
                    "O tipo de máquina informado é inválido.");

            if (contexto.Fim <= contexto.Inicio)
                throw new ArgumentException(
                    "O fim da tarifação deve ser maior que o início.");
        }

        private static int ConverterDiaSemana(DayOfWeek diaSemana)
        {
            return diaSemana switch
            {
                DayOfWeek.Monday => 1,
                DayOfWeek.Tuesday => 2,
                DayOfWeek.Wednesday => 3,
                DayOfWeek.Thursday => 4,
                DayOfWeek.Friday => 5,
                DayOfWeek.Saturday => 6,
                DayOfWeek.Sunday => 7,
                _ => throw new ArgumentOutOfRangeException(nameof(diaSemana))
            };
        }
    }
}
