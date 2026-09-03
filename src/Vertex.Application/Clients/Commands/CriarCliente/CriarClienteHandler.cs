using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Abstractions.Persistence;
using Vertex.Domain.Entities;

namespace Vertex.Application.Clients.Commands.CriarCliente
{
    public sealed class CriarClienteHandler
    {
        private readonly IClienteRepository _repository;

        public CriarClienteHandler(
            IClienteRepository repository)
        {
            _repository = repository;
        }

        public async Task<CriarClienteResponse> HandleAsync(
            CriarClienteCommand command,
            CancellationToken cancellationToken)
        {
            var cliente = new Cliente(
                command.Nome,
                command.CPF,
                command.Email,
                command.Telefone,
                command.DataNascimento);

            await _repository.AdicionarAsync(
                cliente,
                cancellationToken);

            await _repository.SalvarAlteracoesAsync(
                cancellationToken);

            return new CriarClienteResponse(
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
