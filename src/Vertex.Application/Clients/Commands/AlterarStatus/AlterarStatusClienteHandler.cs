using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Abstractions.Persistence;

namespace Vertex.Application.Clients.Commands.AlterarStatus
{
    public sealed class AlterarStatusClienteHandler
    {
        private readonly IClienteRepository _repository;

        public AlterarStatusClienteHandler(
            IClienteRepository repository)
        {
            _repository = repository;
        }

        public async Task HandleAsync(
            AlterarStatusClienteCommand command,
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

            if (command.Ativo)
            {
                cliente.Ativar();
            }
            else
            {
                cliente.Desativar();
            }

            await _repository.SalvarAlteracoesAsync(
                cancellationToken);
        }
    }
}
