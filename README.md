# 🚀 Vertex — LAN House Management System

## 📌 Sobre o Projeto

O **Vertex** é um sistema completo de gerenciamento para LAN houses e cyber cafés, com foco em:

* Controle de sessões
* Gestão de clientes
* Controle financeiro (créditos e débitos)
* Monitoramento em tempo real
* Dashboard administrativo

Desenvolvido com **.NET 9** e baseado em **Clean Architecture**, o projeto evolui progressivamente para um sistema SaaS escalável.

---

## 🧱 Arquitetura da Solução

```plaintext
Vertex.sln

├── Vertex.API            → Controllers, Auth, SignalR, Swagger
├── Vertex.Application    → Services, DTOs, Interfaces
├── Vertex.Domain         → Entidades e regras de negócio
├── Vertex.Infrastructure → EF Core, DbContext
├── Vertex.Admin          → Dashboard Web (Razor Pages)
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
* Async/Await
* Nullable Reference Types

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

* Login com username + senha
* Senhas protegidas com BCrypt
* Autenticação via JWT
* Uso de Claims:

  * Username
  * CustomerId
* Endpoints protegidos com `[Authorize]`
* Swagger com suporte a Bearer Token

---

## 💰 Controle Financeiro

* ✔ Validação de saldo antes de iniciar sessão
* ✔ Débito automático ao encerrar sessão
* ✔ Bloqueio de sessão sem saldo
* ✔ Adição de crédito via API

---

## ⚡ Tempo Real (SignalR)

* ✔ DashboardHub implementado
* ✔ Eventos em tempo real:

  * SessionStarted
  * SessionEnded
* ✔ Atualização automática do dashboard
* ✔ Abstração com `IRealtimeService`
* ✔ Implementação desacoplada na API

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
* ✔ Base para gráficos (Chart.js futuro)

---

## 🖥️ Vertex.Admin (Frontend)

* ✔ Projeto Razor Pages criado
* ✔ Página Dashboard
* ✔ Integração com API (fetch)
* ✔ Integração com SignalR
* ✔ Atualização em tempo real funcionando
* ✔ Listagem de estações com status dinâmico
* ✔ Consumo de métricas do dashboard

---

## 🧪 Funcionalidades Implementadas

### 🔹 Customer

* ✔ Criar cliente
* ✔ Listar clientes
* ✔ Buscar por ID
* ✔ Adicionar saldo

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
* ✔ Validação de sessão ativa
* ✔ Histórico de sessões

---

### 🔹 Autenticação

* ✔ Login com JWT
* ✔ Proteção de endpoints
* ✔ Integração com Swagger

---

## 🔄 Fluxo Atual do Sistema

1. Cliente se cadastra
2. Cliente realiza login
3. Recebe token JWT
4. Adiciona crédito
5. Inicia sessão:

   * Validação de token
   * Validação de saldo
   * Validação de estação
6. Encerra sessão:

   * Tempo calculado
   * Valor calculado
   * Débito do saldo
7. Sistema dispara eventos em tempo real
8. Dashboard atualiza automaticamente

---

## ⚠️ Aprendizados Importantes

* Uso correto de DTOs (não expor entidades)
* Autenticação com JWT e Claims
* Segurança: não confiar em dados do cliente
* Ordem correta: salvar no banco → disparar evento
* Separação de camadas (Clean Architecture)
* SignalR desacoplado via interface
* Integração frontend + backend + tempo real

---

## 🚧 Roadmap (Próximos Passos)

### 🥇 Fase 1 — Segurança no Admin

* [ ] Tela de login no Vertex.Admin
* [ ] Armazenar JWT (localStorage)
* [ ] Enviar token automaticamente nas requisições

---

### 🥈 Fase 2 — UI Profissional

* [ ] Melhorar layout (Bootstrap/Tailwind)
* [ ] Cards visuais para métricas
* [ ] Status das estações com cores e ícones
* [ ] Responsividade

---

### 🥉 Fase 3 — Funcionalidades Avançadas

* [ ] Gráficos com Chart.js
* [ ] Ranking de usuários
* [ ] Relatórios (diário/mensal)
* [ ] Filtros por período

---

### 🏁 Fase 4 — Controle Operacional

* [ ] Controle manual de sessões
* [ ] Encerrar sessão remotamente
* [ ] Bloquear estação

---

### 🎮 Fase 5 — App Cliente (Estações)

* [ ] Bloqueio de tela
* [ ] Login local
* [ ] Contador de tempo
* [ ] Comunicação com API

---

### 🚀 Fase 6 — Evolução SaaS

* [ ] Multi-tenant
* [ ] Deploy em nuvem
* [ ] Escalabilidade

---

## 📈 Objetivo Final

Criar um sistema completo de gerenciamento de LAN house com:

* Controle de uso por estação
* Gestão de clientes
* Controle financeiro
* Dashboard em tempo real
* Interface moderna
* Escalabilidade para SaaS

---

## 🧑‍💻 Autor

**Artenir Pinho**
Desenvolvedor .NET 🚀

---

## 💡 Observações

Este projeto está sendo desenvolvido com foco em aprendizado prático e evolução contínua, simulando um sistema real de produção desde o início.

---
