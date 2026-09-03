using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vertex.Application.Clients.Commands.AtualizarCliente
{
    public sealed record AtualizarClienteCommand(
        Guid ClienteId,
        string Nome,
        string? CPF = null,
        string? Email = null,
        string? Telefone = null,
        DateTime? DataNascimento = null);
}
