# Vertex LAN Manager

> Plataforma de gerenciamento de LAN Houses desenvolvida em .NET 9, com API central, gerenciamento de computadores e estações, controle de sessões, tarifação, créditos, vendas e preparada para comunicação em tempo real e futura evolução para SaaS.

![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?logo=dotnet&logoColor=white)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-9.0-512BD4?logo=dotnet&logoColor=white)
![EF Core](https://img.shields.io/badge/Entity%20Framework%20Core-9.0-512BD4?logo=dotnet&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL%20Server-2022-CC2927?logo=microsoftsqlserver&logoColor=white)
![C%23](https://img.shields.io/badge/C%23-13-239120?logo=csharp&logoColor=white)
![Tests](https://img.shields.io/badge/tests-29%20passing-brightgreen)

**Status geral: 🚧 Em desenvolvimento — aproximadamente 50% concluído.**

## Sobre o projeto

O **Vertex LAN Manager** é um sistema para gerenciamento de operações de LAN Houses. A nova versão está sendo construída com foco em arquitetura limpa, separação de responsabilidades, regras de negócio centralizadas, segurança, persistência confiável e evolução incremental.

A estratégia é concluir a API e as regras centrais antes de avançar para a interface WPF, SignalR e dashboard.

## Arquitetura

O projeto utiliza **Clean Architecture + Modular Monolith**. Não há microservices neste momento.

```text
Vertex.Client (WPF)
        |
    HTTP / SignalR
        |
        v
   Vertex.Api
        |
        v
Vertex.Application
        |
        v
  Vertex.Domain
        ^
        |
Vertex.Infrastructure
        |
    SQL Server

Vertex.Contracts
    Contratos de comunicação
```

### Estrutura da Solution

```text
Vertex/
│
├── Vertex.sln
│
├── src/
│   ├── Vertex.Api/
│   ├── Vertex.Application/
│   ├── Vertex.Domain/
│   ├── Vertex.Infrastructure/
│   ├── Vertex.Contracts/
│   └── Vertex.Client/
│
└── tests/
    ├── Vertex.Domain.Tests/
    ├── Vertex.Application.Tests/
    └── Vertex.Api.Tests/
```

| Projeto | Responsabilidade |
|---|---|
| `Vertex.Domain` | Entidades e regras de negócio |
| `Vertex.Application` | Casos de uso, Commands, Queries e abstrações |
| `Vertex.Infrastructure` | EF Core, SQL Server, repositories e segurança |
| `Vertex.Api` | API REST, Controllers e autenticação |
| `Vertex.Contracts` | Contratos de comunicação |
| `Vertex.Client` | Cliente WPF das estações |
| `*.Tests` | Testes automatizados |

### Princípios

- Domain não conhece EF Core, SQL Server ou ASP.NET Core.
- Controllers permanecem finos.
- Application coordena os casos de uso.
- Infrastructure implementa persistência e integrações.
- Regras de negócio ficam no domínio.
- Evitar abstrações e bibliotecas desnecessárias.
- Construir e validar cada funcionalidade antes de avançar.

## Tecnologias

| Tecnologia | Uso |
|---|---|
| C# 13 | Linguagem principal |
| .NET 9 | Plataforma |
| ASP.NET Core 9 | API REST |
| Entity Framework Core 9.0.0 | ORM / persistência |
| SQL Server | Banco de dados |
| Swagger / OpenAPI | Documentação e testes da API |
| JWT Bearer | Autenticação da API |
| PBKDF2 + SHA-256 | Proteção de credenciais |
| WPF | Cliente Windows |
| SignalR | Comunicação em tempo real |
| xUnit | Testes |
| Visual Studio 2022 | Desenvolvimento |
| Git / GitHub | Versionamento |

# Funcionalidades implementadas

## Computadores

- Cadastro, listagem, consulta e atualização
- Alteração de status
- Heartbeat
- Identidade própria por computador
- Provisionamento e rotação de credenciais
- Histórico de credenciais

Os secrets das estações não são armazenados em texto puro.

## Estações

- Cadastro, listagem e consulta
- Associação com computador
- Controle de status
- Ativação/desativação
- Manutenção
- Validação de disponibilidade

## Clientes

- Cadastro, listagem, consulta e atualização
- Ativação/desativação
- Preservação do histórico

Clientes não são excluídos fisicamente quando já possuem histórico.

## Sessões

- Início, encerramento e cancelamento
- Controle de estação ocupada/livre
- Controle de cliente com sessão ativa
- Cálculo de duração
- Histórico e consultas

## Tipos de máquina

Os tipos de máquina são **dados configuráveis no banco**, e não enums.

Exemplos: `Padrão`, `VIP`, `Premium`.

CRUD implementado:

- Criar
- Listar
- Consultar
- Atualizar
- Ativar/desativar

# Regras comerciais definidas

## Tarifação

A tarifação será baseada em configuração dinâmica, permitindo:

- tipo de máquina;
- faixas de horário;
- dias da semana;
- períodos de vigência;
- tarifas diferentes por horário;
- promoções;
- descontos fixos ou percentuais;
- prioridade entre promoções;
- promoções aplicáveis a todos ou determinados tipos de máquina.

Uma sessão poderá atravessar diferentes faixas tarifárias e será calculada proporcionalmente ao tempo real.

## Crédito

Modelo híbrido:

```text
Cliente
   |
   +-- Crédito pré-pago
   |
   +-- Sessão pós-paga autorizada
```

Regras principais:

- Qualquer saldo positivo pode iniciar uma sessão.
- O consumo é proporcional ao tempo real.
- Não há arredondamento para blocos de hora.
- Ao zerar o crédito, a sessão pré-paga é encerrada automaticamente.
- Pós-pago somente pode ser autorizado por operador/gerente.
- O saldo pode ser usado para sessões e produtos.
- Comprar crédito gera entrada de caixa.
- Consumir crédito posteriormente não gera nova entrada de caixa.

## Pagamentos

Suporte previsto para:

- Dinheiro
- PIX
- Cartão de débito
- Cartão de crédito
- Crédito do cliente

## Produtos e vendas

O MVP contemplará cadastro de produtos, preço de venda, estoque, estoque mínimo, ativação/desativação, vendas, itens da venda e histórico do preço praticado.

# Comunicação em tempo real

O **SignalR** será utilizado posteriormente para comunicação entre a API e os clientes instalados nas estações.

```text
Administrador
      |
      v
 Vertex.Api
      |
   SignalR
      |
      v
Vertex.Client
      |
      +-- Bloquear
      +-- Desbloquear
      +-- Encerrar sessão
      +-- Exibir mensagem
      +-- Outros comandos
```

A API e o banco continuam sendo a autoridade sobre estado e dados financeiros.

A interface poderá atualizar-se localmente com maior frequência, enquanto a sincronização persistida do consumo ocorrerá aproximadamente a cada **1 minuto**. O fechamento da sessão utilizará o tempo real efetivamente transcorrido.

# Fluxo de desenvolvimento

```text
1. Definir regra de negócio
        |
2. Criar/alterar Domain
        |
3. Criar testes quando necessário
        |
4. Criar caso de uso
        |
5. Criar abstrações
        |
6. Implementar Infrastructure
        |
7. Criar migration
        |
8. Expor API
        |
9. Validar no Swagger
        |
10. Integrar Client/UI
```

Cada módulo é construído e validado de ponta a ponta antes do próximo.

# Estado atual

### Concluído

- [x] Arquitetura e Solution
- [x] Domain / Application / Infrastructure / API / Contracts
- [x] EF Core / SQL Server / Migrations
- [x] Swagger / OpenAPI
- [x] JWT Bearer
- [x] Computadores
- [x] Credenciais dos computadores
- [x] Heartbeat
- [x] Estações
- [x] Clientes
- [x] Sessões
- [x] Tipos de máquina

### Próximas etapas

- [ ] Configuração de tarifação
- [ ] Faixas de tarifação
- [ ] Promoções
- [ ] Cálculo financeiro das sessões
- [ ] Crédito / carteira
- [ ] Pós-pago
- [ ] Pagamentos
- [ ] Produtos
- [ ] Vendas
- [ ] Estoque
- [ ] Caixa
- [ ] Cliente WPF
- [ ] SignalR
- [ ] Monitoramento
- [ ] Dashboard
- [ ] Auditoria
- [ ] Relatórios
- [ ] Multiunidade / SaaS

# Progresso aproximado

| Área | Progresso |
|---|---:|
| Arquitetura | 90% |
| Banco / EF Core / Migrations | 95% |
| Autenticação / Segurança | 95% |
| Computadores | 100% |
| Heartbeat | 100% |
| Estações | 100% |
| Clientes | 100% |
| Sessões | 100% |
| Tipos de máquina | 100% |
| Tarifação | 0% |
| Crédito / Carteira | 0% |
| Pagamentos | 0% |
| Produtos / Vendas / Estoque | 0% |
| WPF | ~5% |
| SignalR | 0% |
| Dashboard | ~5% |
| Testes | ~70% |

### Progresso geral estimado: **~50%**

> A porcentagem representa o avanço estimado do MVP como um todo, considerando funcionalidades implementadas, regras de negócio definidas e etapas ainda pendentes.

# Banco de dados

Banco principal: `VertexDb`

Principais tabelas:

```text
Clientes
Computadores
ComputadorCredentials
Estacoes
Sessoes
TiposMaquina
__EFMigrationsHistory
```

O schema é versionado através das migrations do Entity Framework Core.

### Criar migration

```powershell
dotnet ef migrations add NomeDaMigration `
    --project .\src\Vertex.Infrastructure\Vertex.Infrastructure.csproj `
    --startup-project .\src\Vertex.Api\Vertex.Api.csproj `
    --output-dir Persistence\Migrations
```

### Aplicar migration

```powershell
dotnet ef database update `
    --project .\src\Vertex.Infrastructure\Vertex.Infrastructure.csproj `
    --startup-project .\src\Vertex.Api\Vertex.Api.csproj
```

# Executando o projeto

## Pré-requisitos

- .NET 9 SDK
- Visual Studio 2022 ou VS Code
- SQL Server
- Git
- Entity Framework Core CLI 9.0.0

## Build

```powershell
dotnet restore
dotnet build
```

## Testes

```powershell
dotnet test
```

## Executar API

```powershell
dotnet run --project src\Vertex.Api
```

## Swagger

```text
https://localhost:<porta>/swagger
```

# Roadmap

```text
FASE 1 — Fundação
████████████████████ 100%

FASE 2 — Computadores e identidade
████████████████████ 100%

FASE 3 — Estações
████████████████████ 100%

FASE 4 — Clientes e sessões
████████████████████ 100%

FASE 5 — Tarifação e operação comercial
░░░░░░░░░░░░░░░░░░░░   0%

FASE 6 — Cliente WPF
█░░░░░░░░░░░░░░░░░░░   5%

FASE 7 — SignalR e monitoramento
░░░░░░░░░░░░░░░░░░░░   0%

FASE 8 — Dashboard, auditoria e evolução SaaS
░░░░░░░░░░░░░░░░░░░░   0%
```

# Licença

Projeto em desenvolvimento. A licença definitiva ainda será definida.

## Autor

**Artenir Pinho**
