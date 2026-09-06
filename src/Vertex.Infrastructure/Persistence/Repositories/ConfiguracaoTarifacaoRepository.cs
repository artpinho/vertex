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
    public sealed class ConfiguracaoTarifacaoRepository
    : ITarifacaoRepository
    {
        private readonly VertexDbContext _context;

        public ConfiguracaoTarifacaoRepository(VertexDbContext context)
        {
            _context = context;
        }

        public async Task<ConfiguracaoTarifacao?> ObterPorIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return await _context.ConfiguracoesTarifacao
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
        }

        public async Task<IReadOnlyList<ConfiguracaoTarifacao>> ListarAsync(
            CancellationToken cancellationToken = default)
        {
            return await _context.ConfiguracoesTarifacao
                .AsNoTracking()
                .OrderBy(x => x.TipoMaquinaId)
                .ThenBy(x => x.Prioridade)
                .ThenBy(x => x.DataInicio)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> ExistePorNomeAsync(
            string nome,
            Guid tipoMaquinaId,
            CancellationToken cancellationToken = default)
        {
            return await _context.ConfiguracoesTarifacao
                .AnyAsync(
                    x =>
                        x.Nome == nome &&
                        x.TipoMaquinaId == tipoMaquinaId,
                    cancellationToken);
        }

        public async Task AdicionarAsync(
            ConfiguracaoTarifacao configuracao,
            CancellationToken cancellationToken = default)
        {
            await _context.ConfiguracoesTarifacao.AddAsync(
                configuracao,
                cancellationToken);
        }

        public async Task SalvarAlteracoesAsync(
            CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
