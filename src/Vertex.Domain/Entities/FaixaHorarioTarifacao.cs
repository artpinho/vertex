using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Domain.Common;

namespace Vertex.Domain.Entities
{
    public class FaixaHorarioTarifacao : Entity
    {
        public Guid ConfiguracaoTarifacaoId { get; private set; }

        public int DiaSemana { get; private set; }

        public TimeSpan HoraInicio { get; private set; }
        public TimeSpan HoraFim { get; private set; }

        public decimal ValorHora { get; private set; }

        public bool Ativo { get; private set; }

        public DateTime DataCadastro { get; private set; }

        protected FaixaHorarioTarifacao()
        {
        }

        public FaixaHorarioTarifacao(
            Guid configuracaoTarifacaoId,
            int diaSemana,
            TimeSpan horaInicio,
            TimeSpan horaFim,
            decimal valorHora)
        {
            if (configuracaoTarifacaoId == Guid.Empty)
                throw new ArgumentException(
                    "A configuração de tarifação informada é inválida.");

            if (diaSemana < 1 || diaSemana > 7)
                throw new ArgumentException(
                    "O dia da semana deve estar entre 1 e 7.");

            if (horaInicio >= horaFim)
                throw new ArgumentException(
                    "A hora de início deve ser menor que a hora de fim.");

            if (valorHora <= 0)
                throw new ArgumentException(
                    "O valor da hora deve ser maior que zero.");

            ConfiguracaoTarifacaoId = configuracaoTarifacaoId;
            DiaSemana = diaSemana;
            HoraInicio = horaInicio;
            HoraFim = horaFim;
            ValorHora = valorHora;
            Ativo = true;
            DataCadastro = DateTime.UtcNow;
        }

        public void Atualizar(
            int diaSemana,
            TimeSpan horaInicio,
            TimeSpan horaFim,
            decimal valorHora)
        {
            if (diaSemana < 1 || diaSemana > 7)
                throw new ArgumentException(
                    "O dia da semana deve estar entre 1 e 7.");

            if (horaInicio >= horaFim)
                throw new ArgumentException(
                    "A hora de início deve ser menor que a hora de fim.");

            if (valorHora <= 0)
                throw new ArgumentException(
                    "O valor da hora deve ser maior que zero.");

            DiaSemana = diaSemana;
            HoraInicio = horaInicio;
            HoraFim = horaFim;
            ValorHora = valorHora;
        }

        public void Ativar()
        {
            Ativo = true;
        }

        public void Desativar()
        {
            Ativo = false;
        }
    }
}
