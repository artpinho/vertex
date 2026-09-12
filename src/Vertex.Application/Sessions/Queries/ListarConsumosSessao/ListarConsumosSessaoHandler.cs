using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Abstractions.Persistence;
using Vertex.Application.Sessions.DTOs;

namespace Vertex.Application.Sessions.Queries.ListarConsumosSessao
{
    public sealed class ListarConsumosSessaoHandler
    {
        private readonly ISessaoRepository _sessaoRepository;
        private readonly IConsumoTarifacaoRepository _consumoRepository;

        public ListarConsumosSessaoHandler(
            ISessaoRepository sessaoRepository,
            IConsumoTarifacaoRepository consumoRepository)
        {
            _sessaoRepository = sessaoRepository;
            _consumoRepository = consumoRepository;
        }

        public async Task<ConsumosSessaoResponse?> HandleAsync(
            ListarConsumosSessaoQuery query,
            CancellationToken cancellationToken = default)
        {
            var sessao = await _sessaoRepository.ObterPorIdAsync(
                query.SessaoId,
                cancellationToken);

            if (sessao is null)
                return null;

            var consumos = await _consumoRepository.ListarPorSessaoAsync(
                query.SessaoId,
                cancellationToken);

            var response = consumos
                .Select(x => new ConsumoTarifacaoResponse(
                    x.Id,
                    x.SessaoId,
                    x.Inicio,
                    x.Fim,
                    x.ConfiguracaoTarifacaoId,
                    x.PromocaoId,
                    x.ValorHora,
                    x.Desconto,
                    x.Valor))
                .ToList();

            return new ConsumosSessaoResponse(
                sessao.Id,
                response.Sum(x => x.Valor),
                response);
        }
    }
}
