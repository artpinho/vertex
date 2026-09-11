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
    public class PromocaoFaixaHorarioRepository
        : IPromocaoFaixaHorarioRepository
    {
        private readonly VertexDbContext _context;

        public PromocaoFaixaHorarioRepository(
            VertexDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<PromocaoFaixaHorario>> ListarAsync(
            Guid promocaoId,
            CancellationToken cancellationToken = default)
        {
            return await _context.PromocoesFaixasHorario
                .AsNoTracking()
                .Where(x => x.PromocaoId == promocaoId)
                .OrderBy(x => x.HoraInicio)
                .ToListAsync(cancellationToken);
        }

        public async Task<PromocaoFaixaHorario?> ObterPorIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return await _context.PromocoesFaixasHorario
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
        }

        public async Task<bool> ExisteSobreposicaoAsync(
            Guid promocaoId,
            TimeSpan horaInicio,
            TimeSpan horaFim,
            Guid? idIgnorar = null,
            CancellationToken cancellationToken = default)
        {
            return await _context.PromocoesFaixasHorario
                .AnyAsync(
                    x =>
                        x.PromocaoId == promocaoId &&
                        (!idIgnorar.HasValue || x.Id != idIgnorar.Value) &&
                        x.HoraInicio < horaFim &&
                        x.HoraFim > horaInicio,
                    cancellationToken);
        }

        public async Task AdicionarAsync(
            PromocaoFaixaHorario faixa,
            CancellationToken cancellationToken = default)
        {
            await _context.PromocoesFaixasHorario.AddAsync(
                faixa,
                cancellationToken);
        }

        public async Task RemoverAsync(
            PromocaoFaixaHorario faixa,
            CancellationToken cancellationToken = default)
        {
            _context.PromocoesFaixasHorario.Remove(faixa);

            await Task.CompletedTask;
        }

        public async Task SalvarAlteracoesAsync(
            CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
