using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Abstractions.Persistence;
using Vertex.Domain.Entities;
using Vertex.Infrastructure.Persistence.Context;

namespace Vertex.Infrastructure.Persistence.Repositories
{
    public sealed class FaixaHorarioTarifacaoRepository
        : IFaixaHorarioTarifacaoRepository
    {
        private readonly VertexDbContext _context;

        public FaixaHorarioTarifacaoRepository(
            VertexDbContext context)
        {
            _context = context;
        }

        public async Task<FaixaHorarioTarifacao?> ObterPorIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return await _context.FaixasHorarioTarifacao
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
        }

        public async Task<IReadOnlyList<FaixaHorarioTarifacao>> ListarAsync(
            Guid configuracaoTarifacaoId,
            CancellationToken cancellationToken = default)
        {
            return await _context.FaixasHorarioTarifacao
                .AsNoTracking()
                .Where(x =>
                    x.ConfiguracaoTarifacaoId == configuracaoTarifacaoId)
                .OrderBy(x => x.DiaSemana)
                .ThenBy(x => x.HoraInicio)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> ExisteFaixaAsync(
            Guid configuracaoTarifacaoId,
            int diaSemana,
            TimeSpan horaInicio,
            TimeSpan horaFim,
            Guid? idIgnorar = null,
            CancellationToken cancellationToken = default)
        {
            return await _context.FaixasHorarioTarifacao
                .AnyAsync(
                    x =>
                        x.ConfiguracaoTarifacaoId == configuracaoTarifacaoId &&
                        x.DiaSemana == diaSemana &&
                        x.HoraInicio < horaFim &&
                        x.HoraFim > horaInicio &&
                        (!idIgnorar.HasValue ||
                         x.Id != idIgnorar.Value),
                    cancellationToken);
        }

        public async Task AdicionarAsync(
            FaixaHorarioTarifacao faixa,
            CancellationToken cancellationToken = default)
        {
            await _context.FaixasHorarioTarifacao.AddAsync(
                faixa,
                cancellationToken);
        }

        public Task RemoverAsync(
            FaixaHorarioTarifacao faixa,
            CancellationToken cancellationToken = default)
        {
            _context.FaixasHorarioTarifacao.Remove(faixa);

            return Task.CompletedTask;
        }

        public async Task SalvarAlteracoesAsync(
            CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
