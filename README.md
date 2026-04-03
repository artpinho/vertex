# 🚀 Vertex — LAN House Management System

## 📌 Sobre o Projeto

O **Vertex** é um sistema de gerenciamento para LAN houses e cyber cafés, focado em controle de uso de estações, gestão de clientes e autenticação segura.

O projeto está sendo desenvolvido com **.NET 9** e segue princípios de **Clean Architecture**, com separação clara entre camadas e foco em boas práticas de mercado.

---

## 🧱 Arquitetura da Solução

```
Vertex.sln

├── Vertex.API            → Camada HTTP (Controllers / Swagger / Auth)
├── Vertex.Application    → Regras de negócio (Services / DTOs)
├── Vertex.Domain         → Entidades e regras do domínio
├── Vertex.Infrastructure → Persistência (EF Core / SQL Server)
├── Vertex.Admin          → Interface administrativa (futuro)
├── Vertex.Client         → App das estações (futuro)
```

---

## 🧠 Conceitos Aplicados

* Clean Architecture
* Separation of Concerns
* Dependency Injection
* Entity Framework Core (Code First)
* SQL Server (Docker)
* REST API
* DTO Pattern
* JWT Authentication
* Password Hashing (BCrypt)
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
* Balance
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

---

## 🔐 Autenticação e Segurança

* Login com **username + senha**
* Senhas protegidas com **BCrypt**
* Autenticação via **JWT (Bearer Token)**
* Claims incluídas:

  * Username
  * CustomerId
* Endpoints protegidos com `[Authorize]`

---

## ⚙️ Tecnologias Utilizadas

* .NET 9
* ASP.NET Core Web API
* Entity Framework Core
* SQL Server (Docker)
* Swagger (Swashbuckle)
* BCrypt.Net
* JWT Bearer Authentication

---

## 🧪 Funcionalidades Implementadas

### 🔹 Customer

* ✔ Criar cliente (com senha hash)
* ✔ Listar clientes
* ✔ Buscar por ID
* ✔ DTO de entrada e saída

---

### 🔹 Station

* ✔ Criar estação
* ✔ Listar estações
* ✔ Buscar por ID

---

### 🔹 Session

* ✔ Iniciar sessão
* ✔ Encerrar sessão
* ✔ Cálculo de tempo e valor
* ✔ Controle de status da estação
* ✔ Uso do usuário autenticado (JWT)

---

### 🔹 Autenticação

* ✔ Login
* ✔ Geração de JWT
* ✔ Proteção de endpoints
* ✔ Integração com Swagger (Authorize)

---

## 🔄 Fluxo de Uso (Atual)

1. Cliente se cadastra
2. Cliente realiza login
3. API retorna JWT
4. Cliente utiliza token para acessar endpoints protegidos
5. Inicia sessão em uma estação:

   * API valida usuário via token
   * API valida estação disponível
6. Encerra sessão:

   * Calcula tempo
   * Calcula valor
   * Libera estação

---

## ⚠️ Aprendizados Importantes

* Diferença entre instâncias SQL (Docker vs Local)
* Importância da Connection String correta
* Uso correto de DTOs (entrada vs saída)
* Segurança com hashing de senha
* Uso de JWT e Claims
* Separação de responsabilidades entre camadas
* Não confiar em dados vindos do cliente (ex: CustomerId)

---

## 🚧 Roadmap (Próximos Passos)

### 🥇 Fase 1 — Regras de Negócio

* [ ] Validar sessão ativa por usuário
* [ ] Controle de saldo/créditos
* [ ] Bloqueio de sessão sem saldo

---

### 🥈 Fase 2 — Segurança Avançada

* [ ] Refresh Token
* [ ] Expiração configurável
* [ ] Roles (Admin / User)

---

### 🥉 Fase 3 — Funcionalidades

* [ ] Planos de horas
* [ ] Preço por faixa de horário
* [ ] Descontos e promoções
* [ ] Produtos (snacks, impressão)

---

### 🏁 Fase 4 — Interface Admin

* [ ] Dashboard de estações
* [ ] Controle em tempo real
* [ ] Relatórios

---

### 🎮 Fase 5 — App Cliente (Estações)

* [ ] Bloqueio de tela
* [ ] Login do usuário
* [ ] Contador de tempo
* [ ] Comunicação com API

---

### 🚀 Fase 6 — Evolução SaaS

* [ ] Multi-tenant (várias lan houses)
* [ ] Deploy em nuvem
* [ ] Escalabilidade

---

## 📈 Objetivo Final

Construir um sistema completo de gerenciamento de LAN house com:

* Controle de uso por estação
* Gestão de clientes
* Controle financeiro
* Interface moderna
* Escalabilidade para SaaS

---

## 🧑‍💻 Autor

**Artenir Pinho**
Desenvolvedor .NET 🚀

---

## 💡 Observações

Este projeto está sendo desenvolvido com foco em aprendizado prático, aplicando conceitos reais de mercado e evoluindo progressivamente para um sistema completo.

---
