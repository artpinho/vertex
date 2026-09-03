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
    public class ClienteRepository : IClienteRepository
    {
        private readonly VertexDbContext _context;

        public ClienteRepository(VertexDbContext context)
        {
            _context = context;
        }

        public async Task<Cliente?> ObterPorIdAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            return await _context.Clientes
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
        }

        public async Task AdicionarAsync(
            Cliente cliente,
            CancellationToken cancellationToken)
        {
            await _context.Clientes.AddAsync(
                cliente,
                cancellationToken);
        }

        public async Task SalvarAlteracoesAsync(
            CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<Cliente>> ListarAsync(
            CancellationToken cancellationToken)
        {
            return await _context.Clientes
                .AsNoTracking()
                .OrderBy(x => x.Nome)
                .ToListAsync(cancellationToken);
        }
    }
}
