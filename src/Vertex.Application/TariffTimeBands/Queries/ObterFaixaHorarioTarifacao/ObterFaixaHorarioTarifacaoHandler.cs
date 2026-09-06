using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Abstractions.Persistence;

namespace Vertex.Application.TariffTimeBands.Queries.ObterFaixaHorarioTarifacao
{
    public sealed class ObterFaixaHorarioTarifacaoHandler
    {
        private readonly IFaixaHorarioTarifacaoRepository _repository;

        public ObterFaixaHorarioTarifacaoHandler(
            IFaixaHorarioTarifacaoRepository repository)
        {
            _repository = repository;
        }

        public async Task<ObterFaixaHorarioTarifacaoResult?> HandleAsync(
            ObterFaixaHorarioTarifacaoQuery query,
            CancellationToken cancellationToken = default)
        {
            var faixa = await _repository.ObterPorIdAsync(
                query.Id,
                cancellationToken);

            if (faixa is null)
                return null;

            return new ObterFaixaHorarioTarifacaoResult(
                faixa.Id,
                faixa.ConfiguracaoTarifacaoId,
                faixa.DiaSemana,
                faixa.HoraInicio,
                faixa.HoraFim,
                faixa.ValorHora,
                faixa.Ativo,
                faixa.DataCadastro);
        }
    }
}
