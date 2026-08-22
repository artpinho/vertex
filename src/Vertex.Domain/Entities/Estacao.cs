using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Domain.Common;
using Vertex.Domain.Enums;

namespace Vertex.Domain.Entities
{
    public class Estacao : Entity
    {
        public string Nome { get; private set; } = string.Empty;
        public int Numero { get; private set; }
        public StatusEstacao Status { get; private set; }
        public bool Ativa { get; private set; }
        public Guid? ComputadorId { get; private set; }

        protected Estacao()
        {
        }

        public Estacao(string nome, int numero)
        {
            if (string.IsNullOrEmpty(nome))
                throw new ArgumentException("O nome da estação é obrigatório.");

            if (numero <= 0)
                throw new ArgumentException("O número da estação deve ser maior que zero.");

            Nome = nome.Trim();
            Numero = numero;

            Status = StatusEstacao.Livre;
            Ativa = true;
        }

        public void AssociarComputador(Guid computadorId)
        {
            if(computadorId == Guid.Empty)
                throw new ArgumentException("O ID do computador informado é inválido.");

            ComputadorId = computadorId;
        }

        public void Liberar()
        {
            Status = StatusEstacao.Livre;
        }

        public void ColocarEmUso()
        {
            if (!Ativa)
                throw new InvalidOperationException("Não é possível utilizar uma estação inativa.");

            Status = StatusEstacao.EmUso;
        }

        public void Bloquear()
        {
            Status = StatusEstacao.Bloqueada;
        }

        public void ColocarEmManutencao()
        {
            Status = StatusEstacao.Manutencao;
        }

        public void Desativar()
        {
            Ativa = false;
            Status = StatusEstacao.Manutencao;
        }

        public void Ativar()
        {
            Ativa = true;
            Status = StatusEstacao.Livre;
        }

    }
}
