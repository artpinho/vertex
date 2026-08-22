using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Abstractions.Persistence;
using Vertex.Application.Computers.DTOs;
using Vertex.Domain.Entities;

namespace Vertex.Application.Computers.Commands.RegistrarComputador
{
    public sealed class RegistrarComputadorHandler
    {
        private readonly IComputadorRepository _repository;

        public RegistrarComputadorHandler(
            IComputadorRepository repository)
        {
            _repository = repository;
        }

        public async Task<ComputadorResponse> HandleAsync(
            RegistrarComputadorCommand command,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(command.Hostname))
                throw new ArgumentException(
                    "O hostname do computador é obrigatório.");

            var hostname = command.Hostname.Trim();

            var existeHostname =
                await _repository.ExistePorHostNameAsync(
                    hostname,
                    cancellationToken);

            if (existeHostname)
                throw new InvalidOperationException(
                    $"Já existe um computador registrado com o hostname '{hostname}'.");

            if (!string.IsNullOrWhiteSpace(command.MacAddress))
            {
                var macAddress = command.MacAddress.Trim();

                var existeMac =
                    await _repository.ExistePorMacAddressAsync(
                        macAddress,
                        cancellationToken);

                if (existeMac)
                    throw new InvalidOperationException(
                        $"Já existe um computador registrado com o MAC '{macAddress}'.");
            }

            var computador = new Computador(hostname);

            computador.AtualizarInformacoes(
                command.Ip,
                command.MacAddress,
                command.SistemaOperacional,
                command.ClienteVersao);

            await _repository.AdicionarAsync(
                computador,
                cancellationToken);

            return new ComputadorResponse(
                computador.Id,
                computador.HostName,
                computador.Ip,
                computador.MacAddress,
                computador.SistemaOperacional,
                computador.ClienteVersao,
                computador.UltimoHeartbeat,
                computador.Status);
        }
    }
}
