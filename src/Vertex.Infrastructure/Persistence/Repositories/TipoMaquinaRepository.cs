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
    public sealed class TipoMaquinaRepository : ITipoMaquinaRepository
    {
        private readonly VertexDbContext _context;

        public TipoMaquinaRepository(VertexDbContext context)
        {
            _context = context;
        }

        public async Task<TipoMaquina?> ObterPorIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return await _context.TiposMaquina
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
        }

        public async Task<IReadOnlyList<TipoMaquina>> ListarAsync(
            CancellationToken cancellationToken = default)
        {
            return await _context.TiposMaquina
                .AsNoTracking()
                .OrderBy(x => x.Nome)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> ExistePorNomeAsync(
            string nome,
            CancellationToken cancellationToken = default)
        {
            return await _context.TiposMaquina
                .AnyAsync(
                    x => x.Nome == nome,
                    cancellationToken);
        }

        public async Task AdicionarAsync(
            TipoMaquina tipoMaquina,
            CancellationToken cancellationToken = default)
        {
            await _context.TiposMaquina.AddAsync(
                tipoMaquina,
                cancellationToken);
        }

        public async Task SalvarAlteracoesAsync(
            CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
