# 🚀 Vertex — LAN House Management System

## 📌 Sobre o Projeto

O **Vertex** é um sistema completo de gerenciamento para LAN houses e cyber cafés, com foco em:

* Controle de sessões
* Gestão de clientes
* Controle financeiro (créditos e débitos)
* Monitoramento em tempo real
* Dashboard administrativo web

Desenvolvido com **.NET 9** e baseado em **Clean Architecture**, o projeto simula um sistema real de produção desde o início.

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
* ✔ Base para gráficos

---

## 🖥️ Vertex.Admin (Frontend)

### ✔ Implementado

* Projeto Razor Pages criado
* Página de Login
* Autenticação integrada com API
* Token JWT armazenado em Session
* Dashboard protegido (redirect sem login)
* Consumo de API com Authorization Bearer
* Integração com SignalR
* Atualização em tempo real funcionando
* Listagem de estações dinâmica
* Correção de bug de token no JS (ordem de scripts)

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
* ✔ Login no Vertex.Admin

---

## 🔄 Fluxo Atual do Sistema

1. Cliente se cadastra
2. Cliente realiza login (API ou Admin)
3. Recebe token JWT
4. Token é armazenado (Session no Admin)
5. Cliente adiciona crédito
6. Inicia sessão:

   * Validação de token
   * Validação de saldo
   * Validação de estação
7. Encerra sessão:

   * Tempo calculado
   * Valor calculado
   * Débito do saldo
8. Sistema dispara eventos em tempo real
9. Dashboard atualiza automaticamente

---

## ⚠️ Aprendizados Importantes

* Integração completa frontend + backend + realtime
* Uso correto de JWT no frontend (Bearer)
* Importância da ordem de scripts (JS + Razor)
* Não confiar em dados do cliente
* Separação de responsabilidades (Clean Architecture)
* SignalR desacoplado via interface
* Ordem correta: salvar no banco → notificar

---

## 🚧 Roadmap (Próximos Passos)

### 🥇 Fase 1 — UI Profissional

* [ ] Melhorar layout (Bootstrap ou Tailwind)
* [ ] Cards visuais para métricas
* [ ] Status das estações com cores e badges
* [ ] Responsividade

---

### 🥈 Fase 2 — Dashboard Avançado

* [ ] Gráficos com Chart.js
* [ ] Filtros por período
* [ ] Indicadores (KPIs)
* [ ] Ranking de usuários

---

### 🥉 Fase 3 — Funcionalidades de Negócio

* [ ] Planos de horas
* [ ] Preço por horário
* [ ] Promoções
* [ ] Produtos (snacks, impressão)

---

### 🏁 Fase 4 — Controle Operacional

* [ ] Encerrar sessão remotamente
* [ ] Bloquear estação
* [ ] Monitoramento em tempo real avançado

---

### 🎮 Fase 5 — App Cliente

* [ ] Bloqueio de tela
* [ ] Login local
* [ ] Contador de tempo
* [ ] Comunicação com API

---

### 🚀 Fase 6 — SaaS

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

Este projeto evoluiu de um backend simples para um sistema completo com autenticação, controle financeiro e tempo real, aproximando-se de um produto real de mercado.

---
