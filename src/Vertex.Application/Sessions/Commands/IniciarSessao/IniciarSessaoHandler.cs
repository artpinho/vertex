using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Abstractions.Persistence;
using Vertex.Application.Sessions.Services;
using Vertex.Domain.Entities;
using Vertex.Domain.Enums;

namespace Vertex.Application.Sessions.Commands.IniciarSessao
{
    public sealed class IniciarSessaoHandler
    {
        private readonly ISessaoRepository _sessaoRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IEstacaoRepository _estacaoRepository;
        private readonly ICarteiraClienteRepository _carteiraClienteRepository;
        private readonly ILimiteSessaoPrePaga _limiteSessaoPrePaga;
        private readonly IComputadorRepository _computadorRepository;

        public IniciarSessaoHandler(
            ISessaoRepository sessaoRepository,
            IClienteRepository clienteRepository,
            IEstacaoRepository estacaoRepository,
            IComputadorRepository computadorRepository,
            ICarteiraClienteRepository carteiraClienteRepository,
            ILimiteSessaoPrePaga limiteSessaoPrePaga)
        {
            _sessaoRepository = sessaoRepository;
            _clienteRepository = clienteRepository;
            _estacaoRepository = estacaoRepository;
            _computadorRepository = computadorRepository;
            _carteiraClienteRepository = carteiraClienteRepository;
            _limiteSessaoPrePaga = limiteSessaoPrePaga;
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

            if (!estacao.ComputadorId.HasValue)
            {
                throw new InvalidOperationException(
                    "A estação não possui um computador associado.");
            }

            var computador =
                await _computadorRepository.ObterPorIdAsync(
                    estacao.ComputadorId.Value,
                    cancellationToken);

            if (computador is null)
            {
                throw new KeyNotFoundException(
                    "Computador associado à estação não encontrado.");
            }

            if (!computador.TipoMaquinaId.HasValue)
            {
                throw new InvalidOperationException(
                    "O computador associado à estação não possui um tipo de máquina definido.");
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


            if (command.TipoCobranca == TipoCobranca.PrePaga)
            {
                var carteira =
                    await _carteiraClienteRepository.ObterPorClienteIdAsync(
                        command.ClienteId,
                        cancellationToken);

                if (carteira is null)
                {
                    throw new InvalidOperationException(
                        "O cliente não possui uma carteira.");
                }

                if (carteira.Saldo <= 0)
                {
                    throw new InvalidOperationException(
                        "O cliente não possui saldo disponível.");
                }
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
                command.EstacaoId,
                command.TipoCobranca);

            DateTime? fimPermitido = null;

            if (sessao.TipoCobranca == TipoCobranca.PrePaga)
            {
                var carteira =
                    await _carteiraClienteRepository.ObterPorClienteIdAsync(
                        command.ClienteId,
                        cancellationToken);

                if (carteira is null)
                {
                    throw new InvalidOperationException(
                        "O cliente não possui uma carteira.");
                }

                fimPermitido =
                    await _limiteSessaoPrePaga.CalcularFimPermitidoAsync(
                        computador.Id,
                        computador.TipoMaquinaId!.Value,
                        sessao.Inicio,
                        carteira.Saldo,
                        cancellationToken);
            }

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
                sessao.Status,
                sessao.TipoCobranca,
                fimPermitido);
        }
    }
}
