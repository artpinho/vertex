using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Abstractions.Persistence;

namespace Vertex.Application.Clients.Commands.AtualizarCliente
{
    public sealed class AtualizarClienteHandler
    {
        private readonly IClienteRepository _repository;

        public AtualizarClienteHandler(
            IClienteRepository repository)
        {
            _repository = repository;
        }

        public async Task<AtualizarClienteResponse> HandleAsync(
            AtualizarClienteCommand command,
            CancellationToken cancellationToken)
        {
            var cliente =
                await _repository.ObterPorIdAsync(
                    command.ClienteId,
                    cancellationToken);

            if (cliente is null)
            {
                throw new KeyNotFoundException(
                    "Cliente não encontrado.");
            }

            cliente.AtualizarDados(
                command.Nome,
                command.CPF,
                command.Email,
                command.Telefone,
                command.DataNascimento);

            await _repository.SalvarAlteracoesAsync(
                cancellationToken);

            return new AtualizarClienteResponse(
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
