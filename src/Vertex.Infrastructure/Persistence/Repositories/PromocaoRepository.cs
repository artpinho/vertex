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
    public class PromocaoRepository : IPromocaoRepository
    {
        private readonly VertexDbContext _context;

        public PromocaoRepository(VertexDbContext context)
        {
            _context = context;
        }

        public async Task<Promocao?> ObterPorIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return await _context.Promocoes
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
        }

        public async Task<IReadOnlyList<Promocao>> ListarAsync(
            CancellationToken cancellationToken = default)
        {
            return await _context.Promocoes
                .AsNoTracking()
                .OrderBy(x => x.Nome)
                .ToListAsync(cancellationToken);
        }

        public async Task AdicionarAsync(
            Promocao promocao,
            CancellationToken cancellationToken = default)
        {
            await _context.Promocoes.AddAsync(
                promocao,
                cancellationToken);
        }

        public async Task SalvarAlteracoesAsync(
            CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
