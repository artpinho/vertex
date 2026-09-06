using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Abstractions.Persistence;

namespace Vertex.Application.Promotions.Queries.ListarTiposMaquinaPromocao
{
    public class ListarTiposMaquinaPromocaoHandler
    {
        private readonly IPromocaoRepository _promocaoRepository;
        private readonly IPromocaoTipoMaquinaRepository _promocaoTipoMaquinaRepository;
        private readonly ITipoMaquinaRepository _tipoMaquinaRepository;

        public ListarTiposMaquinaPromocaoHandler(
            IPromocaoRepository promocaoRepository,
            IPromocaoTipoMaquinaRepository promocaoTipoMaquinaRepository,
            ITipoMaquinaRepository tipoMaquinaRepository)
        {
            _promocaoRepository = promocaoRepository;
            _promocaoTipoMaquinaRepository = promocaoTipoMaquinaRepository;
            _tipoMaquinaRepository = tipoMaquinaRepository;
        }

        public async Task<IReadOnlyList<ListarTipoMaquinaPromocaoResponse>> HandleAsync(
            ListarTiposMaquinaPromocaoQuery query,
            CancellationToken cancellationToken = default)
        {
            var promocao = await _promocaoRepository.ObterPorIdAsync(
                query.PromocaoId,
                cancellationToken);

            if (promocao is null)
                throw new KeyNotFoundException(
                    "A promoção informada não foi encontrada.");

            var associacoes =
                await _promocaoTipoMaquinaRepository.ListarAsync(
                    query.PromocaoId,
                    cancellationToken);

            var resultado = new List<ListarTipoMaquinaPromocaoResponse>();

            foreach (var associacao in associacoes)
            {
                var tipoMaquina =
                    await _tipoMaquinaRepository.ObterPorIdAsync(
                        associacao.TipoMaquinaId,
                        cancellationToken);

                if (tipoMaquina is null)
                    continue;

                resultado.Add(
                    new ListarTipoMaquinaPromocaoResponse(
                        tipoMaquina.Id,
                        tipoMaquina.Nome,
                        tipoMaquina.Descricao,
                        tipoMaquina.Ativo));
            }

            return resultado;
        }
    }
}
