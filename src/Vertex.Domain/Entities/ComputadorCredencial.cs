using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Domain.Common;
using Vertex.Domain.Enums;

namespace Vertex.Domain.Entities;

public class ComputadorCredential : Entity
{
    private ComputadorCredential()
    {
    }
    public ComputadorCredential(
        Guid computadorId,
        string clientId,
        string secretHash)
    {
        if (computadorId == Guid.Empty)
            throw new ArgumentException(
                "O computador é obrigatório.",
                nameof(computadorId));

        if (string.IsNullOrWhiteSpace(clientId))
            throw new ArgumentException(
                "O ClientId é obrigatório.",
                nameof(clientId));

        if (string.IsNullOrWhiteSpace(secretHash))
            throw new ArgumentException(
                "O hash do segredo é obrigatório.",
                nameof(secretHash));

        ComputadorId = computadorId;
        ClientId = clientId;
        SecretHash = secretHash;
        Status = StatusCredential.Ativa;
        DataCriacao = DateTime.UtcNow;
    }

    public Guid ComputadorId { get; private set; }

    public string ClientId { get; private set; } = string.Empty;

    public string SecretHash { get; private set; } = string.Empty;

    public StatusCredential Status { get; private set; }

    public DateTime DataCriacao { get; private set; }

    public DateTime? DataRevogacao { get; private set; }

    public void Revogar()
    {
        if (Status == StatusCredential.Revogada)
            return;

        Status = StatusCredential.Revogada;
        DataRevogacao = DateTime.UtcNow;
    }

    public bool EstaAtiva()
    {
        return Status == StatusCredential.Ativa;
    }
}