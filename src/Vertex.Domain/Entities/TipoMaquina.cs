using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Domain.Common;

namespace Vertex.Domain.Entities
{
    public class TipoMaquina : Entity
    {
        public string Nome { get; private set; } = string.Empty;
        public string? Descricao { get; private set; }
        public bool Ativo { get; private set; }
        public DateTime DataCadastro { get; private set; }

        protected TipoMaquina()
        {
        }

        public TipoMaquina(
            string nome,
            string? descricao = null)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException(
                    "O nome do tipo de máquina é obrigatório.");

            Nome = nome.Trim();
            Descricao = descricao?.Trim();
            Ativo = true;
            DataCadastro = DateTime.UtcNow;
        }

        public void Atualizar(
            string nome,
            string? descricao)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException(
                    "O nome do tipo de máquina é obrigatório.");

            Nome = nome.Trim();
            Descricao = descricao?.Trim();
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
