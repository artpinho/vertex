using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Abstractions.Persistence;

namespace Vertex.Application.Promotions.Commands.AlterarStatusPromocao
{
    public class AlterarStatusPromocaoHandler
    {
        private readonly IPromocaoRepository _repository;

        public AlterarStatusPromocaoHandler(
            IPromocaoRepository repository)
        {
            _repository = repository;
        }

        public async Task HandleAsync(
            AlterarStatusPromocaoCommand command,
            CancellationToken cancellationToken = default)
        {
            if (!Enum.IsDefined(command.Operacao))
            {
                throw new ArgumentException(
                    "A operação informada é inválida.");
            }

            var promocao = await _repository.ObterPorIdAsync(
                command.PromocaoId,
                cancellationToken);

            if (promocao is null)
            {
                throw new KeyNotFoundException(
                    "A promoção informada não foi encontrada.");
            }

            switch (command.Operacao)
            {
                case OperacaoPromocao.Ativar:
                    promocao.Ativar();
                    break;

                case OperacaoPromocao.Desativar:
                    promocao.Desativar();
                    break;

                default:
                    throw new ArgumentException(
                        "A operação informada é inválida.");
            }

            await _repository.SalvarAlteracoesAsync(
                cancellationToken);
        }
    }
}
