using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Abstractions.Persistence;
using Vertex.Domain.Entities;
using Vertex.Infrastructure.Persistence.Context;

namespace Vertex.Infrastructure.Persistence.Repositories;

public class EstacaoRepository : IEstacaoRepository
{
    private readonly VertexDbContext _context;

    public EstacaoRepository(VertexDbContext context)
    {
        _context = context;
    }

    public async Task<Estacao?> ObterPorIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        return await _context.Estacoes
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task<IReadOnlyList<Estacao>> ListarAsync(
        CancellationToken cancellationToken)
    {
        return await _context.Estacoes
            .AsNoTracking()
            .OrderBy(x => x.Numero)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistePorNumeroAsync(
        int numero,
        CancellationToken cancellationToken)
    {
        return await _context.Estacoes
            .AnyAsync(
                x => x.Numero == numero,
                cancellationToken);
    }

    public async Task AdicionarAsync(
        Estacao estacao,
        CancellationToken cancellationToken)
    {
        await _context.Estacoes.AddAsync(
            estacao,
            cancellationToken);
    }

    public async Task SalvarAlteracoesAsync(
        CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExisteComComputadorAsync(
        Guid computadorId,
        Guid estacaoId,
        CancellationToken cancellationToken)
    {
        return await _context.Estacoes
            .AnyAsync(
                x => x.ComputadorId == computadorId
                     && x.Id != estacaoId,
                cancellationToken);
    }
}