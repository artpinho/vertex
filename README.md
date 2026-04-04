# 🚀 Vertex — LAN House Management System

## 📌 Sobre o Projeto

O **Vertex** é um sistema completo de gerenciamento para LAN houses e cyber cafés, com foco em:

* Controle de sessões de uso
* Gestão de clientes
* Controle financeiro (créditos e débitos)
* Monitoramento em tempo real

Desenvolvido com **.NET 9**, seguindo princípios de **Clean Architecture**, com separação clara de responsabilidades e preparado para evolução para SaaS.

---

## 🧱 Arquitetura da Solução

```plaintext
Vertex.sln

├── Vertex.API            → Controllers, Auth, SignalR, Swagger
├── Vertex.Application    → Services, DTOs, Interfaces
├── Vertex.Domain         → Entidades e regras do domínio
├── Vertex.Infrastructure → EF Core, DbContext, persistência
├── Vertex.Admin          → Dashboard Web (em construção)
├── Vertex.Client         → App das estações (futuro)
```

---

## 🧠 Conceitos Aplicados

* Clean Architecture
* Separation of Concerns
* Dependency Injection
* DTO Pattern (Request/Response)
* Entity Framework Core (Code First)
* SQL Server (Docker)
* JWT Authentication (Bearer Token)
* Password Hashing (BCrypt)
* SignalR (tempo real)
* Nullable Reference Types
* Async/Await

---

## 🗄️ Modelagem Atual

### 👤 Customer

* Id
* Name
* Username
* PasswordHash 🔐
* Email
* PhoneNumber
* Balance 💰
* CreatedAt
* Address (Value Object)
* Photo

---

### 💻 Station

* Id
* Name
* Status (Available, InUse, Blocked)

---

### ⏱️ Session

* Id
* CustomerId
* StationId
* StartTime
* EndTime
* DurationMinutes
* AmountCharged
* IsActive (computed)
* Navigation: Station

---

## 🔐 Autenticação e Segurança

* Login com **username + senha**
* Senhas protegidas com **BCrypt**
* Autenticação via **JWT**
* Uso de Claims:

  * Username
  * CustomerId
* Endpoints protegidos com `[Authorize]`
* Swagger com suporte a autenticação Bearer

---

## 💰 Controle Financeiro

* ✔ Validação de saldo antes de iniciar sessão
* ✔ Débito automático ao encerrar sessão
* ✔ Bloqueio de sessão sem saldo
* ✔ Adição de crédito via API (`add-balance`)

---

## ⚡ Tempo Real (SignalR)

* ✔ Hub de comunicação (`DashboardHub`)
* ✔ Eventos disparados:

  * `SessionStarted`
  * `SessionEnded`
* ✔ Abstração via `IRealtimeService`
* ✔ Implementação desacoplada (`SignalRRealtimeService`)
* ✔ Base para dashboard em tempo real

---

## 📊 Dashboard (API)

* ✔ Total de clientes
* ✔ Total de estações
* ✔ Estações em uso
* ✔ Sessões ativas
* ✔ Faturamento do dia

---

## 📈 Gráficos (Analytics)

* ✔ Receita por período
* ✔ Sessões por dia
* ✔ Base para gráficos de linha/barra

---

## 🧪 Funcionalidades Implementadas

### 🔹 Customer

* ✔ Criar cliente com senha hash
* ✔ Listar clientes
* ✔ Buscar por ID
* ✔ Adicionar saldo (usuário autenticado)

---

### 🔹 Station

* ✔ Criar estação
* ✔ Listar estações
* ✔ Buscar por ID

---

### 🔹 Session

* ✔ Iniciar sessão (com usuário autenticado)
* ✔ Encerrar sessão
* ✔ Cálculo de tempo e valor
* ✔ Controle de status da estação
* ✔ Validação de sessão ativa (1 por usuário)
* ✔ Histórico de sessões

---

### 🔹 Autenticação

* ✔ Login com validação de senha
* ✔ Geração de JWT
* ✔ Proteção de endpoints
* ✔ Integração com Swagger

---

## 🔄 Fluxo de Uso Atual

1. Cliente se cadastra
2. Cliente realiza login
3. Recebe token JWT
4. Cliente adiciona crédito
5. Cliente inicia sessão:

   * Validação de token
   * Validação de saldo
   * Validação de estação
6. Cliente encerra sessão:

   * Tempo calculado
   * Valor calculado
   * Saldo debitado
7. Sistema dispara eventos em tempo real
8. Cliente pode consultar histórico

---

## ⚠️ Aprendizados Importantes

* Diferença entre ambientes SQL (Docker vs local)
* Importância de DTOs (segurança e desacoplamento)
* Uso correto de JWT e Claims
* Não confiar em dados vindos do cliente
* Separação de camadas (Clean Architecture)
* Uso correto de navegação no EF (`Include`)
* Ordem correta: salvar → notificar (SignalR)

---

## 🚧 Roadmap (Próximos Passos)

### 🥇 Fase 1 — Backend Profissional

* [ ] Padronização de respostas (Result Pattern)
* [ ] Middleware global de exceções
* [ ] Logs estruturados

---

### 🥈 Fase 2 — Dashboard Web (Vertex.Admin)

* [ ] Interface web (Razor Pages ou React)
* [ ] Lista de estações em tempo real
* [ ] Status visual (cores)
* [ ] Atualização via SignalR
* [ ] Cards de métricas

---

### 🥉 Fase 3 — Regras de Negócio Avançadas

* [ ] Planos de horas
* [ ] Preço por faixa de horário
* [ ] Promoções e descontos
* [ ] Controle de produtos (snacks, impressão)

---

### 🏁 Fase 4 — App Cliente (Estações)

* [ ] Bloqueio de tela
* [ ] Login local
* [ ] Contador de tempo
* [ ] Comunicação com API

---

### 🚀 Fase 5 — Evolução SaaS

* [ ] Multi-tenant (várias lan houses)
* [ ] Deploy em nuvem
* [ ] Escalabilidade

---

## 📈 Objetivo Final

Construir um sistema completo de gerenciamento de LAN house com:

* Controle de uso por estação
* Gestão de clientes
* Controle financeiro
* Monitoramento em tempo real
* Dashboard moderno
* Escalabilidade para SaaS

---

## 🧑‍💻 Autor

**Artenir Pinho**
Desenvolvedor .NET 🚀

---

## 💡 Observações

Este projeto está sendo desenvolvido com foco em aprendizado prático e aplicação de conceitos reais de mercado, evoluindo progressivamente para um sistema completo e profissional.

---
