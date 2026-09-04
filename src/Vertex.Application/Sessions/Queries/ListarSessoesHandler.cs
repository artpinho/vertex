using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Abstractions.Persistence;

namespace Vertex.Application.Sessions.Queries
{
    public class ListarSessoesHandler
    {
        private readonly ISessaoRepository _sessaoRepository;

        public ListarSessoesHandler(
            ISessaoRepository sessaoRepository)
        {
            _sessaoRepository = sessaoRepository;
        }

        public async Task<IReadOnlyList<SessaoResponse>> HandleAsync(
            CancellationToken cancellationToken)
        {
            var sessoes = await _sessaoRepository.ListarAsync(
                cancellationToken);

            return sessoes
                .Select(x => new SessaoResponse(
                    x.Id,
                    x.ClienteId,
                    x.EstacaoId,
                    x.Inicio,
                    x.Fim,
                    x.Duracao,
                    x.Status))
                .ToList();
        }
    }
}
