using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Domain.Common;
using Vertex.Domain.Enums; 

namespace Vertex.Domain.Entities
{
    public class Computador : Entity
    {
        public string HostName { get; private set; } = string.Empty;
        public string? Ip { get; private set; }
        public string? MacAddress { get; private set; }
        public string? SistemaOperacional { get; private set; }
        public string? ClienteVersao { get; private set; }
        public DateTime? UltimoHeartbeat { get; private set; }
        public StatusComputador Status { get; private set; }

        protected Computador() 
        {
        }

        public Computador(string hostName)
        {
            if (string.IsNullOrWhiteSpace(hostName))
                throw new ArgumentException("O HostName do computador é obrigatório.", nameof(hostName));

            HostName = hostName.Trim();
            Status = StatusComputador.Offline;
        }

        public void AtualizarHeartbeat(
            string? ip,
            string? macAddress,
            string? sistemaOperacional,
            string? clienteVersao)
        {
            Ip = ip;
            MacAddress = macAddress;
            SistemaOperacional = sistemaOperacional;
            ClienteVersao = clienteVersao;

            UltimoHeartbeat = DateTime.UtcNow;

            if (Status == StatusComputador.Offline ||
                Status == StatusComputador.Online)
            {
                Status = StatusComputador.Online;
            }
        }

        public void MarcarOffline()
        {
            Status = StatusComputador.Offline;
        }

        public void Bloquear()
        {
            Status = StatusComputador.Bloqueado;
        }

        public void ColocarEmManutencao()
        {
            Status = StatusComputador.Manutencao;
        }

        public void AtualizarInformacoes(
            string? ip, 
            string? macAddress, 
            string? sistemaOperacional, 
            string? clienteVersao)
        {
            Ip = ip;
            MacAddress = macAddress;
            SistemaOperacional = sistemaOperacional;
            ClienteVersao = clienteVersao;
        }

    }
}
