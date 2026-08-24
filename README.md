# Vertex LAN Manager

> Plataforma de gerenciamento de LAN Houses desenvolvida em .NET 9, com API central, cliente instalado nas estações e arquitetura preparada para monitoramento, controle remoto, sessões, planos, promoções, pagamentos e futura evolução para SaaS.

![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?logo=dotnet&logoColor=white)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-9.0-512BD4?logo=dotnet&logoColor=white)
![EF Core](https://img.shields.io/badge/Entity%20Framework%20Core-9.0-512BD4?logo=dotnet&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL%20Server-2022-CC2927?logo=microsoftsqlserver&logoColor=white)
![C#](https://img.shields.io/badge/C%23-13-239120?logo=csharp&logoColor=white)
![Tests](https://img.shields.io/badge/tests-29%20passing-brightgreen)

---

## Sobre o projeto

O **Vertex LAN Manager** é um sistema de gerenciamento para LAN Houses, desenvolvido com foco em arquitetura limpa, separação de responsabilidades, segurança e comunicação confiável entre o servidor e os computadores das estações.

O projeto está sendo reconstruído do zero após uma primeira versão experimental. A nova implementação prioriza um domínio bem definido, testes automatizados e uma API robusta antes da construção das interfaces administrativas e do cliente instalado nas estações.

### Objetivos

- Gerenciar clientes, estações e computadores.
- Controlar sessões de utilização.
- Monitorar computadores em tempo real.
- Identificar o estado online/offline das estações.
- Permitir bloqueio e liberação remota.
- Gerenciar planos, tarifas, créditos e promoções.
- Controlar pagamentos e caixa.
- Gerenciar produtos e consumo.
- Implementar agendamentos e recursos de fidelidade.
- Centralizar a comunicação entre servidor e clientes através de uma API.
- Preparar a arquitetura para múltiplas unidades e futura evolução para SaaS.

---

## Arquitetura

O Vertex utiliza uma abordagem de **Clean Architecture / Modular Monolith**, mantendo o domínio independente de frameworks e infraestrutura.

```text
                    +---------------------+
                    |    Vertex.Client    |
                    |    WPF / Windows    |
                    +----------+----------+
                               |
                         HTTP / SignalR
                               |
                               v
                    +---------------------+
                    |     Vertex.Api      |
                    |    ASP.NET Core     |
                    +----------+----------+
                               |
                               v
                    +---------------------+
                    |  Vertex.Application |
                    |    Use Cases / App  |
                    +----------+----------+
                               |
                               v
                    +---------------------+
                    |    Vertex.Domain    |
                    |   Business Rules    |
                    +---------------------+
                               ^
                               |
                    +----------+----------+
                    | Vertex.Infrastructure|
                    | EF Core / SQL Server |
                    +---------------------+

                    Vertex.Contracts
                    API communication DTOs
```

### Princípios

- O **Domain não conhece EF Core**.
- O **Domain não conhece SQL Server**.
- O **Domain não conhece ASP.NET Core**.
- Controllers não possuem regras de negócio.
- A Application coordena os casos de uso.
- Infrastructure implementa persistência e integrações.
- Contracts define contratos de comunicação.
- Regras de negócio são testadas independentemente da infraestrutura.
- Dependências apontam para dentro da arquitetura.

---

## Estrutura da Solution

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
| `Vertex.Domain` | Entidades, regras de negócio, enums, value objects e eventos |
| `Vertex.Application` | Casos de uso, comandos, queries, DTOs e abstrações |
| `Vertex.Infrastructure` | EF Core, SQL Server, repositories e integrações |
| `Vertex.Api` | HTTP, controllers, autenticação e exposição da API |
| `Vertex.Contracts` | Contratos compartilhados entre API e clientes |
| `Vertex.Client` | Aplicação WPF instalada nas estações |
| `*.Tests` | Testes automatizados |

---

## Tecnologias

### Backend

| Tecnologia | Versão | Utilização |
|---|---:|---|
| C# | 13 | Linguagem principal |
| .NET | 9.0 | Plataforma |
| ASP.NET Core | 9.0 | API REST |
| Entity Framework Core | 9.0.0 | ORM |
| SQL Server | 2022 | Banco de dados |
| SignalR | .NET 9 | Comunicação em tempo real |
| Swagger / OpenAPI | ASP.NET Core | Documentação e testes da API |

### Desktop

| Tecnologia | Versão | Utilização |
|---|---:|---|
| WPF | .NET 9 | Cliente das estações |
| Windows | — | Sistema operacional alvo |

### Testes

- xUnit
- Fake Repositories
- Testes de Domain
- Testes de Application
- Testes de API

### Ferramentas

- Visual Studio 2022
- Visual Studio Code
- Git
- GitHub
- Docker
- SQL Server
- EF Core CLI
- Swagger / OpenAPI

---

# Estado atual

### Fundação

- [x] Solution criada.
- [x] Clean Architecture inicial.
- [x] Projetos separados.
- [x] Referências entre projetos definidas.
- [x] Domain independente de infraestrutura.
- [x] Testes automatizados configurados.

### Domain

- [x] `Entity`
- [x] `AggregateRoot`
- [x] `Cliente`
- [x] `Computador`
- [x] `Estacao`
- [x] `Sessao`
- [x] Status de computador.
- [x] Status de estação.
- [x] Status de sessão.
- [x] Regras básicas de sessão.
- [x] Heartbeat no domínio.

### Persistência

- [x] EF Core 9.0.0
- [x] SQL Server
- [x] `VertexDbContext`
- [x] Configurações independentes por entidade.
- [x] Repositories.
- [x] Migration inicial.
- [x] Banco `VertexDb`.
- [x] Índice único para `Estacao.Numero`.
- [x] Índices únicos para `Computador.HostName` e `MacAddress`.

### API

- [x] ASP.NET Core Web API.
- [x] Dependency Injection.
- [x] Registro de computador.
- [x] Consulta de computadores.
- [x] Consulta de computador por ID.
- [x] Heartbeat.
- [x] Estado Online/Offline.
- [x] Swagger/OpenAPI.

### Testes

- [x] Testes de Domain.
- [x] Testes de Application.
- [x] Fake Repository.
- [x] Validação de registro de computador.
- [x] Validação de HostName duplicado.
- [x] Validação de MAC duplicado.
- [x] Testes de heartbeat.
- [x] **24 testes passando atualmente.**

---

# Modelo de domínio

## Cliente

```text
Cliente
├── Id
├── Nome
├── CPF
├── Email
├── Telefone
├── DataNascimento
├── Ativo
└── DataCadastro
```

## Computador

Representa o equipamento físico onde o `Vertex.Client` será instalado.

```text
Computador
├── Id
├── HostName
├── Ip
├── MacAddress
├── SistemaOperacional
├── ClienteVersao
├── UltimoHeartbeat
└── Status
```

> O padrão de nomenclatura adotado no projeto é `HostName`.

## Estação

```text
Estacao
├── Id
├── Nome
├── Numero
├── Status
├── Ativa
└── ComputadorId
```

## Sessão

```text
Sessao
├── Id
├── ClienteId
├── EstacaoId
├── Inicio
├── Fim
└── Status
```

A duração da sessão é calculada no domínio e não é persistida como uma coluna.

### Relacionamentos

```text
Cliente
   │
   │ 1:N
   v
Sessao
   │
   │ N:1
   v
Estacao
   │
   │ 1:1
   v
Computador
```

---

# Comunicação com os computadores

O `Vertex.Client` será instalado em cada computador da LAN House e será responsável por:

- Identificar o computador.
- Autenticar-se na API.
- Enviar heartbeat.
- Informar versão do cliente.
- Informar estado da máquina.
- Receber comandos.
- Aplicar bloqueios.
- Controlar a sessão local.
- Comunicar eventos ao servidor.

### Heartbeat

```text
Vertex.Client
     |
     | POST /api/v1/computadores/{id}/heartbeat
     v
Vertex.Api
     |
     v
Application
     |
     v
Computador
     |
     +-- UltimoHeartbeat = agora
     +-- Status = Online
```

O objetivo é evitar que o cliente simplesmente declare que está online. A API passa a determinar o estado com base na última comunicação válida.

---

# API atual

A API utiliza versionamento:

```text
/api/v1
```

### Registrar computador

```http
POST /api/v1/computadores
```

```json
{
  "hostName": "PC-001",
  "ip": "192.168.0.101",
  "macAddress": "00:11:22:33:44:55",
  "sistemaOperacional": "Windows 11",
  "clienteVersao": "1.0.0"
}
```

### Listar computadores

```http
GET /api/v1/computadores
```

### Obter computador

```http
GET /api/v1/computadores/{id}
```

### Heartbeat

```http
POST /api/v1/computadores/{id}/heartbeat
```

---

# Segurança

A segurança da comunicação entre o cliente e a API será implementada progressivamente.

A arquitetura planejada possui uma credencial própria para cada instalação:

```text
Computador
    |
    +-- Credential
          +-- ClientId
          +-- SecretHash
```

Fluxo planejado:

```text
ClientId + Secret
       |
       v
POST /api/v1/auth/computers
       |
       v
Access Token
       |
       v
Heartbeat / SignalR / Commands
```

O segredo permanente não será armazenado em texto puro.

Também estão planejados:

- Revogação de credenciais.
- Regeneração de credenciais.
- Controle de tokens.
- Auditoria de autenticações.
- Autorização de comandos.

---

# Funcionalidades planejadas

## Clientes

- Cadastro.
- Atualização.
- Ativação/desativação.
- Histórico.
- Identificação.
- Saldo/créditos.
- Histórico de consumo.

## Estações

- Cadastro.
- Numeração.
- Status.
- Associação com computador.
- Bloqueio.
- Manutenção.
- Disponibilidade.

## Sessões

- Iniciar sessão.
- Encerrar sessão.
- Cancelar sessão.
- Calcular duração.
- Calcular valor.
- Histórico.
- Controle remoto.

## Planos e tarifas

```text
Plano
├── Nome
├── Descrição
├── Valor
├── Duração
├── Tipo de cobrança
└── Ativo
```

Possibilidades:

- Hora avulsa.
- Pacotes de horas.
- Créditos.
- Planos recorrentes.
- Tarifas diferenciadas.
- Horários promocionais.

## Promoções

- Desconto percentual.
- Desconto fixo.
- Horário promocional.
- Dias da semana.
- Pacotes.
- Cupons.
- Campanhas.

## Pagamentos e caixa

- Dinheiro.
- PIX.
- Cartão.
- Créditos.
- Histórico financeiro.
- Caixa.
- Fechamento.
- Sangria.
- Auditoria.

## Produtos e vendas

```text
Produto
├── Nome
├── Código
├── Preço
├── Estoque
├── Ativo
└── Categoria
```

Futuramente:

```text
Venda
├── Cliente
├── Itens
├── Pagamento
└── Total
```

## Monitoramento

- CPU.
- Memória.
- Disco.
- Temperatura.
- Rede.
- Processos.
- Estado do cliente.
- Versão instalada.
- Último heartbeat.

O histórico de monitoramento será separado do cadastro principal do computador para evitar transformar `Computador` em uma tabela de telemetria.

---

# Controle remoto

Uma das funcionalidades centrais do Vertex será permitir que o administrador controle as estações.

```text
Administrador
     |
     v
Vertex.Api
     |
     v
SignalR
     |
     v
Vertex.Client
     |
     +-- Bloquear
     +-- Desbloquear
     +-- Encerrar sessão
     +-- Exibir mensagem
     +-- Atualizar cliente
     +-- Outros comandos
```

O SignalR será utilizado para comunicação em tempo real, enquanto a API REST continuará sendo utilizada para operações tradicionais e persistência.

---

# Módulos planejados

```text
Vertex
|
+-- Identity
+-- Customers
+-- Computers
+-- Stations
+-- Sessions
+-- Plans
+-- Pricing
+-- Promotions
+-- Payments
+-- Cashier
+-- Products
+-- Sales
+-- Scheduling
+-- Monitoring
+-- Notifications
+-- Auditing
+-- Reports
```

Cada módulo será implementado gradualmente.

---

# Roadmap

## Fase 1 — Fundação

- [x] Solution.
- [x] Clean Architecture.
- [x] Domain.
- [x] EF Core.
- [x] SQL Server.
- [x] Migrations.
- [x] Testes.

## Fase 2 — Infraestrutura de computadores

- [x] Registro de computador.
- [x] Consulta de computadores.
- [x] Heartbeat.
- [x] Estado Online/Offline.
- [ ] Credencial do computador.
- [ ] Autenticação do cliente.
- [ ] Token.
- [ ] Provisionamento seguro.

## Fase 3 — Estações

- [ ] CRUD de estações.
- [ ] Associação computador/estação.
- [ ] Disponibilidade.
- [ ] Bloqueio.
- [ ] Manutenção.

## Fase 4 — Clientes e sessões

- [ ] CRUD de clientes.
- [ ] Iniciar sessão.
- [ ] Encerrar sessão.
- [ ] Cálculo de duração.
- [ ] Cálculo de preço.
- [ ] Histórico.

## Fase 5 — Operação comercial

- [ ] Planos.
- [ ] Tarifas.
- [ ] Créditos.
- [ ] Promoções.
- [ ] Pagamentos.
- [ ] Caixa.
- [ ] Produtos.
- [ ] Vendas.

## Fase 6 — Controle remoto

- [ ] SignalR.
- [ ] Bloqueio remoto.
- [ ] Desbloqueio.
- [ ] Mensagens.
- [ ] Encerramento remoto.
- [ ] Atualização do cliente.

## Fase 7 — Monitoramento

- [ ] Telemetria.
- [ ] Histórico.
- [ ] Alertas.
- [ ] Dashboard.
- [ ] Detecção automática de offline.

## Fase 8 — Evolução

- [ ] Auditoria.
- [ ] Relatórios.
- [ ] Multiunidade.
- [ ] SaaS.
- [ ] Observabilidade.
- [ ] Escalabilidade.

---

# Princípios de desenvolvimento

### 1. Domínio primeiro

As regras de negócio devem existir no Domain sempre que forem regras próprias do negócio.

### 2. Controllers finos

Controllers recebem requisições, chamam casos de uso e retornam respostas.

### 3. Infrastructure não dita o domínio

EF Core, SQL Server e outros frameworks são detalhes de infraestrutura.

### 4. Testes antes de complexidade

Novas regras devem possuir testes automatizados.

### 5. Sem abstrações desnecessárias

Bibliotecas como MediatR, AutoMapper e outras ferramentas somente serão adicionadas quando houver necessidade real.

### 6. Segurança desde o início

A comunicação com computadores não será tratada como uma API pública comum.

### 7. Evolução incremental

Cada funcionalidade deve ser construída de ponta a ponta e validada antes de iniciar a próxima.

---

# Banco de dados

Banco atual:

```text
VertexDb
```

Tabelas iniciais:

```text
Clientes
Computadores
Estacoes
Sessoes
__EFMigrationsHistory
```

O projeto utiliza migrations do Entity Framework Core para versionamento do schema.

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

---

# Executando o projeto

## Pré-requisitos

- .NET 9 SDK
- Visual Studio 2022 ou VS Code
- SQL Server
- Git
- Entity Framework Core CLI 9.0.0

Verifique:

```powershell
dotnet --version
dotnet ef --version
```

O projeto atualmente utiliza:

```text
Entity Framework Core .NET Command-line Tools
9.0.0
```

## Build

```powershell
dotnet restore
dotnet build
```

## Testes

```powershell
dotnet test
```

## Executar a API

```powershell
dotnet run --project src\Vertex.Api
```

A API disponibiliza a documentação OpenAPI/Swagger conforme a configuração do projeto.

---

# Fluxo de desenvolvimento

Uma funcionalidade típica segue:

```text
1. Definir regra
       |
2. Criar/alterar Domain
       |
3. Criar testes
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
9. Testar integração
       |
10. Integrar Client/UI
```

Essa abordagem reduz o acoplamento e permite que o projeto cresça sem transformar a API em um conjunto de Controllers contendo toda a lógica do sistema.

---

# Status

🚧 **Em desenvolvimento ativo**

O Vertex está atualmente na construção da infraestrutura central da API e comunicação com os computadores.

A próxima grande etapa é implementar a **identidade e autenticação do `Vertex.Client`**, preparando o sistema para comunicação segura, heartbeat autenticado e posteriormente comunicação em tempo real através de SignalR.

---

# Licença

Projeto em desenvolvimento.

A licença definitiva ainda será definida.

---

## Autor

**Artenir Pinho**

Projeto de estudo e construção de uma solução real para gerenciamento de LAN Houses, com foco em arquitetura de software, desenvolvimento .NET, APIs, persistência, testes automatizados e comunicação entre aplicações.
