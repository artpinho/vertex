using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Abstractions.Persistence;

namespace Vertex.Application.Sessions.Queries
{
    public class ObterSessaoHandler
    {
        private readonly ISessaoRepository _sessaoRepository;

        public ObterSessaoHandler(
            ISessaoRepository sessaoRepository)
        {
            _sessaoRepository = sessaoRepository;
        }

        public async Task<SessaoResponse?> HandleAsync(
            Guid sessaoId,
            CancellationToken cancellationToken)
        {
            var sessao = await _sessaoRepository.ObterPorIdAsync(
                sessaoId,
                cancellationToken);

            if (sessao is null)
                return null;

            return new SessaoResponse(
                sessao.Id,
                sessao.ClienteId,
                sessao.EstacaoId,
                sessao.Inicio,
                sessao.Fim,
                sessao.Duracao,
                sessao.Status);
        }
    }
}
