using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Abstractions.Persistence;

namespace Vertex.Application.TariffConfigurations.Queries.ObterConfiguracaoTarifacao
{
    public sealed class ObterConfiguracaoTarifacaoHandler
    {
        private readonly ITarifacaoRepository _repository;

        public ObterConfiguracaoTarifacaoHandler(
            ITarifacaoRepository repository)
        {
            _repository = repository;
        }

        public async Task<ObterConfiguracaoTarifacaoResult?> HandleAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            var configuracao = await _repository.ObterPorIdAsync(
                id,
                cancellationToken);

            if (configuracao is null)
                return null;

            return new ObterConfiguracaoTarifacaoResult(
                configuracao.Id,
                configuracao.Nome,
                configuracao.Descricao,
                configuracao.TipoMaquinaId,
                configuracao.ValorHora,
                configuracao.DataInicio,
                configuracao.DataFim,
                configuracao.Prioridade,
                configuracao.Ativo,
                configuracao.DataCadastro);
        }
    }
}
