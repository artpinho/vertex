using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Domain.Common;

namespace Vertex.Domain.Entities
{
    public class ConsumoTarifacao : Entity
    {
        public Guid SessaoId { get; private set; }

        public DateTime Inicio { get; private set; }
        public DateTime Fim { get; private set; }

        public Guid ConfiguracaoTarifacaoId { get; private set; }
        public Guid? PromocaoId { get; private set; }

        public decimal ValorHora { get; private set; }
        public decimal Desconto { get; private set; }
        public decimal Valor { get; private set; }

        protected ConsumoTarifacao()
        {
        }

        public ConsumoTarifacao(
            Guid sessaoId,
            DateTime inicio,
            DateTime fim,
            Guid configuracaoTarifacaoId,
            Guid? promocaoId,
            decimal valorHora,
            decimal desconto,
            decimal valor)
        {
            if (sessaoId == Guid.Empty)
                throw new ArgumentException("A sessão informada é inválida.");

            if (fim <= inicio)
                throw new ArgumentException(
                    "O fim do consumo deve ser maior que o início.");

            if (configuracaoTarifacaoId == Guid.Empty)
                throw new ArgumentException(
                    "A configuração de tarifação informada é inválida.");

            if (valorHora <= 0)
                throw new ArgumentException(
                    "O valor da hora deve ser maior que zero.");

            if (desconto < 0)
                throw new ArgumentException(
                    "O desconto não pode ser negativo.");

            if (valor < 0)
                throw new ArgumentException(
                    "O valor do consumo não pode ser negativo.");

            SessaoId = sessaoId;
            Inicio = inicio;
            Fim = fim;
            ConfiguracaoTarifacaoId = configuracaoTarifacaoId;
            PromocaoId = promocaoId;
            ValorHora = valorHora;
            Desconto = desconto;
            Valor = valor;
        }
    }
}
