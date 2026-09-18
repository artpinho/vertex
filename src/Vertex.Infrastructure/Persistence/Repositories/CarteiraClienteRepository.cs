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
    public class CarteiraClienteRepository : ICarteiraClienteRepository
    {
        private readonly VertexDbContext _context;

        public CarteiraClienteRepository(VertexDbContext context)
        {
            _context = context;
        }

        public async Task<CarteiraCliente?> ObterPorClienteIdAsync(
            Guid clienteId,
            CancellationToken cancellationToken = default)
        {
            return await _context.CarteirasClientes
                .FirstOrDefaultAsync(
                    x => x.ClienteId == clienteId,
                    cancellationToken);
        }

        public async Task AdicionarAsync(
            CarteiraCliente carteira,
            CancellationToken cancellationToken = default)
        {
            await _context.CarteirasClientes.AddAsync(
                carteira,
                cancellationToken);
        }

        public async Task AdicionarMovimentacaoAsync(
            MovimentacaoCarteira movimentacao,
            CancellationToken cancellationToken = default)
        {
            await _context.MovimentacoesCarteira.AddAsync(
                movimentacao,
                cancellationToken);
        }

        public async Task SalvarAlteracoesAsync(
            CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
