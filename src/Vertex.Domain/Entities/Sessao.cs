using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Domain.Common;
using Vertex.Domain.Enums;

namespace Vertex.Domain.Entities
{
    public class Sessao : AggregateRoot
    {
        public Guid ClienteId { get; private set; }
        public Guid EstacaoId { get; private set; }
        public DateTime Inicio { get; private set; }
        public DateTime? Fim { get; private set; }
        public StatusSessao Status { get; private set; }

        protected Sessao()
        {
        }

        public Sessao(
            Guid clienteId,
            Guid estacaoId)
        {
            if (clienteId == Guid.Empty)
                throw new ArgumentException("O cliente informado é inválido.");

            if (estacaoId == Guid.Empty)
                throw new ArgumentException("A estação informada é inválida.");

            ClienteId = clienteId;
            EstacaoId = estacaoId;

            Inicio = DateTime.UtcNow;
            Status = StatusSessao.Ativa;
        }

        public void Encerrar()
        {
            if (Status != StatusSessao.Ativa)
                throw new InvalidOperationException(
                    "Somente uma sessão ativa pode ser encerrada.");

            Fim = DateTime.UtcNow;
            Status = StatusSessao.Encerrada;
        }

        public void Cancelar()
        {
            if (Status != StatusSessao.Ativa)
                throw new InvalidOperationException(
                    "Somente uma sessão ativa pode ser cancelada.");

            Fim = DateTime.UtcNow;
            Status = StatusSessao.Cancelada;
        }

        public TimeSpan Duracao
        {
            get
            {
                var fim = Fim ?? DateTime.UtcNow;

                return fim - Inicio;
            }
        }
    }
}
