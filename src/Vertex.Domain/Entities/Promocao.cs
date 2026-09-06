using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Domain.Common;

namespace Vertex.Domain.Entities
{
    public class Promocao : Entity
    {
        public string Nome { get; private set; } = string.Empty;
        public string? Descricao { get; private set; }

        public decimal? PercentualDesconto { get; private set; }
        public decimal? ValorDescontoHora { get; private set; }

        public DateTime DataInicio { get; private set; }
        public DateTime? DataFim { get; private set; }

        public int Prioridade { get; private set; }

        public bool TodosTiposMaquina { get; private set; }

        public bool Ativo { get; private set; }

        public DateTime DataCadastro { get; private set; }

        protected Promocao()
        {
        }

        public Promocao(
            string nome,
            DateTime dataInicio,
            DateTime? dataFim = null,
            string? descricao = null,
            decimal? percentualDesconto = null,
            decimal? valorDescontoHora = null,
            int prioridade = 0,
            bool todosTiposMaquina = true)
        {
            Validar(
                nome,
                dataInicio,
                dataFim,
                percentualDesconto,
                valorDescontoHora,
                prioridade);

            Nome = nome.Trim();
            Descricao = descricao?.Trim();

            PercentualDesconto = percentualDesconto;
            ValorDescontoHora = valorDescontoHora;

            DataInicio = dataInicio;
            DataFim = dataFim;

            Prioridade = prioridade;
            TodosTiposMaquina = todosTiposMaquina;

            Ativo = true;
            DataCadastro = DateTime.UtcNow;
        }

        public void Atualizar(
            string nome,
            DateTime dataInicio,
            DateTime? dataFim,
            string? descricao,
            decimal? percentualDesconto,
            decimal? valorDescontoHora,
            int prioridade,
            bool todosTiposMaquina)
        {
            Validar(
                nome,
                dataInicio,
                dataFim,
                percentualDesconto,
                valorDescontoHora,
                prioridade);

            Nome = nome.Trim();
            Descricao = descricao?.Trim();

            PercentualDesconto = percentualDesconto;
            ValorDescontoHora = valorDescontoHora;

            DataInicio = dataInicio;
            DataFim = dataFim;

            Prioridade = prioridade;
            TodosTiposMaquina = todosTiposMaquina;
        }

        public void Ativar() => Ativo = true;

        public void Desativar() => Ativo = false;

        private static void Validar(
            string nome,
            DateTime dataInicio,
            DateTime? dataFim,
            decimal? percentualDesconto,
            decimal? valorDescontoHora,
            int prioridade)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException(
                    "O nome da promoção é obrigatório.");

            if (dataFim.HasValue && dataFim.Value <= dataInicio)
                throw new ArgumentException(
                    "A data de fim deve ser maior que a data de início.");

            if (prioridade < 0)
                throw new ArgumentException(
                    "A prioridade não pode ser negativa.");

            if (!percentualDesconto.HasValue &&
                !valorDescontoHora.HasValue)
            {
                throw new ArgumentException(
                    "A promoção deve possuir um percentual de desconto ou um valor de desconto por hora.");
            }

            if (percentualDesconto.HasValue &&
                valorDescontoHora.HasValue)
            {
                throw new ArgumentException(
                    "A promoção não pode possuir percentual e valor de desconto por hora simultaneamente.");
            }

            if (percentualDesconto.HasValue &&
                (percentualDesconto.Value <= 0 ||
                 percentualDesconto.Value > 100))
            {
                throw new ArgumentException(
                    "O percentual de desconto deve ser maior que zero e menor ou igual a 100.");
            }

            if (valorDescontoHora.HasValue &&
                valorDescontoHora.Value <= 0)
            {
                throw new ArgumentException(
                    "O valor de desconto por hora deve ser maior que zero.");
            }
        }
    }
}
