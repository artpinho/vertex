using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Abstractions.Persistence;
using Vertex.Domain.Entities;

namespace Vertex.Application.Stations.Commands.CriarEstacao
{
    public sealed class CriarEstacaoHandler
    {
        private readonly IEstacaoRepository _repository;

        public CriarEstacaoHandler(
            IEstacaoRepository repository)
        {
            _repository = repository;
        }

        public async Task<CriarEstacaoResponse> HandleAsync(
            CriarEstacaoCommand command,
            CancellationToken cancellationToken)
        {
            var numeroExiste =
                await _repository.ExistePorNumeroAsync(
                    command.Numero,
                    cancellationToken);

            if (numeroExiste)
            {
                throw new InvalidOperationException(
                    $"Já existe uma estação com o número {command.Numero}.");
            }

            var estacao = new Estacao(
                command.Nome,
                command.Numero);

            await _repository.AdicionarAsync(
                estacao,
                cancellationToken);

            await _repository.SalvarAlteracoesAsync(
                cancellationToken);

            return new CriarEstacaoResponse(
                estacao.Id,
                estacao.Nome,
                estacao.Numero,
                estacao.Status,
                estacao.Ativa,
                estacao.ComputadorId);
        }
    }
}
