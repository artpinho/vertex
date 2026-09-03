using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex.Domain.Common;

namespace Vertex.Domain.Entities;

public class Cliente : Entity
{
    public string Nome { get; private set; } = string.Empty;

    public string? CPF { get; private set; }

    public string? Email { get; private set; }

    public string? Telefone { get; private set; }

    public DateTime? DataNascimento { get; private set; }

    public bool Ativo { get; private set; }

    public DateTime DataCadastro { get; private set; }

    protected Cliente()
    {
    }

    public Cliente(
        string nome,
        string? cpf = null,
        string? email = null,
        string? telefone = null,
        DateTime? dataNascimento = null)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("O nome do cliente é obrigatório.");

        Nome = nome.Trim();
        CPF = cpf;
        Email = email;
        Telefone = telefone;
        DataNascimento = dataNascimento;

        Ativo = true;
        DataCadastro = DateTime.UtcNow;
    }

    public void Desativar()
    {
        Ativo = false;
    }

    public void Ativar()
    {
        Ativo = true;
    }

    public void AtualizarDados(
    string nome,
    string? cpf,
    string? email,
    string? telefone,
    DateTime? dataNascimento)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException(
                "O nome do cliente é obrigatório.");

        Nome = nome.Trim();
        CPF = cpf;
        Email = email;
        Telefone = telefone;
        DataNascimento = dataNascimento;
    }
}