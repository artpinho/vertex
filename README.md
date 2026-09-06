# Vertex LAN Manager

> Plataforma de gerenciamento de LAN Houses desenvolvida em .NET 9, com API central, cliente instalado nas estações e arquitetura preparada para monitoramento, controle remoto, sessões, planos, promoções, pagamentos e futura evolução para SaaS.

![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?logo=dotnet&logoColor=white)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-9.0-512BD4?logo=dotnet&logoColor=white)
![EF Core](https://img.shields.io/badge/Entity%20Framework%20Core-9.0-512BD4?logo=dotnet&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL%20Server-2022-CC2927?logo=microsoftsqlserver&logoColor=white)
![C%23](https://img.shields.io/badge/C%23-13-239120?logo=csharp&logoColor=white)
![Tests](https://img.shields.io/badge/tests-29%20passing-brightgreen)

---

## Sobre o projeto

O **Vertex LAN Manager** é um sistema de gerenciamento para LAN Houses, desenvolvido com foco em arquitetura limpa, separação de responsabilidades, segurança e comunicação confiável entre o servidor e os computadores das estações.

O projeto está sendo reconstruído do zero após uma primeira versão experimental. A nova implementação prioriza domínio bem definido, persistência organizada, testes automatizados e uma API robusta antes da construção das interfaces administrativas e do cliente instalado nas estações.

---

## Arquitetura

O Vertex utiliza **Clean Architecture / Modular Monolith**.

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

- Domain não conhece EF Core.
- Domain não conhece SQL Server.
- Domain não conhece ASP.NET Core.
- Controllers permanecem finos.
- Application coordena casos de uso.
- Infrastructure implementa persistência e integrações.
- Contracts concentra contratos compartilhados.
- Testes protegem regras e fluxos críticos.
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
| `Vertex.Domain` | Entidades e regras de negócio |
| `Vertex.Application` | Casos de uso, comandos, queries e abstrações |
| `Vertex.Infrastructure` | EF Core, SQL Server, repositories e segurança |
| `Vertex.Api` | API REST, controllers e autenticação |
| `Vertex.Contracts` | Contratos de comunicação |
| `Vertex.Client` | Cliente WPF instalado nas estações |
| `*.Tests` | Testes automatizados |

---

# Tecnologias

| Tecnologia | Versão / Uso |
|---|---|
| C# | 13 |
| .NET | 9.0 |
| ASP.NET Core | 9.0 |
| Entity Framework Core | 9.0.0 |
| SQL Server | Banco principal |
| Swagger / OpenAPI | Swashbuckle / Swagger UI |
| SignalR | Comunicação em tempo real planejada |
| WPF | Cliente Windows planejado |
| xUnit | Testes |
| Visual Studio | Desenvolvimento |
| Git / GitHub | Versionamento |

---

# Estado atual do projeto

### Fundação

- [x] Solution.
- [x] Clean Architecture.
- [x] Domain.
- [x] Application.
- [x] Infrastructure.
- [x] API.
- [x] Contracts.
- [x] Testes automatizados.
- [x] Swagger UI.

### Domínio

- [x] `Entity`
- [x] `AggregateRoot`
- [x] `Cliente`
- [x] `Computador`
- [x] `Estacao`
- [x] `Sessao`
- [x] `ComputadorCredential`
- [x] Estados de computador, estação, sessão e credencial.
- [x] Heartbeat.
- [x] Regras básicas de sessão.

### Persistência

- [x] EF Core 9.0.0.
- [x] SQL Server.
- [x] `VertexDbContext`.
- [x] Configurações por entidade.
- [x] Repositories.
- [x] Migrations.
- [x] Banco `VertexDb`.
- [x] Histórico de credenciais.
- [x] `ClientId` único.
- [x] `HostName` mantido como padrão de nomenclatura.

### API

- [x] Registro de computadores.
- [x] Listagem de computadores.
- [x] Consulta por ID.
- [x] Heartbeat.
- [x] Swagger UI / OpenAPI.
- [x] Provisionamento de credenciais.
- [x] Rotação de credenciais.
- [x] Autenticação por `ClientId + ClientSecret` na camada de Application/Infrastructure.
- [x] Emissão de JWT.
- [x] Proteção dos endpoints com JWT Bearer.

### Testes

Atualmente:

```text
29 testes
29 aprovados
0 falhas
```

A estratégia é manter testes focados em regras de negócio, segurança e fluxos críticos, sem buscar cobertura artificial de código trivial.

---

# Swagger / OpenAPI

O Swagger UI é a ferramenta principal para testar a API durante o desenvolvimento.

```text
https://localhost:<porta>/swagger
```

Os endpoints podem ser executados diretamente pelo **Try it out**.

O Postman continua disponível para cenários específicos, mas o Swagger é o fluxo preferencial para desenvolvimento.

Quando o JWT estiver implementado, o Swagger receberá configuração de **Bearer Authentication** e o botão **Authorize**, permitindo autenticar uma vez e reutilizar o token nas chamadas protegidas.

---

# Identidade dos computadores

Cada estação possuirá uma identidade própria para comunicação com a API.

```text
Computador
    │
    │ 1:N
    ▼
ComputadorCredentials
    ├── Credential antiga → Revogada
    ├── Credential antiga → Revogada
    └── Credential atual  → Ativa
```

Um computador pode possuir várias credenciais históricas, mas somente uma deve permanecer ativa.

## Provisionamento

```http
POST /api/v1/computadores/{id}/credentials
```

Resposta:

```json
{
  "computadorId": "...",
  "clientId": "vtx_...",
  "clientSecret": "..."
}
```

O `clientSecret` é entregue ao administrador no momento do provisionamento e não é armazenado em texto puro.

## Segurança do secret

Os secrets são armazenados utilizando:

- PBKDF2;
- SHA-256;
- salt aleatório;
- 100.000 iterações;
- comparação em tempo constante.

O banco armazena somente o material derivado.

## Rotação

```http
POST /api/v1/computadores/{id}/credentials/rotate
```

A credencial atual é revogada e uma nova é criada.

```text
Credential antiga
      │
      └── Revogada

Nova Credential
      │
      └── Ativa
```

O histórico permanece disponível no banco.

---

# Autenticação

A autenticação dos computadores está implementada com `ClientId + ClientSecret`, PBKDF2 e JWT Bearer.

Fluxo atual:

```text
ClientId + ClientSecret
        │
        ▼
IComputerAuthenticator
        │
        ▼
CredentialRepository
        │
        ▼
PBKDF2
        │
        ▼
ComputadorId
```

Endpoint planejado:

```http
POST /api/v1/auth/computers

# Autorização
Os endpoints protegidos utilizam **JWT Bearer Authentication**.
O Swagger está configurado com o botão **Authorize** para testes autenticados.
```

Próxima evolução:

```text
Vertex.Client
      │
      │ ClientId + ClientSecret
      ▼
POST /api/v1/auth/computers
      │
      ▼
Validação PBKDF2
      │
      ▼
JWT Bearer Token
      │
      ├── Heartbeat
      ├── SignalR
      └── Comandos
```

A intenção é manter o desenvolvimento prático pelo Swagger, sem exigir cópia manual de tokens a cada requisição.

---

# API

Versionamento atual:

```text
/api/v1
```

Endpoints já implementados ou em construção:

```http
POST /api/v1/computadores
GET /api/v1/computadores
GET /api/v1/computadores/{id}
POST /api/v1/computadores/{id}/heartbeat

POST /api/v1/computadores/{id}/credentials
POST /api/v1/computadores/{id}/credentials/rotate

POST /api/v1/auth/computers

# Autorização
Os endpoints protegidos utilizam **JWT Bearer Authentication**.
O Swagger está configurado com o botão **Authorize** para testes autenticados.

# Tarifação

```http
POST /api/v1/faixas-horario-tarifacao
GET /api/v1/faixas-horario-tarifacao?configuracaoTarifacaoId={id}
GET /api/v1/faixas-horario-tarifacao/{id}
PUT /api/v1/faixas-horario-tarifacao/{id}
POST /api/v1/faixas-horario-tarifacao/{id}/status
```

As faixas possuem validação de sobreposição de horários e podem ser
ativadas ou desativadas sem alterar o histórico da configuração.
```

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

## Tipo de máquina

```text
TipoMaquina
├── Id
├── Nome
├── Descricao
├── Ativo
└── DataCadastro
```

## Configuração de tarifação

```text
ConfiguracaoTarifacao
├── Id
├── Nome
├── Descricao
├── TipoMaquinaId
├── ValorHora
├── DataInicio
├── DataFim
├── Prioridade
├── Ativo
└── DataCadastro
```

## Faixa de horário da tarifação

```text
FaixaHorarioTarifacao
├── Id
├── ConfiguracaoTarifacaoId
├── DiaSemana
├── HoraInicio
├── HoraFim
├── ValorHora
├── Ativo
└── DataCadastro
```

As faixas permitem definir valores específicos por dia e período,
com proteção contra sobreposição de horários dentro da mesma configuração.

---

# Funcionalidades planejadas

## Clientes

- Cadastro.
- Atualização.
- Ativação/desativação.
- Histórico.
- Créditos.
- Histórico de consumo.

## Estações

- CRUD.
- Numeração.
- Status.
- Associação computador/estação.
- Bloqueio.
- Manutenção.
- Disponibilidade.

## Tipos de máquina

- CRUD.
- Ativação/desativação.
- Configuração tarifária por tipo de máquina.

## Sessões

- Iniciar.
- Encerrar.
- Cancelar.
- Calcular duração.
- Validação de cliente e estação.
- Controle de estação em uso.
- Histórico.

## Planos e tarifas

- Hora avulsa.
- Pacotes de horas.
- Créditos.
- Planos recorrentes.
- Tarifas diferenciadas.
- Configuração por tipo de máquina.
- Período de vigência.
- Prioridade.
- Horários promocionais.

## Promoções

- Desconto percentual.
- Desconto fixo.
- Horário promocional.
- Dias da semana.
- Pacotes.
- Cupons.

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

---

# Controle remoto

Planejamento:

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

REST será utilizado para operações tradicionais e persistência. SignalR será utilizado para comunicação em tempo real.

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
- [x] Swagger UI.

## Fase 2 — Identidade e computadores

- [x] Registro de computador.
- [x] Consulta de computadores.
- [x] Heartbeat.
- [x] Estado Online/Offline.
- [x] Credencial do computador.
- [x] Provisionamento seguro.
- [x] Rotação.
- [x] Histórico de credenciais.
- [x] PBKDF2.
- [ ] Endpoint de autenticação completo.
- [ ] JWT.

## Fase 3 — Estações

- [x] CRUD.
- [x] Associação computador/estação.
- [x] Disponibilidade.
- [x] Bloqueio.
- [x] Manutenção.

## Fase 4 — Clientes e sessões

- [x] CRUD de clientes.
- [x] Iniciar sessão.
- [x] Encerrar sessão.
- [x] Cancelar sessão.
- [x] Cálculo de duração.
- [x] Histórico.

## Fase 5 — Operação comercial

- [x] Tipos de máquina.
- [x] Configurações de tarifação.
- [x] Ativação/desativação de tarifas.
- [x] Faixas de horário.
- [x] Validação de sobreposição de faixas.
- [x] Ativação/desativação de faixas.
- [ ] Planos.
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

1. **Domínio primeiro** — regras de negócio ficam no Domain.
2. **Controllers finos** — Controllers não concentram lógica de negócio.
3. **Infrastructure não dita o domínio** — EF Core e SQL Server são detalhes.
4. **Testes com foco em valor** — testar regras críticas, segurança e fluxos importantes.
5. **Sem abstrações desnecessárias** — novas bibliotecas somente quando agregarem valor real.
6. **Segurança desde o início** — identidade das estações e comunicação serão tratadas como recursos protegidos.
7. **Evolução incremental** — construir, validar e somente então avançar.

---

# Banco de dados

Banco atual:

```text
VertexDb
```

Tabelas principais:

```text
Clientes
Computadores
ComputadorCredentials
Estacoes
Sessoes
TiposMaquina
ConfiguracoesTarifacao
FaixasHorarioTarifacao
__EFMigrationsHistory
```

Criar migration:

```powershell
dotnet ef migrations add NomeDaMigration `
    --project .\src\Vertex.Infrastructure\Vertex.Infrastructure.csproj `
    --startup-project .\src\Vertex.Api\Vertex.Api.csproj `
    --output-dir Persistence\Migrations
```

Aplicar:

```powershell
dotnet ef database update `
    --project .\src\Vertex.Infrastructure\Vertex.Infrastructure.csproj `
    --startup-project .\src\Vertex.Api\Vertex.Api.csproj
```

---

# Executando o projeto

## Pré-requisitos

- .NET 9 SDK.
- Visual Studio 2022 ou VS Code.
- SQL Server.
- Git.
- Entity Framework Core CLI 9.0.0.

Verifique:

```powershell
dotnet --version
dotnet ef --version
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

## Executar API

```powershell
dotnet run --project src\Vertex.Api
```

Swagger:

```text
https://localhost:<porta>/swagger
```

---

# Fluxo de desenvolvimento

```text
1. Definir regra
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

---

# Progresso atual

Aproximadamente **55%** da fundação e dos módulos centrais planejados já estão implementados.

```text
Arquitetura / infraestrutura       ██████████████████░░  90%
Banco / EF / Migrations             ███████████████████  95%
JWT / Segurança                     ███████████████████  95%
Computadores / Heartbeat            ███████████████████ 100%
Estações                            ███████████████████ 100%
Clientes                            ███████████████████ 100%
Sessões                             ███████████████████ 100%
Tipos de máquina                    ███████████████████ 100%
Configuração de tarifação           ███████████████████ 100%
Faixas de horário                   ███████████████████ 100%
Créditos / Pagamentos               ░░░░░░░░░░░░░░░░░░░  0%
Produtos / Vendas                   ░░░░░░░░░░░░░░░░░░░  0%
WPF                                 █░░░░░░░░░░░░░░░░░░  5%
SignalR                             ░░░░░░░░░░░░░░░░░░░  0%
Dashboard                           █░░░░░░░░░░░░░░░░░░  5%
```

## Testes

```text
29 testes
29 aprovados
0 falhas
```

---

# Status

🚧 **Em desenvolvimento ativo**

A fundação da API, persistência, Swagger, JWT Bearer, gerenciamento de computadores, heartbeat, estações, clientes, sessões, tipos de máquina, configuração de tarifação e faixas de horário já estão implementados.

As faixas de horário possuem CRUD administrativo, ativação/desativação e proteção contra sobreposição de períodos.

**Próxima grande etapa:** implementar **promoções e descontos**, preparando o conjunto completo de regras para o futuro motor de cálculo de sessões, créditos e pagamentos.

---

# Licença

Projeto em desenvolvimento.

A licença definitiva ainda será definida.

---

## Autor

**Artenir Pinho**

Projeto de estudo e construção de uma solução real para gerenciamento de LAN Houses, com foco em arquitetura de software, desenvolvimento .NET, APIs, persistência, segurança, testes automatizados e comunicação entre aplicações.
