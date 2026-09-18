using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Domain.Common;
using Vertex.Domain.Enums;

namespace Vertex.Domain.Entities
{
    public class MovimentacaoCarteira : Entity
    {
        public Guid CarteiraClienteId { get; private set; }

        public decimal Valor { get; private set; }

        public TipoMovimentacaoCarteira Tipo { get; private set; }

        public TipoPagamento? TipoPagamento { get; private set; }

        public Guid? SessaoId { get; private set; }

        public Guid? VendaId { get; private set; }

        public DateTime Data { get; private set; }

        public string? Descricao { get; private set; }

        protected MovimentacaoCarteira()
        {
        }

        public MovimentacaoCarteira(
            Guid carteiraClienteId,
            decimal valor,
            TipoMovimentacaoCarteira tipo,
            TipoPagamento? tipoPagamento = null,
            Guid? sessaoId = null,
            Guid? vendaId = null,
            string? descricao = null)
        {
            if (carteiraClienteId == Guid.Empty)
                throw new ArgumentException(
                    "A carteira informada é inválida.");

            if (valor <= 0)
                throw new ArgumentException(
                    "O valor da movimentação deve ser maior que zero.");

            if (tipo == TipoMovimentacaoCarteira.Credito &&
                tipoPagamento is null)
            {
                throw new ArgumentException(
                    "O tipo de pagamento é obrigatório para créditos.");
            }

            if (tipo == TipoMovimentacaoCarteira.Debito &&
                tipoPagamento is not null)
            {
                throw new ArgumentException(
                    "Débitos de carteira não possuem tipo de pagamento.");
            }

            if (tipo == TipoMovimentacaoCarteira.Debito &&
                sessaoId is null &&
                vendaId is null)
            {
                throw new ArgumentException(
                    "O débito deve estar vinculado a uma sessão ou venda.");
            }

            CarteiraClienteId = carteiraClienteId;
            Valor = valor;
            Tipo = tipo;
            TipoPagamento = tipoPagamento;
            SessaoId = sessaoId;
            VendaId = vendaId;
            Descricao = descricao?.Trim();
            Data = DateTime.UtcNow;
        }
    }
}
