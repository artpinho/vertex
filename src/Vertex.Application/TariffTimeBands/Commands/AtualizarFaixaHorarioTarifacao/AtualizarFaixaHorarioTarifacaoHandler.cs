using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Application.Abstractions.Persistence;

namespace Vertex.Application.TariffTimeBands.Commands.AtualizarFaixaHorarioTarifacao
{
    public sealed class AtualizarFaixaHorarioTarifacaoHandler
    {
        private readonly IFaixaHorarioTarifacaoRepository _faixaRepository;
        private readonly ITarifacaoRepository _tarifacaoRepository;

        public AtualizarFaixaHorarioTarifacaoHandler(
            IFaixaHorarioTarifacaoRepository faixaRepository,
            ITarifacaoRepository tarifacaoRepository)
        {
            _faixaRepository = faixaRepository;
            _tarifacaoRepository = tarifacaoRepository;
        }

        public async Task HandleAsync(
            AtualizarFaixaHorarioTarifacaoCommand command,
            CancellationToken cancellationToken = default)
        {
            var faixa = await _faixaRepository.ObterPorIdAsync(
                command.Id,
                cancellationToken);

            if (faixa is null)
                throw new KeyNotFoundException(
                    "A faixa de horário informada não foi encontrada.");

            var configuracao =
                await _tarifacaoRepository.ObterPorIdAsync(
                    faixa.ConfiguracaoTarifacaoId,
                    cancellationToken);

            if (configuracao is null)
                throw new KeyNotFoundException(
                    "A configuração de tarifação vinculada à faixa não foi encontrada.");

            if (!configuracao.Ativo)
                throw new InvalidOperationException(
                    "Não é possível alterar uma faixa de uma configuração de tarifação inativa.");

            var existeSobreposicao =
                await _faixaRepository.ExisteFaixaAsync(
                    faixa.ConfiguracaoTarifacaoId,
                    command.DiaSemana,
                    command.HoraInicio,
                    command.HoraFim,
                    command.Id,
                    cancellationToken);

            if (existeSobreposicao)
                throw new InvalidOperationException(
                    "Já existe uma faixa de horário que se sobrepõe ao período informado.");

            faixa.Atualizar(
                command.DiaSemana,
                command.HoraInicio,
                command.HoraFim,
                command.ValorHora);

            await _faixaRepository.SalvarAlteracoesAsync(
                cancellationToken);
        }
    }
}
