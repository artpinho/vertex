using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Domain.Common;

namespace Vertex.Domain.Entities
{
    public class ConfiguracaoTarifacao : Entity
    {
        public string Nome { get; private set; } = string.Empty;
        public string? Descricao { get; private set; }

        public Guid TipoMaquinaId { get; private set; }

        public decimal ValorHora { get; private set; }

        public DateTime DataInicio { get; private set; }
        public DateTime? DataFim { get; private set; }

        public int Prioridade { get; private set; }

        public bool Ativo { get; private set; }

        public DateTime DataCadastro { get; private set; }

        protected ConfiguracaoTarifacao()
        {
        }

        public ConfiguracaoTarifacao(
            string nome,
            Guid tipoMaquinaId,
            decimal valorHora,
            DateTime dataInicio,
            DateTime? dataFim = null,
            string? descricao = null,
            int prioridade = 0)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException(
                    "O nome da configuração de tarifação é obrigatório.");

            if (tipoMaquinaId == Guid.Empty)
                throw new ArgumentException(
                    "O tipo de máquina informado é inválido.");

            if (valorHora <= 0)
                throw new ArgumentException(
                    "O valor da hora deve ser maior que zero.");

            if (dataFim.HasValue && dataFim.Value <= dataInicio)
                throw new ArgumentException(
                    "A data de fim deve ser maior que a data de início.");

            if (prioridade < 0)
                throw new ArgumentException(
                    "A prioridade não pode ser negativa.");

            Nome = nome.Trim();
            Descricao = descricao?.Trim();
            TipoMaquinaId = tipoMaquinaId;
            ValorHora = valorHora;
            DataInicio = dataInicio;
            DataFim = dataFim;
            Prioridade = prioridade;
            Ativo = true;
            DataCadastro = DateTime.UtcNow;
        }

        public void Atualizar(
            string nome,
            decimal valorHora,
            DateTime dataInicio,
            DateTime? dataFim,
            string? descricao,
            int prioridade)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException(
                    "O nome da configuração de tarifação é obrigatório.");

            if (valorHora <= 0)
                throw new ArgumentException(
                    "O valor da hora deve ser maior que zero.");

            if (dataFim.HasValue && dataFim.Value <= dataInicio)
                throw new ArgumentException(
                    "A data de fim deve ser maior que a data de início.");

            if (prioridade < 0)
                throw new ArgumentException(
                    "A prioridade não pode ser negativa.");

            Nome = nome.Trim();
            Descricao = descricao?.Trim();
            ValorHora = valorHora;
            DataInicio = dataInicio;
            DataFim = dataFim;
            Prioridade = prioridade;
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
