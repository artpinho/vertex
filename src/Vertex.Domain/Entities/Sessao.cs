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
        public TipoCobranca TipoCobranca { get; private set; }

        protected Sessao()
        {
        }

        public Sessao(
            Guid clienteId,
            Guid estacaoId,
            TipoCobranca tipoCobranca = TipoCobranca.PrePaga)
        {
            if (clienteId == Guid.Empty)
                throw new ArgumentException("O cliente informado é inválido.");

            if (estacaoId == Guid.Empty)
                throw new ArgumentException("A estação informada é inválida.");

            ClienteId = clienteId;
            EstacaoId = estacaoId;
            TipoCobranca = tipoCobranca;
            Inicio = DateTime.UtcNow;
            Status = StatusSessao.Ativa;
        }

        public void Encerrar(DateTime? fim = null)
        {
            if (Status != StatusSessao.Ativa)
                throw new InvalidOperationException(
                    "Somente uma sessão ativa pode ser encerrada.");

            Fim = fim ?? DateTime.UtcNow;

            if (Fim.Value < Inicio)
                throw new InvalidOperationException(
                    "O fim da sessão não pode ser anterior ao início.");

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
