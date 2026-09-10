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
    public class PromocaoDiaSemanaRepository
        : IPromocaoDiaSemanaRepository
    {
        private readonly VertexDbContext _context;

        public PromocaoDiaSemanaRepository(VertexDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<PromocaoDiaSemana>> ListarAsync(
            Guid promocaoId,
            CancellationToken cancellationToken = default)
        {
            return await _context.PromocoesDiasSemana
                .AsNoTracking()
                .Where(x => x.PromocaoId == promocaoId)
                .OrderBy(x => x.DiaSemana)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> ExisteAsync(
            Guid promocaoId,
            int diaSemana,
            CancellationToken cancellationToken = default)
        {
            return await _context.PromocoesDiasSemana
                .AnyAsync(
                    x => x.PromocaoId == promocaoId &&
                         x.DiaSemana == diaSemana,
                    cancellationToken);
        }

        public async Task AdicionarAsync(
            PromocaoDiaSemana promocaoDiaSemana,
            CancellationToken cancellationToken = default)
        {
            await _context.PromocoesDiasSemana.AddAsync(
                promocaoDiaSemana,
                cancellationToken);
        }

        public async Task RemoverAsync(
            PromocaoDiaSemana promocaoDiaSemana,
            CancellationToken cancellationToken = default)
        {
            _context.PromocoesDiasSemana.Remove(promocaoDiaSemana);

            await Task.CompletedTask;
        }

        public async Task<PromocaoDiaSemana?> ObterAsync(
            Guid promocaoId,
            int diaSemana,
            CancellationToken cancellationToken = default)
        {
            return await _context.PromocoesDiasSemana
                .FirstOrDefaultAsync(
                    x => x.PromocaoId == promocaoId &&
                         x.DiaSemana == diaSemana,
                    cancellationToken);
        }

        public async Task SalvarAlteracoesAsync(
            CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
