using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Tariffing.Models;
using Vertex.Application.Tariffing.Services;

namespace Vertex.Application.Sessions.Services
{
    public sealed class LimiteSessaoPrePaga : ILimiteSessaoPrePaga
    {
        private readonly IMotorTarifacao _motorTarifacao;

        public LimiteSessaoPrePaga(
            IMotorTarifacao motorTarifacao)
        {
            _motorTarifacao = motorTarifacao;
        }

        public async Task<DateTime?> CalcularFimPermitidoAsync(
            Guid computadorId,
            Guid tipoMaquinaId,
            DateTime inicio,
            decimal saldo,
            CancellationToken cancellationToken = default)
        {
            if (saldo <= 0)
                return inicio;

            /*
             * Primeiro procuramos um ponto no futuro
             * cujo custo seja maior que o saldo.
             */
            var inicioBusca = inicio;
            var minutos = 1;

            DateTime fimBusca = inicio.AddMinutes(minutos);

            while (true)
            {
                var resultado =
                    await CalcularAsync(
                        computadorId,
                        tipoMaquinaId,
                        inicioBusca,
                        fimBusca,
                        cancellationToken);

                if (resultado.ValorTotal > saldo)
                    break;

                minutos *= 2;

                if (minutos > 60 * 24 * 30)
                {
                    throw new InvalidOperationException(
                        "Não foi possível determinar o limite financeiro da sessão.");
                }

                fimBusca = inicio.AddMinutes(minutos);
            }

            /*
             * Agora temos:
             *
             * inicioIntervalo
             *     ↓
             * custo <= saldo
             *
             * fimIntervalo
             *     ↓
             * custo > saldo
             *
             * Fazemos busca binária para encontrar
             * o instante exato.
             */
            var limiteInferior = inicio;
            var limiteSuperior = fimBusca;

            for (var i = 0; i < 40; i++)
            {
                var meio =
                    limiteInferior +
                    TimeSpan.FromTicks(
                        (limiteSuperior - limiteInferior).Ticks / 2);

                var resultado =
                    await CalcularAsync(
                        computadorId,
                        tipoMaquinaId,
                        inicio,
                        meio,
                        cancellationToken);

                if (resultado.ValorTotal <= saldo)
                {
                    limiteInferior = meio;
                }
                else
                {
                    limiteSuperior = meio;
                }
            }

            return limiteInferior;
        }

        private async Task<ResultadoTarifacao> CalcularAsync(
            Guid computadorId,
            Guid tipoMaquinaId,
            DateTime inicio,
            DateTime fim,
            CancellationToken cancellationToken)
        {
            return await _motorTarifacao.CalcularAsync(
                new ContextoTarifacao(
                    computadorId,
                    tipoMaquinaId,
                    inicio,
                    fim),
                cancellationToken);
        }
    }
}
