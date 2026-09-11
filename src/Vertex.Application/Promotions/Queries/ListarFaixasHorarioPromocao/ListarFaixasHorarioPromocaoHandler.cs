using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Abstractions.Persistence;

namespace Vertex.Application.Promotions.Queries.ListarFaixasHorarioPromocao
{
    public class ListarFaixasHorarioPromocaoHandler
    {
        private readonly IPromocaoRepository _promocaoRepository;
        private readonly IPromocaoFaixaHorarioRepository _repository;

        public ListarFaixasHorarioPromocaoHandler(
            IPromocaoRepository promocaoRepository,
            IPromocaoFaixaHorarioRepository repository)
        {
            _promocaoRepository = promocaoRepository;
            _repository = repository;
        }

        public async Task<IReadOnlyList<ListarFaixasHorarioPromocaoResponse>>
            HandleAsync(
                ListarFaixasHorarioPromocaoQuery query,
                CancellationToken cancellationToken = default)
        {
            var promocao = await _promocaoRepository.ObterPorIdAsync(
                query.PromocaoId,
                cancellationToken);

            if (promocao is null)
                throw new KeyNotFoundException(
                    "A promoção informada não foi encontrada.");

            var faixas = await _repository.ListarAsync(
                query.PromocaoId,
                cancellationToken);

            return faixas
                .Select(x => new ListarFaixasHorarioPromocaoResponse(
                    x.Id,
                    x.HoraInicio,
                    x.HoraFim))
                .ToList();
        }
    }
}
