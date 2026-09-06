using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Abstractions.Persistence;

namespace Vertex.Application.TariffConfigurations.Commands.AtualizarConfiguracaoTarifacao
{
    public sealed class AtualizarConfiguracaoTarifacaoHandler
    {
        private readonly ITarifacaoRepository _repository;

        public AtualizarConfiguracaoTarifacaoHandler(
            ITarifacaoRepository repository)
        {
            _repository = repository;
        }

        public async Task<AtualizarConfiguracaoTarifacaoResult> HandleAsync(
            AtualizarConfiguracaoTarifacaoCommand command,
            CancellationToken cancellationToken = default)
        {
            var configuracao = await _repository.ObterPorIdAsync(
                command.Id,
                cancellationToken);

            if (configuracao is null)
                throw new KeyNotFoundException(
                    "Configuração de tarifação não encontrada.");

            var nome = command.Nome.Trim();

            var existe = await _repository.ExistePorNomeAsync(
                nome,
                configuracao.TipoMaquinaId,
                cancellationToken);

            if (existe &&
                !string.Equals(
                    configuracao.Nome,
                    nome,
                    StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException(
                    "Já existe uma configuração de tarifação com esse nome para o tipo de máquina informado.");
            }

            configuracao.Atualizar(
                nome,
                command.ValorHora,
                command.DataInicio,
                command.DataFim,
                command.Descricao,
                command.Prioridade);

            await _repository.SalvarAlteracoesAsync(
                cancellationToken);

            return new AtualizarConfiguracaoTarifacaoResult(
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
