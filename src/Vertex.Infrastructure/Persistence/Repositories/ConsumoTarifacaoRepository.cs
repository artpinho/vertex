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
    public class ConsumoTarifacaoRepository : IConsumoTarifacaoRepository
    {
        private readonly VertexDbContext _context;

        public ConsumoTarifacaoRepository(VertexDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(
            ConsumoTarifacao consumo,
            CancellationToken cancellationToken = default)
        {
            await _context.ConsumosTarifacao.AddAsync(
                consumo,
                cancellationToken);
        }

        public async Task AdicionarListaAsync(
            IReadOnlyList<ConsumoTarifacao> consumos,
            CancellationToken cancellationToken = default)
        {
            await _context.ConsumosTarifacao.AddRangeAsync(
                consumos,
                cancellationToken);
        }

        public async Task<IReadOnlyList<ConsumoTarifacao>> ListarPorSessaoAsync(
            Guid sessaoId,
            CancellationToken cancellationToken = default)
        {
            return await _context.ConsumosTarifacao
                .AsNoTracking()
                .Where(x => x.SessaoId == sessaoId)
                .OrderBy(x => x.Inicio)
                .ToListAsync(cancellationToken);
        }

        public async Task SalvarAlteracoesAsync(
            CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
