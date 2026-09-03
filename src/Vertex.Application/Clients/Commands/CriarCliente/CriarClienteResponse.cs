using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vertex.Application.Clients.Commands.CriarCliente
{
    public sealed record CriarClienteResponse(
        Guid Id,
        string Nome,
        string? CPF,
        string? Email,
        string? Telefone,
        DateTime? DataNascimento,
        bool Ativo,
        DateTime DataCadastro);
}
