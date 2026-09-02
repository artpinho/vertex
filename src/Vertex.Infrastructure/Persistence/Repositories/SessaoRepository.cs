using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Abstractions.Persistence;
using Vertex.Domain.Entities;
using Vertex.Infrastructure.Persistence.Context;
using Vertex.Domain.Enums;

namespace Vertex.Infrastructure.Persistence.Repositories
{
    public class SessaoRepository : ISessaoRepository
    {
        private readonly VertexDbContext _context;

        public SessaoRepository(VertexDbContext context)
        {
            _context = context;
        }

        public async Task<Sessao?> ObterPorIdAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            return await _context.Sessoes
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
        }

        public async Task<bool> ExisteSessaoAtivaPorClienteAsync(
            Guid clienteId,
            CancellationToken cancellationToken)
        {
            return await _context.Sessoes
                .AnyAsync(
                    x => x.ClienteId == clienteId
                         && x.Status == StatusSessao.Ativa,
                    cancellationToken);
        }

        public async Task<bool> ExisteSessaoAtivaPorEstacaoAsync(
            Guid estacaoId,
            CancellationToken cancellationToken)
        {
            return await _context.Sessoes
                .AnyAsync(
                    x => x.EstacaoId == estacaoId
                         && x.Status == StatusSessao.Ativa,
                    cancellationToken);
        }

        public async Task AdicionarAsync(
            Sessao sessao,
            CancellationToken cancellationToken)
        {
            await _context.Sessoes.AddAsync(
                sessao,
                cancellationToken);
        }

        public async Task SalvarAlteracoesAsync(
            CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
