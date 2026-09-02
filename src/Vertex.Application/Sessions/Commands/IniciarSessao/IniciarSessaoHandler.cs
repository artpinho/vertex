using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Abstractions.Persistence;
using Vertex.Domain.Entities;
using Vertex.Domain.Enums;

namespace Vertex.Application.Sessions.Commands.IniciarSessao
{
    public sealed class IniciarSessaoHandler
    {
        private readonly ISessaoRepository _sessaoRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IEstacaoRepository _estacaoRepository;

        public IniciarSessaoHandler(
            ISessaoRepository sessaoRepository,
            IClienteRepository clienteRepository,
            IEstacaoRepository estacaoRepository)
        {
            _sessaoRepository = sessaoRepository;
            _clienteRepository = clienteRepository;
            _estacaoRepository = estacaoRepository;
        }

        public async Task<IniciarSessaoResponse> HandleAsync(
            IniciarSessaoCommand command,
            CancellationToken cancellationToken)
        {
            var cliente =
                await _clienteRepository.ObterPorIdAsync(
                    command.ClienteId,
                    cancellationToken);

            if (cliente is null)
            {
                throw new KeyNotFoundException(
                    "Cliente não encontrado.");
            }

            if (!cliente.Ativo)
            {
                throw new InvalidOperationException(
                    "O cliente está inativo.");
            }

            var estacao =
                await _estacaoRepository.ObterPorIdAsync(
                    command.EstacaoId,
                    cancellationToken);

            if (estacao is null)
            {
                throw new KeyNotFoundException(
                    "Estação não encontrada.");
            }

            if (!estacao.Ativa)
            {
                throw new InvalidOperationException(
                    "Não é possível iniciar uma sessão em uma estação inativa.");
            }

            if (estacao.Status != StatusEstacao.Livre)
            {
                throw new InvalidOperationException(
                    "A estação não está livre.");
            }

            var clientePossuiSessao =
                await _sessaoRepository.ExisteSessaoAtivaPorClienteAsync(
                    command.ClienteId,
                    cancellationToken);

            if (clientePossuiSessao)
            {
                throw new InvalidOperationException(
                    "O cliente já possui uma sessão ativa.");
            }

            var estacaoPossuiSessao =
                await _sessaoRepository.ExisteSessaoAtivaPorEstacaoAsync(
                    command.EstacaoId,
                    cancellationToken);

            if (estacaoPossuiSessao)
            {
                throw new InvalidOperationException(
                    "A estação já possui uma sessão ativa.");
            }

            var sessao = new Sessao(
                command.ClienteId,
                command.EstacaoId);

            estacao.ColocarEmUso();

            await _sessaoRepository.AdicionarAsync(
                sessao,
                cancellationToken);

            await _sessaoRepository.SalvarAlteracoesAsync(
                cancellationToken);

            return new IniciarSessaoResponse(
                sessao.Id,
                sessao.ClienteId,
                sessao.EstacaoId,
                sessao.Inicio,
                sessao.Status);
        }
    }
}
