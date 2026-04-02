# 🚀 Vertex — LAN House Management System

## 📌 Sobre o Projeto

O **Vertex** é um sistema de gerenciamento para LAN houses e cyber cafés, desenvolvido com foco em controle de sessões de uso de estações, gestão de clientes e futura expansão para controle financeiro, produtos e serviços.

O sistema segue uma arquitetura moderna baseada em **.NET** com separação em camadas (Clean Architecture), visando escalabilidade, manutenção e boas práticas de mercado.

---

## 🧱 Arquitetura do Projeto

A solução foi estruturada da seguinte forma:

```
Vertex.sln

├── Vertex.API           → Camada de entrada (HTTP / Controllers)
├── Vertex.Application   → Regras de negócio (Services)
├── Vertex.Domain        → Entidades e regras do domínio
├── Vertex.Infrastructure→ Persistência (EF Core / Banco de dados)
├── Vertex.Admin         → Interface administrativa (futuro)
├── Vertex.Client        → App das estações (futuro)
```

---

## 🧠 Conceitos Aplicados

* Clean Architecture
* Separation of Concerns
* Dependency Injection
* Entity Framework Core (Code First)
* SQL Server (Docker)
* REST API
* Async/Await
* Domain Modeling

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

## 🔐 Segurança

* Senhas armazenadas com **hash (BCrypt)**
* Nunca armazenar senha em texto plano
* Preparado para futura autenticação com JWT

---

## ⚙️ Tecnologias Utilizadas

* .NET 9
* ASP.NET Core Web API
* Entity Framework Core
* SQL Server (Docker)
* Swagger (Swashbuckle)
* BCrypt.Net

---

## 🧪 Funcionalidades Implementadas (MVP atual)

* ✔ Cadastro estrutural de entidades
* ✔ Configuração do banco com EF Core
* ✔ Migrations funcionando
* ✔ Controle de sessão:

  * Iniciar sessão
  * Encerrar sessão
  * Cálculo de tempo e valor
* ✔ Controle de status da estação
* ✔ Hash de senha

---

## 🔄 Fluxo de Sessão

1. Cliente inicia sessão em uma estação
2. Sistema valida disponibilidade
3. Sessão é criada
4. Estação muda para **InUse**
5. Ao encerrar:

   * Calcula tempo
   * Calcula valor
   * Libera estação

---

## ⚠️ Aprendizados Importantes

* Diferença entre instâncias de banco (Docker vs Local)
* Importância da connection string correta
* Separação de camadas
* Migrations podem afetar dados
* Nem todo "número" deve ser tipo numérico (ex: telefone)

---

## 🚧 Roadmap (Próximos Passos)

### 🥇 Fase 1 — API Completa

* [ ] CustomerController (CRUD)
* [ ] StationController (CRUD)
* [ ] DTOs (não expor entidades)
* [ ] Validações (FluentValidation)

---

### 🥈 Fase 2 — Autenticação

* [ ] Login de usuário
* [ ] JWT
* [ ] Controle de acesso

---

### 🥉 Fase 3 — Regras de Negócio Avançadas

* [ ] Planos de tempo
* [ ] Preço por horário
* [ ] Descontos
* [ ] Créditos

---

### 🏁 Fase 4 — Interface

* [ ] Vertex.Admin (Razor Pages)
* [ ] Dashboard de estações
* [ ] Controle em tempo real

---

### 🎮 Fase 5 — App Cliente (Estações)

* [ ] Bloqueio de tela
* [ ] Login do usuário
* [ ] Contador de tempo
* [ ] Comunicação com API

---

### 🚀 Fase 6 — Diferenciais

* [ ] Relatórios
* [ ] Produtos (snacks, impressão)
* [ ] Promoções
* [ ] Controle remoto de máquinas

---

## 📈 Objetivo Final

Construir um sistema completo de gerenciamento de LAN house com:

* Controle de uso
* Gestão financeira
* Interface amigável
* Escalabilidade para SaaS

---

## 🧑‍💻 Autor

**Artenir Pinho**
Desenvolvedor .NET em evolução 🚀

---

## 💡 Observações

Este projeto está sendo desenvolvido com foco em aprendizado contínuo, aplicando boas práticas do mercado e evoluindo progressivamente em complexidade.

---
