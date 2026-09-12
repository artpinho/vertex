using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Tariffing.Models;
using Vertex.Application.Tariffing.Services;

namespace Vertex.Application.Tariffing.Commands.CalcularTarifacao
{
    public sealed class CalcularTarifacaoHandler
    {
        private readonly IMotorTarifacao _motorTarifacao;

        public CalcularTarifacaoHandler(IMotorTarifacao motorTarifacao)
        {
            _motorTarifacao = motorTarifacao;
        }

        public async Task<CalcularTarifacaoResponse> HandleAsync(
            CalcularTarifacaoCommand command,
            CancellationToken cancellationToken = default)
        {
            var contexto = new ContextoTarifacao(
                command.ComputadorId,
                command.TipoMaquinaId,
                command.Inicio,
                command.Fim);

            var resultado = await _motorTarifacao.CalcularAsync(
                contexto,
                cancellationToken);

            var segmentos = resultado.Segmentos
                .Select(x => new SegmentoTarifacaoResponse(
                    x.Inicio,
                    x.Fim,
                    x.ConfiguracaoTarifacaoId,
                    x.PromocaoId,
                    x.ValorHora,
                    x.Desconto,
                    x.Valor))
                .ToList();

            return new CalcularTarifacaoResponse(
                resultado.ValorTotal,
                segmentos);
        }
    }
}
