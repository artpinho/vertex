using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Abstractions.Persistence;

namespace Vertex.Application.Clients.Queries
{
    public sealed class ListarClientesHandler
    {
        private readonly IClienteRepository _repository;

        public ListarClientesHandler(
            IClienteRepository repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyList<ClienteResponse>> HandleAsync(
            CancellationToken cancellationToken)
        {
            var clientes =
                await _repository.ListarAsync(
                    cancellationToken);

            return clientes
                .Select(cliente => new ClienteResponse(
                    cliente.Id,
                    cliente.Nome,
                    cliente.CPF,
                    cliente.Email,
                    cliente.Telefone,
                    cliente.DataNascimento,
                    cliente.Ativo,
                    cliente.DataCadastro))
                .ToList();
        }
    }
}
