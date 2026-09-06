using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Abstractions.Persistence;

namespace Vertex.Application.Promotions.Commands.AtualizarPromocao
{
    public class AtualizarPromocaoHandler
    {
        private readonly IPromocaoRepository _repository;

        public AtualizarPromocaoHandler(
            IPromocaoRepository repository)
        {
            _repository = repository;
        }

        public async Task HandleAsync(
            AtualizarPromocaoCommand command,
            CancellationToken cancellationToken = default)
        {
            var promocao = await _repository.ObterPorIdAsync(
                command.Id,
                cancellationToken);

            if (promocao is null)
            {
                throw new KeyNotFoundException(
                    "A promoção informada não foi encontrada.");
            }

            promocao.Atualizar(
                command.Nome,
                command.DataInicio,
                command.DataFim,
                command.Descricao,
                command.PercentualDesconto,
                command.ValorDescontoHora,
                command.Prioridade,
                command.TodosTiposMaquina);

            await _repository.SalvarAlteracoesAsync(
                cancellationToken);
        }
    }
}
