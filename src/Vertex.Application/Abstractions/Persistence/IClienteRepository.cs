using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Domain.Entities;

namespace Vertex.Application.Abstractions.Persistence
{
    public interface IClienteRepository
    {
        Task<Cliente?> ObterPorIdAsync(
            Guid id,
            CancellationToken cancellationToken);
    }
}
