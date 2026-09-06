using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Abstractions.Persistence;
using Vertex.Domain.Entities;

namespace Vertex.Application.Promotions.Commands.CriarPromocao
{
    public class CriarPromocaoHandler
    {
        private readonly IPromocaoRepository _repository;

        public CriarPromocaoHandler(
            IPromocaoRepository repository)
        {
            _repository = repository;
        }

        public async Task<CriarPromocaoResponse> HandleAsync(
            CriarPromocaoCommand command,
            CancellationToken cancellationToken = default)
        {
            var promocao = new Promocao(
                command.Nome,
                command.DataInicio,
                command.DataFim,
                command.Descricao,
                command.PercentualDesconto,
                command.ValorDescontoHora,
                command.Prioridade,
                command.TodosTiposMaquina);

            await _repository.AdicionarAsync(
                promocao,
                cancellationToken);

            await _repository.SalvarAlteracoesAsync(
                cancellationToken);

            return new CriarPromocaoResponse(
                promocao.Id);
        }
    }
}
