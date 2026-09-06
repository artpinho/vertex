using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Abstractions.Persistence;

namespace Vertex.Application.TariffTimeBands.Queries.ListarFaixasHorarioTarifacao
{
    public sealed class ListarFaixasHorarioTarifacaoHandler
    {
        private readonly IFaixaHorarioTarifacaoRepository _repository;

        public ListarFaixasHorarioTarifacaoHandler(
            IFaixaHorarioTarifacaoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyList<ListarFaixasHorarioTarifacaoResult>> HandleAsync(
            ListarFaixasHorarioTarifacaoQuery query,
            CancellationToken cancellationToken = default)
        {
            var faixas = await _repository.ListarAsync(
                query.ConfiguracaoTarifacaoId,
                cancellationToken);

            return faixas
                .Select(x => new ListarFaixasHorarioTarifacaoResult(
                    x.Id,
                    x.ConfiguracaoTarifacaoId,
                    x.DiaSemana,
                    x.HoraInicio,
                    x.HoraFim,
                    x.ValorHora,
                    x.Ativo,
                    x.DataCadastro))
                .ToList();
        }
    }
}
