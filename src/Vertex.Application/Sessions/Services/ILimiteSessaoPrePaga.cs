using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vertex.Application.Sessions.Services
{
    public interface ILimiteSessaoPrePaga
    {
        Task<DateTime?> CalcularFimPermitidoAsync(
            Guid computadorId,
            Guid tipoMaquinaId,
            DateTime inicio,
            decimal saldo,
            CancellationToken cancellationToken = default);
    }
}
