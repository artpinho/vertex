using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Abstractions.Persistence;

namespace Vertex.Application.Sessions.Commands.EncerrarSessao
{
    public sealed class EncerrarSessaoHandler
    {
        private readonly ISessaoRepository _sessaoRepository;
        private readonly IEstacaoRepository _estacaoRepository;

        public EncerrarSessaoHandler(
            ISessaoRepository sessaoRepository,
            IEstacaoRepository estacaoRepository)
        {
            _sessaoRepository = sessaoRepository;
            _estacaoRepository = estacaoRepository;
        }

        public async Task<EncerrarSessaoResponse> HandleAsync(
            EncerrarSessaoCommand command,
            CancellationToken cancellationToken)
        {
            var sessao =
                await _sessaoRepository.ObterPorIdAsync(
                    command.SessaoId,
                    cancellationToken);

            if (sessao is null)
            {
                throw new KeyNotFoundException(
                    "Sessão não encontrada.");
            }

            var estacao =
                await _estacaoRepository.ObterPorIdAsync(
                    sessao.EstacaoId,
                    cancellationToken);

            if (estacao is null)
            {
                throw new KeyNotFoundException(
                    "Estação da sessão não encontrada.");
            }

            sessao.Encerrar();

            estacao.Liberar();

            await _sessaoRepository.SalvarAlteracoesAsync(
                cancellationToken);

            return new EncerrarSessaoResponse(
                sessao.Id,
                sessao.ClienteId,
                sessao.EstacaoId,
                sessao.Inicio,
                sessao.Fim!.Value,
                sessao.Duracao,
                sessao.Status);
        }
    }
}
