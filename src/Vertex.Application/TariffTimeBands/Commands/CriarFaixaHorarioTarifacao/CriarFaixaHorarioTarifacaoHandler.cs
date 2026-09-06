using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Abstractions.Persistence;
using Vertex.Domain.Entities;

namespace Vertex.Application.TariffTimeBands.Commands.CriarFaixaHorarioTarifacao
{
    public sealed class CriarFaixaHorarioTarifacaoHandler
    {
        private readonly IFaixaHorarioTarifacaoRepository _faixaRepository;
        private readonly ITarifacaoRepository _tarifacaoRepository;

        public CriarFaixaHorarioTarifacaoHandler(
            IFaixaHorarioTarifacaoRepository faixaRepository,
            ITarifacaoRepository tarifacaoRepository)
        {
            _faixaRepository = faixaRepository;
            _tarifacaoRepository = tarifacaoRepository;
        }

        public async Task<CriarFaixaHorarioTarifacaoResult> HandleAsync(
            CriarFaixaHorarioTarifacaoCommand command,
            CancellationToken cancellationToken = default)
        {
            var configuracao =
                await _tarifacaoRepository.ObterPorIdAsync(
                    command.ConfiguracaoTarifacaoId,
                    cancellationToken);

            if (configuracao is null)
                throw new KeyNotFoundException(
                    "A configuração de tarifação informada não foi encontrada.");

            if (!configuracao.Ativo)
                throw new InvalidOperationException(
                    "Não é possível adicionar uma faixa a uma configuração de tarifação inativa.");

            var existeSobreposicao =
                await _faixaRepository.ExisteFaixaAsync(
                    command.ConfiguracaoTarifacaoId,
                    command.DiaSemana,
                    command.HoraInicio,
                    command.HoraFim,
                    null,
                    cancellationToken);

            if (existeSobreposicao)
                throw new InvalidOperationException(
                    "Já existe uma faixa de horário que se sobrepõe ao período informado.");

            var faixa = new FaixaHorarioTarifacao(
                command.ConfiguracaoTarifacaoId,
                command.DiaSemana,
                command.HoraInicio,
                command.HoraFim,
                command.ValorHora);

            await _faixaRepository.AdicionarAsync(
                faixa,
                cancellationToken);

            await _faixaRepository.SalvarAlteracoesAsync(
                cancellationToken);

            return new CriarFaixaHorarioTarifacaoResult(
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
