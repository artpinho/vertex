# Vertex LAN Manager

> Plataforma de gerenciamento de LAN Houses desenvolvida em .NET 9, com API central, cliente instalado nas estações e arquitetura preparada para monitoramento, controle remoto, sessões, planos, promoções, pagamentos e futura evolução para SaaS.

![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?logo=dotnet&logoColor=white)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-9.0-512BD4?logo=dotnet&logoColor=white)
![EF Core](https://img.shields.io/badge/Entity%20Framework%20Core-9.0-512BD4?logo=dotnet&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL%20Server-2022-CC2927?logo=microsoftsqlserver&logoColor=white)
![C%23](https://img.shields.io/badge/C%23-13-239120?logo=csharp&logoColor=white)
![Tests](https://img.shields.io/badge/tests-29%20passing-brightgreen)

---

## Objetivo

O Vertex LAN Manager centraliza o gerenciamento operacional da LAN House, permitindo:

- Cadastro de clientes;
- Cadastro de computadores;
- Cadastro de estações;
- Associação entre estações e computadores;
- Controle do status das estações;
- Abertura e encerramento de sessões;
- Autenticação dos computadores via JWT;
- Futuro controle de cobrança por tempo;
- Futuro monitoramento dos computadores;
- Futuro gerenciamento em tempo real via SignalR;
- Futuro cliente desktop em WPF.

---

## Arquitetura

O projeto utiliza **Clean Architecture / Modular Monolith**:

```text
Vertex
├── Vertex.Domain
│   ├── Entities
│   ├── Enums
│   └── Common
│
├── Vertex.Application
│   ├── Abstractions
│   ├── Clients
│   ├── Stations
│   └── Sessions
│
├── Vertex.Infrastructure
│   ├── Persistence
│   ├── Configurations
│   └── Repositories
│
├── Vertex.Api
│   ├── Controllers
│   ├── Authentication
│   └── Configuration
│
├── Vertex.Contracts
│
└── Vertex.Client
```

### Responsabilidades

**Vertex.Domain**
- Entidades;
- Regras de negócio;
- Enums;
- Comportamentos fundamentais do domínio.

**Vertex.Application**
- Casos de uso;
- Commands;
- Handlers;
- Responses;
- Abstrações de persistência.

**Vertex.Infrastructure**
- Entity Framework Core;
- SQL Server;
- DbContext;
- Configurações;
- Repositories.

**Vertex.Api**
- Controllers;
- Endpoints HTTP;
- JWT;
- Swagger;
- Configuração da aplicação.

**Vertex.Contracts**
- Contratos compartilhados.

**Vertex.Client**
- Futuro cliente WPF.

---

## Tecnologias

- .NET 9
- C# 13
- ASP.NET Core 9
- Entity Framework Core 9
- SQL Server
- JWT Bearer Authentication
- Swagger / OpenAPI
- WPF
- SignalR (planejado)

Pacotes EF Core atualmente alinhados em `9.0.0`.

---

## Banco de Dados

Banco:

```text
VertexDb
```

DbContext:

```text
VertexDbContext
```

Principais entidades persistidas:

```text
Clientes
Computadores
Estacoes
Sessoes
ComputadorCredentials
```

As configurações das entidades são aplicadas por assembly:

```csharp
modelBuilder.ApplyConfigurationsFromAssembly(
    typeof(VertexDbContext).Assembly);
```

---

# Clientes

O módulo de clientes está concluído para o MVP.

A entidade possui:

- Nome;
- CPF;
- Email;
- Telefone;
- Data de nascimento;
- Ativo/Inativo;
- Data de cadastro.

### Endpoints

```http
POST /api/v1/clientes
GET  /api/v1/clientes
GET  /api/v1/clientes/{id}
PUT  /api/v1/clientes/{id}
POST /api/v1/clientes/{id}/status
```

Não existe exclusão física de clientes.

O cliente é desativado através de `Ativo`, preservando o histórico das sessões.

---

# Computadores

O sistema possui cadastro e identificação dos computadores da LAN House.

A identidade do computador é utilizada na autenticação da comunicação com a API.

Principais entidades:

```text
Computador
ComputadorCredential
```

A autenticação utiliza JWT Bearer.

---

# Estações

A entidade `Estacao` representa a posição física disponibilizada ao cliente.

Possui:

- Nome;
- Número;
- Status;
- Ativa/Inativa;
- Computador associado.

### Status

```text
Livre
EmUso
Bloqueada
Manutencao
```

### Endpoints

```http
POST /api/v1/estacoes
GET  /api/v1/estacoes
GET  /api/v1/estacoes/{id}
POST /api/v1/estacoes/{id}/associar-computador
POST /api/v1/estacoes/{id}/status
```

### Operações de status

```text
Liberar
ColocarEmUso
Bloquear
ColocarEmManutencao
Ativar
Desativar
```

Um computador não pode estar associado simultaneamente a duas estações.

---

# Sessões

Uma sessão representa o período em que um cliente utiliza uma estação.

Possui:

- Cliente;
- Estação;
- Início;
- Fim;
- Status.

### Status

```text
Ativa
Encerrada
Cancelada
```

## Iniciar sessão

```http
POST /api/v1/sessoes
```

Valida:

1. Cliente existente;
2. Cliente ativo;
3. Estação existente;
4. Estação ativa;
5. Estação livre;
6. Cliente sem outra sessão ativa;
7. Estação sem outra sessão ativa.

Ao iniciar:

```text
Sessão  -> Ativa
Estação -> EmUso
```

## Encerrar sessão

```http
POST /api/v1/sessoes/{id}/encerrar
```

Ao encerrar:

```text
Sessão  -> Encerrada
Estação -> Livre
```

A duração é calculada com base em `Inicio` e `Fim`.

Enquanto a sessão está ativa, o horário atual é utilizado para calcular a duração.

---

# Fluxo Operacional Validado

O fluxo principal já funciona de ponta a ponta:

```text
Cliente
   ↓
Estação
   ↓
Iniciar sessão
   ↓
Estação = EmUso
   ↓
Sessão = Ativa
   ↓
Utilização
   ↓
Encerrar sessão
   ↓
Sessão = Encerrada
   ↓
Estação = Livre
```

Esse fluxo foi validado através do Swagger.

---

# Autenticação

A API utiliza:

```text
JWT Bearer Authentication
```

O token identifica o computador através do claim:

```text
computadorId
```

A aplicação possui a abstração:

```text
ICurrentComputer
```

para centralizar a identificação do computador autenticado.

A chave simétrica utilizada na validação do JWT possui `KeyId`.

---

# Heartbeat

Existe suporte à comunicação autenticada dos computadores com a API através de heartbeat.

O objetivo é permitir futuramente o acompanhamento de:

- Computador online;
- Computador offline;
- Última comunicação;
- Estado operacional.

---

# Swagger

O Swagger é atualmente a principal ferramenta de testes da API.

Exemplo:

```text
https://localhost:7098/swagger
```

Para endpoints protegidos:

1. Obter o JWT;
2. Clicar em `Authorize`;
3. Informar o token Bearer;
4. Executar o endpoint.

---

# CORS

O ambiente de desenvolvimento possui CORS configurado para permitir o acesso local.

Exemplo:

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("SwaggerPolicy", policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:5250",
                "https://localhost:7098")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
```

Pipeline:

```csharp
app.UseHttpsRedirection();
app.UseCors("SwaggerPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
```

---

# Testes

A estratégia de testes é pragmática, priorizando:

- Regras de negócio importantes;
- Segurança;
- Casos de uso críticos;
- Fluxos que possam gerar inconsistência.

Estado atual:

```text
29 testes
29 aprovados
0 falhas
```

---

# Cobrança

A cobrança ainda não foi implementada.

A proposta para o MVP é utilizar uma tarifa global por hora, com cálculo proporcional ao tempo:

```text
Valor = duração em horas × valor/hora
```

O valor da tarifa ainda será definido.

A intenção é manter inicialmente uma única tarifa global, sem preços diferentes por estação.

---

# Cliente WPF

O projeto `Vertex.Client` será utilizado futuramente como software instalado nos computadores da LAN House.

Responsabilidades previstas:

- Identificar o computador;
- Autenticar na API;
- Enviar heartbeat;
- Receber comandos;
- Exibir o estado da estação;
- Bloquear/desbloquear o computador;
- Informar disponibilidade;
- Participar do controle da sessão.

---

# SignalR

SignalR será implementado posteriormente para comunicação em tempo real entre API e clientes WPF.

Possíveis usos:

```text
API
 ↓
SignalR
 ↓
Cliente WPF
```

Exemplos:

- Bloquear computador;
- Liberar computador;
- Atualizar status;
- Encerrar sessão;
- Atualizar configurações;
- Atualizar dashboard em tempo real.

---

# Dashboard

Ainda planejado.

Possíveis funcionalidades:

- Estações livres;
- Estações em uso;
- Estações bloqueadas;
- Clientes ativos;
- Sessões atuais;
- Faturamento;
- Histórico;
- Computadores online/offline;
- Relatórios.

---

# Roadmap

## Concluído

- [x] Estrutura da solução
- [x] Clean Architecture / Modular Monolith
- [x] Entity Framework Core
- [x] SQL Server
- [x] Migrations
- [x] Banco `VertexDb`
- [x] JWT
- [x] Identificação do computador autenticado
- [x] Cadastro de computadores
- [x] Cadastro de estações
- [x] Associação estação/computador
- [x] Controle de status da estação
- [x] Cadastro de clientes
- [x] Consulta de clientes
- [x] Atualização de clientes
- [x] Ativação/desativação de clientes
- [x] Criação de sessões
- [x] Encerramento de sessões
- [x] Alteração automática do status da estação durante a sessão
- [x] Swagger
- [x] CORS
- [x] Testes críticos

## Próximos passos

- [ ] GET `/api/v1/sessoes`
- [ ] GET `/api/v1/sessoes/{id}`
- [ ] Histórico e consultas de sessões
- [ ] Definição da tarifa por hora
- [ ] Implementação da cobrança
- [ ] Evolução do heartbeat
- [ ] Cliente WPF
- [ ] Controle remoto das estações
- [ ] SignalR
- [ ] Dashboard administrativo
- [ ] Relatórios
- [ ] Melhorias de segurança e operação

---

# Estratégia de Desenvolvimento

O projeto é desenvolvido de forma incremental:

```text
Domínio
   ↓
Persistência
   ↓
Caso de uso
   ↓
API
   ↓
Teste via Swagger
   ↓
Fechar módulo
   ↓
Próximo módulo
```

A prioridade é concluir cada módulo de forma funcional antes de avançar, evitando complexidade prematura.

---

# Estado Atual

O núcleo operacional do backend já representa corretamente:

```text
Computador
    ↓
Estação
    ↓
Cliente
    ↓
Sessão
```

O fluxo de utilização de uma estação já funciona de ponta a ponta.

O backend está significativamente mais avançado que o cliente desktop, dashboard e comunicação em tempo real.

A próxima etapa recomendada é finalizar as **consultas de sessões**, permitindo visualizar sessões ativas e encerradas antes de iniciar o módulo de cobrança.

---

# Status

**Em desenvolvimento — MVP Backend em evolução**