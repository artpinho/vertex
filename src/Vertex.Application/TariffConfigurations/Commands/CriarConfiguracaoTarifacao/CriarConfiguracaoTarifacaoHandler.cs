using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Abstractions.Persistence;
using Vertex.Domain.Entities;

namespace Vertex.Application.TariffConfigurations.Commands.CriarConfiguracaoTarifacao
{
    public sealed class CriarConfiguracaoTarifacaoHandler
    {
        private readonly ITarifacaoRepository _tarifacaoRepository;
        private readonly ITipoMaquinaRepository _tipoMaquinaRepository;

        public CriarConfiguracaoTarifacaoHandler(
            ITarifacaoRepository tarifacaoRepository,
            ITipoMaquinaRepository tipoMaquinaRepository)
        {
            _tarifacaoRepository = tarifacaoRepository;
            _tipoMaquinaRepository = tipoMaquinaRepository;
        }

        public async Task<CriarConfiguracaoTarifacaoResult> HandleAsync(
            CriarConfiguracaoTarifacaoCommand command,
            CancellationToken cancellationToken = default)
        {
            var tipoMaquina =
                await _tipoMaquinaRepository.ObterPorIdAsync(
                    command.TipoMaquinaId,
                    cancellationToken);

            if (tipoMaquina is null)
                throw new KeyNotFoundException(
                    "O tipo de máquina informado não foi encontrado.");

            if (!tipoMaquina.Ativo)
                throw new InvalidOperationException(
                    "Não é possível criar uma configuração para um tipo de máquina inativo.");

            var nome = command.Nome.Trim();

            var existe =
                await _tarifacaoRepository.ExistePorNomeAsync(
                    nome,
                    command.TipoMaquinaId,
                    cancellationToken);

            if (existe)
                throw new InvalidOperationException(
                    "Já existe uma configuração de tarifação com esse nome para o tipo de máquina informado.");

            var configuracao = new ConfiguracaoTarifacao(
                nome,
                command.TipoMaquinaId,
                command.ValorHora,
                command.DataInicio,
                command.DataFim,
                command.Descricao,
                command.Prioridade);

            await _tarifacaoRepository.AdicionarAsync(
                configuracao,
                cancellationToken);

            await _tarifacaoRepository.SalvarAlteracoesAsync(
                cancellationToken);

            return new CriarConfiguracaoTarifacaoResult(
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
