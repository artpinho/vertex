using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Domain.Common;

namespace Vertex.Domain.Entities
{
    public class CarteiraCliente : Entity
    {
        public Guid ClienteId { get; private set; }

        public decimal Saldo { get; private set; }

        protected CarteiraCliente()
        {
        }

        public CarteiraCliente(Guid clienteId)
        {
            if (clienteId == Guid.Empty)
                throw new ArgumentException(
                    "O cliente informado é inválido.");

            ClienteId = clienteId;
            Saldo = 0m;
        }

        public void Creditar(decimal valor)
        {
            if (valor <= 0)
                throw new ArgumentException(
                    "O valor do crédito deve ser maior que zero.");

            Saldo += valor;
        }

        public void Debitar(decimal valor)
        {
            if (valor <= 0)
                throw new ArgumentException(
                    "O valor do débito deve ser maior que zero.");

            if (Saldo < valor)
                throw new InvalidOperationException(
                    "Saldo insuficiente.");

            Saldo -= valor;
        }
    }
}
