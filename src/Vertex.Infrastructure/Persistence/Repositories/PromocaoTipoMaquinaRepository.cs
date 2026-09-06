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
    public class PromocaoTipoMaquinaRepository
        : IPromocaoTipoMaquinaRepository
    {
        private readonly VertexDbContext _context;

        public PromocaoTipoMaquinaRepository(VertexDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExisteAsync(
            Guid promocaoId,
            Guid tipoMaquinaId,
            CancellationToken cancellationToken = default)
        {
            return await _context.PromocoesTiposMaquina
                .AnyAsync(
                    x =>
                        x.PromocaoId == promocaoId &&
                        x.TipoMaquinaId == tipoMaquinaId,
                    cancellationToken);
        }

        public async Task<IReadOnlyList<PromocaoTipoMaquina>> ListarAsync(
            Guid promocaoId,
            CancellationToken cancellationToken = default)
        {
            return await _context.PromocoesTiposMaquina
                .AsNoTracking()
                .Where(x => x.PromocaoId == promocaoId)
                .ToListAsync(cancellationToken);
        }

        public async Task<PromocaoTipoMaquina?> ObterAsync(
            Guid promocaoId,
            Guid tipoMaquinaId,
            CancellationToken cancellationToken = default)
        {
            return await _context.PromocoesTiposMaquina
                .FirstOrDefaultAsync(
                    x =>
                        x.PromocaoId == promocaoId &&
                        x.TipoMaquinaId == tipoMaquinaId,
                    cancellationToken);
        }

        public async Task AdicionarAsync(
            PromocaoTipoMaquina associacao,
            CancellationToken cancellationToken = default)
        {
            await _context.PromocoesTiposMaquina
                .AddAsync(associacao, cancellationToken);
        }

        public Task RemoverAsync(
            PromocaoTipoMaquina associacao,
            CancellationToken cancellationToken = default)
        {
            _context.PromocoesTiposMaquina.Remove(associacao);

            return Task.CompletedTask;
        }

        public async Task SalvarAlteracoesAsync(
            CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<PromocaoTipoMaquina>> ListarComTipoMaquinaAsync(
            Guid promocaoId,
            CancellationToken cancellationToken = default)
        {
            return await _context.PromocoesTiposMaquina
                .AsNoTracking()
                .Where(x => x.PromocaoId == promocaoId)
                .ToListAsync(cancellationToken);
        }
    }
}
