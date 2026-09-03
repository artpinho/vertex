using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Abstractions.Persistence;

namespace Vertex.Application.Clients.Queries
{
    public sealed class ObterClienteHandler
    {
        private readonly IClienteRepository _repository;

        public ObterClienteHandler(
            IClienteRepository repository)
        {
            _repository = repository;
        }

        public async Task<ClienteResponse?> HandleAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            var cliente =
                await _repository.ObterPorIdAsync(
                    id,
                    cancellationToken);

            if (cliente is null)
                return null;

            return new ClienteResponse(
                cliente.Id,
                cliente.Nome,
                cliente.CPF,
                cliente.Email,
                cliente.Telefone,
                cliente.DataNascimento,
                cliente.Ativo,
                cliente.DataCadastro);
        }
    }
}
