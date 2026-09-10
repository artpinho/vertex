using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Abstractions.Persistence;

namespace Vertex.Application.Promotions.Queries.ListarDiasSemanaPromocao
{
    public class ListarDiasSemanaPromocaoHandler
    {
        private readonly IPromocaoRepository _promocaoRepository;
        private readonly IPromocaoDiaSemanaRepository _repository;

        public ListarDiasSemanaPromocaoHandler(
            IPromocaoRepository promocaoRepository,
            IPromocaoDiaSemanaRepository repository)
        {
            _promocaoRepository = promocaoRepository;
            _repository = repository;
        }

        public async Task<IReadOnlyList<ListarDiasSemanaPromocaoResponse>>
            HandleAsync(
                ListarDiasSemanaPromocaoQuery query,
                CancellationToken cancellationToken = default)
        {
            var promocao = await _promocaoRepository.ObterPorIdAsync(
                query.PromocaoId,
                cancellationToken);

            if (promocao is null)
                throw new KeyNotFoundException(
                    "A promoção informada não foi encontrada.");

            var dias = await _repository.ListarAsync(
                query.PromocaoId,
                cancellationToken);

            return dias
                .Select(x => new ListarDiasSemanaPromocaoResponse(
                    x.DiaSemana,
                    ObterNomeDiaSemana(x.DiaSemana)))
                .ToList();
        }

        private static string ObterNomeDiaSemana(int diaSemana)
        {
            return diaSemana switch
            {
                1 => "Segunda-feira",
                2 => "Terça-feira",
                3 => "Quarta-feira",
                4 => "Quinta-feira",
                5 => "Sexta-feira",
                6 => "Sábado",
                7 => "Domingo",
                _ => throw new ArgumentException(
                    "O dia da semana deve estar entre 1 e 7.")
            };
        }
    }
}
