using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Abstractions.Persistence;
using Vertex.Domain.Entities;
using Vertex.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Vertex.Infrastructure.Persistence.Repositories;

public sealed class ComputadorCredentialRepository
    : IComputadorCredentialRepository
{
    private readonly VertexDbContext _context;

    public ComputadorCredentialRepository(
        VertexDbContext context)
    {
        _context = context;
    }

    public async Task<ComputadorCredential?> ObterPorComputadorIdAsync(
        Guid computadorId,
        CancellationToken cancellationToken = default)
    {
        return await _context.ComputadorCredentials
            .FirstOrDefaultAsync(
                x => x.ComputadorId == computadorId,
                cancellationToken);
    }

    public async Task<ComputadorCredential?> ObterPorClientIdAsync(
        string clientId,
        CancellationToken cancellationToken = default)
    {
        return await _context.ComputadorCredentials
            .FirstOrDefaultAsync(
                x => x.ClientId == clientId,
                cancellationToken);
    }

    public async Task AdicionarAsync(
        ComputadorCredential credential,
        CancellationToken cancellationToken = default)
    {
        await _context.ComputadorCredentials.AddAsync(
            credential,
            cancellationToken);

        await _context.SaveChangesAsync(
            cancellationToken);
    }
}
