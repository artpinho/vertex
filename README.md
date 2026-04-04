# 🚀 Vertex — LAN House Management System

## 📌 Sobre o Projeto

O **Vertex** é um sistema completo de gerenciamento para LAN houses e cyber cafés, com foco em controle de sessões, gestão de clientes, autenticação segura e controle financeiro.

O projeto está sendo desenvolvido com **.NET 9** utilizando princípios de **Clean Architecture**, visando escalabilidade, segurança e boas práticas do mercado.

---

## 🧱 Arquitetura da Solução

```
Vertex.sln

├── Vertex.API            → Controllers, autenticação, Swagger
├── Vertex.Application    → Services, DTOs, regras de negócio
├── Vertex.Domain         → Entidades e enums
├── Vertex.Infrastructure → EF Core, DbContext, persistência
├── Vertex.Admin          → Interface administrativa (futuro)
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
* Uso de **Claims**:

  * Username
  * CustomerId
* Endpoints protegidos com `[Authorize]`
* Swagger configurado com suporte a Bearer Token

---

## 💰 Controle Financeiro

* ✔ Validação de saldo antes de iniciar sessão
* ✔ Débito automático ao encerrar sessão
* ✔ Bloqueio de sessão sem saldo
* ✔ Adição de crédito via API (`add-balance`)

---

## 🧪 Funcionalidades Implementadas

### 🔹 Customer

* ✔ Criar cliente com senha hash
* ✔ Listar clientes
* ✔ Buscar por ID
* ✔ Adicionar saldo (com usuário autenticado)

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
* ✔ Validação de sessão ativa (não permite múltiplas)
* ✔ Histórico de sessões por usuário

---

### 🔹 Autenticação

* ✔ Login com validação de senha
* ✔ Geração de JWT
* ✔ Proteção de endpoints
* ✔ Integração com Swagger (Authorize)

---

## 🔄 Fluxo de Uso Atual

1. Cliente se cadastra
2. Cliente realiza login
3. API retorna JWT
4. Cliente adiciona crédito
5. Cliente inicia sessão:

   * API valida token
   * API valida saldo
   * API valida estação
6. Cliente encerra sessão:

   * Tempo calculado
   * Valor calculado
   * Saldo debitado
7. Cliente pode consultar histórico de sessões

---

## ⚠️ Aprendizados Importantes

* Diferença entre instâncias SQL (Docker vs Local)
* Configuração correta de Connection String
* Importância de DTOs (não expor entidade)
* Segurança com hashing de senha
* JWT e uso de Claims
* Separação de responsabilidades entre camadas
* Não confiar em dados do cliente (ex: CustomerId)
* Uso correto de navegação no EF (`Include`)

---

## 🚧 Roadmap (Próximos Passos)

### 🥇 Fase 1 — Backend Profissional

* [ ] Padronização de respostas (Result Pattern)
* [ ] Tratamento global de exceções (Middleware)
* [ ] Logs estruturados

---

### 🥈 Fase 2 — Funcionalidades Avançadas

* [ ] Dashboard administrativo
* [ ] Sessões em tempo real
* [ ] Relatórios (uso, faturamento)
* [ ] Ranking de usuários

---

### 🥉 Fase 3 — Regras de Negócio

* [ ] Planos de horas
* [ ] Preço por faixa de horário
* [ ] Promoções e descontos
* [ ] Controle de produtos (snacks, impressão)

---

### 🏁 Fase 4 — Interface Admin

* [ ] Painel web (Razor Pages)
* [ ] Monitoramento de estações
* [ ] Controle manual de sessões

---

### 🎮 Fase 5 — App Cliente (Estações)

* [ ] Bloqueio de tela
* [ ] Login local do usuário
* [ ] Contador de tempo
* [ ] Comunicação com API

---

### 🚀 Fase 6 — Evolução SaaS

* [ ] Multi-tenant (várias lan houses)
* [ ] Deploy em nuvem (Azure/AWS)
* [ ] Escalabilidade

---

## 📈 Objetivo Final

Construir um sistema completo de gerenciamento de LAN house com:

* Controle de uso por estação
* Gestão de clientes
* Controle financeiro
* Segurança robusta
* Interface moderna
* Escalabilidade para SaaS

---

## 🧑‍💻 Autor

**Artenir Pinho**
Desenvolvedor .NET 🚀

---

## 💡 Observações

Este projeto está evoluindo de forma incremental, aplicando conceitos reais de mercado e simulando cenários de produção desde o início.

---
