using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vertex.Application.Abstractions.Persistence;
using Vertex.Domain.Entities;
using Vertex.Infrastructure.Persistence.Context;

namespace Vertex.Infrastructure.Persistence.Repositories
{
    public sealed class ComputadorRepository : IComputadorRepository
    {
        private readonly VertexDbContext _context;

        public ComputadorRepository(VertexDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<bool> ExistePorHostNameAsync(
        string hostname,
        CancellationToken cancellationToken = default)
        {
            return await _context.Computadores
                .AnyAsync(
                    x => x.HostName == hostname,
                    cancellationToken);
        }

        public async Task<bool> ExistePorMacAddressAsync(
            string macAddress,
            CancellationToken cancellationToken = default)
        {
            return await _context.Computadores
                .AnyAsync(
                    x => x.MacAddress == macAddress,
                    cancellationToken);
        }

        public async Task AdicionarAsync(
            Computador computador,
            CancellationToken cancellationToken = default)
        {
            await _context.Computadores.AddAsync(
                computador,
                cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<Computador>> ListarAsync(
    CancellationToken cancellationToken = default)
        {
            return await _context.Computadores
                .AsNoTracking()
                .OrderBy(x => x.HostName)
                .ToListAsync(cancellationToken);
        }

        public async Task<Computador?> ObterPorIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return await _context.Computadores
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
        }

        public async Task AtualizarAsync(
            Computador computador,
            CancellationToken cancellationToken = default)
        {
            _context.Computadores.Update(computador);

            await _context.SaveChangesAsync(cancellationToken);
        }

    }
}
