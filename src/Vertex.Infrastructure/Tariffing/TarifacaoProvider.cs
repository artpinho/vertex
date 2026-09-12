using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Tariffing.Models;
using Vertex.Application.Tariffing.Services;
using Vertex.Infrastructure.Persistence.Context;

namespace Vertex.Infrastructure.Tariffing
{
    sealed class TarifacaoProvider : ITarifacaoProvider
    {
        private readonly VertexDbContext _context;

        public TarifacaoProvider(VertexDbContext context)
        {
            _context = context;
        }

        public async Task<ConfiguracaoTarifacaoAplicavel?> ObterConfiguracaoAsync(
            Guid tipoMaquinaId,
            DateTime momento,
            CancellationToken cancellationToken = default)
        {
            var diaSemana = ConverterDiaSemana(momento.DayOfWeek);
            var horario = momento.TimeOfDay;

            var configuracoes = await _context.ConfiguracoesTarifacao
                .AsNoTracking()
                .Where(x =>
                    x.TipoMaquinaId == tipoMaquinaId &&
                    x.Ativo &&
                    x.DataInicio <= momento &&
                    (!x.DataFim.HasValue || x.DataFim.Value >= momento))
                .OrderByDescending(x => x.Prioridade)
                .ThenByDescending(x => x.DataInicio)
                .ToListAsync(cancellationToken);

            foreach (var configuracao in configuracoes)
            {
                var faixas = await _context.FaixasHorarioTarifacao
                    .AsNoTracking()
                    .Where(x =>
                        x.ConfiguracaoTarifacaoId == configuracao.Id &&
                        x.Ativo)
                    .Select(x => new
                    {
                        x.DiaSemana,
                        x.HoraInicio,
                        x.HoraFim
                    })
                    .ToListAsync(cancellationToken);

                /*
                 * Se a configuração possui faixas,
                 * ela somente é aplicável quando uma
                 * faixa correspondente ao momento existir.
                 */
                if (faixas.Count > 0)
                {
                    var possuiFaixaAplicavel = faixas.Any(x =>
                        x.DiaSemana == diaSemana &&
                        x.HoraInicio <= horario &&
                        x.HoraFim > horario);

                    if (!possuiFaixaAplicavel)
                        continue;
                }

                /*
                 * Configuração sem faixas funciona como
                 * tarifa base durante toda a sua vigência.
                 */
                return new ConfiguracaoTarifacaoAplicavel(
                    configuracao.Id,
                    configuracao.ValorHora,
                    configuracao.Prioridade);
            }

            return null;
        }

        public async Task<decimal?> ObterValorHoraFaixaAsync(
            Guid configuracaoTarifacaoId,
            int diaSemana,
            TimeSpan horario,
            CancellationToken cancellationToken = default)
        {
            var faixa = await _context.FaixasHorarioTarifacao
                .AsNoTracking()
                .Where(x =>
                    x.ConfiguracaoTarifacaoId == configuracaoTarifacaoId &&
                    x.DiaSemana == diaSemana &&
                    x.Ativo &&
                    x.HoraInicio <= horario &&
                    x.HoraFim > horario)
                .OrderByDescending(x => x.HoraInicio)
                .FirstOrDefaultAsync(cancellationToken);

            return faixa?.ValorHora;
        }

        public async Task<RegraTarifacao?> ObterPromocaoAsync(
            Guid tipoMaquinaId,
            DateTime momento,
            CancellationToken cancellationToken = default)
        {
            var diaSemana = ConverterDiaSemana(momento.DayOfWeek);
            var horario = momento.TimeOfDay;

            var promocoes = await _context.Promocoes
                .AsNoTracking()
                .Where(x =>
                    x.Ativo &&
                    x.DataInicio <= momento &&
                    (!x.DataFim.HasValue || momento < x.DataFim.Value))
                .OrderByDescending(x => x.Prioridade)
                .ThenByDescending(x => x.DataInicio)
                .ToListAsync(cancellationToken);

            foreach (var promocao in promocoes)
            {
                var diaConfigurado = await _context.PromocoesDiasSemana
                    .AsNoTracking()
                    .AnyAsync(
                        x =>
                            x.PromocaoId == promocao.Id &&
                            x.DiaSemana == diaSemana,
                        cancellationToken);

                var possuiDiasConfigurados = await _context.PromocoesDiasSemana
                    .AsNoTracking()
                    .AnyAsync(
                        x => x.PromocaoId == promocao.Id,
                        cancellationToken);

                if (possuiDiasConfigurados && !diaConfigurado)
                    continue;

                var faixaHorarioConfigurada =
                    await _context.PromocoesFaixasHorario
                        .AsNoTracking()
                        .AnyAsync(
                            x => x.PromocaoId == promocao.Id,
                            cancellationToken);

                if (faixaHorarioConfigurada)
                {
                    var dentroDaFaixa =
                        await _context.PromocoesFaixasHorario
                            .AsNoTracking()
                            .AnyAsync(
                                x =>
                                    x.PromocaoId == promocao.Id &&
                                    x.HoraInicio <= horario &&
                                    x.HoraFim > horario,
                                cancellationToken);

                    if (!dentroDaFaixa)
                        continue;
                }

                if (!promocao.TodosTiposMaquina)
                {
                    var tipoAssociado =
                        await _context.PromocoesTiposMaquina
                            .AsNoTracking()
                            .AnyAsync(
                                x =>
                                    x.PromocaoId == promocao.Id &&
                                    x.TipoMaquinaId == tipoMaquinaId,
                                cancellationToken);

                    if (!tipoAssociado)
                        continue;
                }

                if (promocao.ValorDescontoHora.HasValue)
                {
                    return new RegraTarifacao(
                        promocao.Id,
                        null,
                        promocao.ValorDescontoHora.Value);
                }

                return new RegraTarifacao(
                    promocao.Id,
                    promocao.PercentualDesconto,
                    null);
            }

            return null;
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

        public async Task<IReadOnlyList<DateTime>> ObterPontosDeQuebraAsync(
    Guid tipoMaquinaId,
    DateTime inicio,
    DateTime fim,
    CancellationToken cancellationToken = default)
        {
            var pontos = new HashSet<DateTime>();

            var configuracoes = await _context.ConfiguracoesTarifacao
                .AsNoTracking()
                .Where(x =>
                    x.TipoMaquinaId == tipoMaquinaId &&
                    x.Ativo &&
                    x.DataInicio <= fim &&
                    (!x.DataFim.HasValue || x.DataFim.Value >= inicio))
                .Select(x => new
                {
                    x.Id
                })
                .ToListAsync(cancellationToken);

            var dataAtual = inicio.Date;

            while (dataAtual <= fim.Date)
            {
                var diaSemana = ConverterDiaSemana(dataAtual.DayOfWeek);

                foreach (var configuracao in configuracoes)
                {
                    var faixas = await _context.FaixasHorarioTarifacao
                        .AsNoTracking()
                        .Where(x =>
                            x.ConfiguracaoTarifacaoId == configuracao.Id &&
                            x.Ativo &&
                            x.DiaSemana == diaSemana)
                        .Select(x => new
                        {
                            x.HoraInicio,
                            x.HoraFim
                        })
                        .ToListAsync(cancellationToken);

                    foreach (var faixa in faixas)
                    {
                        var inicioFaixa = dataAtual.Add(faixa.HoraInicio);

                        var fimFaixa = faixa.HoraFim == TimeSpan.FromTicks(TimeSpan.TicksPerDay - 1)
                            ? dataAtual.AddDays(1)
                            : dataAtual.Add(faixa.HoraFim);

                        if (inicioFaixa > inicio &&
                            inicioFaixa < fim)
                        {
                            pontos.Add(inicioFaixa);
                        }

                        if (fimFaixa > inicio &&
                            fimFaixa < fim)
                        {
                            pontos.Add(fimFaixa);
                        }
                    }
                }

                dataAtual = dataAtual.AddDays(1);
            }

            /*
             * Promoções
             */
            var promocoes = await _context.Promocoes
                .AsNoTracking()
                .Where(x =>
                    x.Ativo &&
                    x.DataInicio <= fim &&
                    (!x.DataFim.HasValue || x.DataFim.Value >= inicio))
                .Select(x => new
                {
                    x.Id,
                    x.DataInicio,
                    x.DataFim
                })
                .ToListAsync(cancellationToken);

            foreach (var promocao in promocoes)
            {
                /*
                 * Início e fim da promoção também podem
                 * representar mudança de regra.
                 */
                if (promocao.DataInicio > inicio &&
                    promocao.DataInicio < fim)
                {
                    pontos.Add(promocao.DataInicio);
                }

                if (promocao.DataFim.HasValue &&
                    promocao.DataFim.Value > inicio &&
                    promocao.DataFim.Value < fim)
                {
                    pontos.Add(promocao.DataFim.Value);
                }

                var diasPromocao = await _context.PromocoesDiasSemana
                    .AsNoTracking()
                    .Where(x => x.PromocaoId == promocao.Id)
                    .Select(x => x.DiaSemana)
                    .ToListAsync(cancellationToken);

                var possuiDiasConfigurados = diasPromocao.Count > 0;

                var faixasPromocao = await _context.PromocoesFaixasHorario
                    .AsNoTracking()
                    .Where(x => x.PromocaoId == promocao.Id)
                    .Select(x => new
                    {
                        x.HoraInicio,
                        x.HoraFim
                    })
                    .ToListAsync(cancellationToken);

                foreach (var dia in Enumerable.Range(
                    0,
                    (fim.Date - inicio.Date).Days + 1))
                {
                    var data = inicio.Date.AddDays(dia);

                    var diaSemana = ConverterDiaSemana(data.DayOfWeek);

                    if (possuiDiasConfigurados &&
                        !diasPromocao.Contains(diaSemana))
                    {
                        continue;
                    }

                    foreach (var faixa in faixasPromocao)
                    {
                        var inicioFaixa = data.Add(faixa.HoraInicio);

                        var fimFaixa = faixa.HoraFim == TimeSpan.MaxValue
                            ? data.AddDays(1)
                            : data.Add(faixa.HoraFim);

                        if (inicioFaixa > inicio &&
                            inicioFaixa < fim)
                        {
                            pontos.Add(inicioFaixa);
                        }

                        if (fimFaixa > inicio &&
                            fimFaixa < fim)
                        {
                            pontos.Add(fimFaixa);
                        }
                    }
                }
            }

            return pontos
                .OrderBy(x => x)
                .ToList();
        }
    }
}
