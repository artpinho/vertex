# 🎮 Vertex LAN Manager

Sistema de gerenciamento de **LAN House**, desenvolvido com foco em controle de estações, clientes, sessões, tarifação e operações financeiras.

O projeto está sendo construído de forma incremental, utilizando **Clean Architecture + Modular Monolith**, mantendo a API como autoridade das regras de negócio.

---

## 🚀 Tecnologias

![.NET 9](https://img.shields.io/badge/.NET-9.0-512BD4)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-9.0-512BD4)
![EF Core](https://img.shields.io/badge/EF%20Core-9.0-512BD4)
![C%23](https://img.shields.io/badge/C%23-13-239120)
![SQL Server](https://img.shields.io/badge/SQL%20Server-2022-CC2927)
![Tests](https://img.shields.io/badge/Tests-29%20passing-success)

---

## 🏗️ Arquitetura

O Vertex utiliza uma arquitetura modular baseada em Clean Architecture:

```text
Vertex
│
├── Vertex.Domain
├── Vertex.Application
├── Vertex.Infrastructure
├── Vertex.Contracts
├── Vertex.Api
└── Vertex.Client
```

Princípios adotados:

- Domain independente de infraestrutura
- Controllers enxutos
- Application responsável pelos casos de uso
- Infrastructure responsável por persistência e integrações
- DTOs/Contracts para comunicação
- EF Core + SQL Server
- JWT para autenticação
- Regras financeiras centralizadas
- Histórico financeiro preservado

---

# 🖥️ Funcionalidades Implementadas

## 🏢 Estações

Controle das estações da LAN House.

Cada estação possui:

- Nome
- Número
- Status
- Ativa/Inativa
- Computador associado

Status:

```text
Livre
EmUso
Bloqueada
Manutencao
```

Operações:

- Criar
- Listar
- Consultar
- Associar computador
- Alterar status
- Ativar/desativar

Um computador não pode estar associado a mais de uma estação simultaneamente.

---

# 💻 Computadores

Informações:

- HostName
- IP
- MAC Address
- Sistema Operacional
- Versão do cliente
- Último heartbeat
- Status
- Tipo de máquina

Status:

```text
Offline
Online
Bloqueado
Manutencao
```

Operações:

- Cadastro
- Consulta
- Atualização
- Alteração de status
- Heartbeat
- Associação de tipo de máquina
- Provisionamento de credencial
- Rotação de credencial

---

# 🔐 Autenticação e Segurança

Fluxo:

```text
ClientId + ClientSecret
        ↓
      Login
        ↓
       JWT
        ↓
Endpoints protegidos
```

O `ClientSecret` não é armazenado em texto puro.

Utiliza:

- PBKDF2
- SHA-256
- Salt aleatório
- 100.000 iterações
- Comparação em tempo constante
- Histórico de credenciais

---

# 👤 Clientes

Dados:

- Nome
- CPF
- E-mail
- Telefone
- Data de nascimento
- Ativo
- Data de cadastro

Operações:

- Criar
- Listar
- Consultar
- Atualizar
- Ativar/desativar

Não existe exclusão física do cliente, preservando o histórico.

---

# 🎮 Sessões

Uma sessão representa o período de utilização de uma estação por um cliente.

Dados:

```text
Cliente
Estação
Início
Fim
Status
Tipo de cobrança
```

Status:

```text
Ativa
Encerrada
Cancelada
```

Tipos:

```text
Pré-paga
Pós-paga
```

---

## 💰 Sessão Pré-paga

O cliente utiliza o saldo disponível em sua carteira.

Fluxo:

```text
Carteira
   ↓
Saldo disponível
   ↓
Início da sessão
   ↓
Cálculo do tempo permitido
   ↓
Sessão ativa
   ↓
Monitor automático
   ↓
Limite atingido
   ↓
Encerramento automático
   ↓
Tarifação final
   ↓
Débito da carteira
   ↓
Liberação da estação
```

Regras implementadas:

- Saldo positivo para iniciar.
- Não é necessário possuir saldo para uma hora completa.
- O tempo permitido é calculado pela tarifação vigente.
- A sessão é encerrada automaticamente quando o limite financeiro é atingido.
- O encerramento financeiro considera o tempo real utilizado.
- O consumo tarifado é persistido.
- O débito da carteira é persistido.
- A movimentação financeira é persistida.
- A estação é liberada.

### Monitor automático

A API possui um `BackgroundService`:

```text
MonitorSessaoPrePaga
        ↓
Sessões pré-pagas ativas
        ↓
ProcessadorSessaoPrePaga
        ↓
Verifica limite permitido
        ↓
Encerra quando necessário
```

O fluxo foi validado com persistência completa no banco.

---

# 💵 Sessão Pós-paga

A modalidade pós-paga permite que uma sessão seja encerrada posteriormente e tenha seu valor calculado ao final.

A utilização dessa modalidade será autorizada somente ao perfil operacional adequado.

O cliente não poderá simplesmente informar `TipoCobranca = PosPaga` para obter uma sessão pós-paga sem autorização.

---

# 💳 Carteira do Cliente

Cada cliente pode possuir uma carteira reutilizável.

```text
Cliente
   │
   └── Carteira
          │
          ├── Saldo
          └── Movimentações
```

A carteira poderá ser utilizada para:

- Sessões
- Produtos
- Outros débitos futuros

---

## 💰 Recargas

A recarga aumenta o saldo da carteira e representa uma entrada financeira.

Tipos de pagamento:

```text
Dinheiro
PIX
Cartão de Débito
Cartão de Crédito
```

Toda recarga gera uma movimentação financeira.

---

## 📒 Movimentações da Carteira

Tipos:

```text
Crédito
Débito
Estorno
```

Uma movimentação registra:

- Valor
- Tipo
- Tipo de pagamento
- Sessão relacionada
- Venda relacionada
- Data
- Descrição

O consumo de uma sessão não representa uma nova entrada de dinheiro.

```text
Recarga R$ 50,00
       ↓
Saldo R$ 50,00
       ↓
Sessão R$ 8,00
       ↓
Saldo R$ 42,00
```

---

# 💵 Tarifação

O **Motor de Tarifação** determina o valor de utilização da estação considerando:

- Tipo de máquina
- Data
- Dia da semana
- Horário
- Configuração de tarifa
- Prioridade
- Promoções
- Faixas de horário
- Descontos

---

## 🧮 Tarifas

Uma configuração possui:

- Nome
- Descrição
- Tipo de máquina
- Valor por hora
- Data de início
- Data de fim
- Prioridade
- Status

Exemplo:

```text
08:00 ───── 17:00   R$ 9/h
17:00 ───── 18:00   R$ 10/h
18:00 ───── 22:00   R$ 12/h
```

---

# ⏱️ Faixas de Horário

Permitem valores diferentes durante o dia e regras por dia da semana.

Faixas adjacentes são permitidas.

Sobreposição de faixas dentro da mesma configuração não é permitida.

---

# 🎁 Promoções

Promoções podem aplicar:

- Percentual de desconto
- Valor fixo por hora
- Período de validade
- Dias da semana
- Faixas de horário
- Tipo de máquina
- Prioridade

Podem valer para todos os tipos ou apenas tipos específicos.

Quando mais de uma promoção é aplicável, a prioridade determina a regra utilizada. Não ocorre empilhamento.

---

# 🧾 Histórico da Tarifação

A tarifação aplicada é persistida através de `ConsumoTarifacao`.

Cada segmento registra:

```text
Início
Fim
Configuração de tarifação
Promoção
Valor/hora
Desconto
Valor
```

Alterações futuras nas tarifas não alteram o histórico financeiro das sessões encerradas.

---

# 🧮 Precisão da Cobrança

A cobrança é proporcional ao tempo real utilizado.

Exemplo:

```text
Tarifa: R$ 10,00/h

1 hora       = R$ 10,00
1h30         = R$ 15,00
1h37m42s     = valor proporcional
```

O motor divide o período sempre que ocorre mudança de:

- tarifa
- faixa horária
- promoção
- período de validade

---

# 🖥️ Tipos de Máquina

Os tipos são dados dinâmicos do banco.

Exemplos:

```text
Padrão
VIP
Premium
```

O tipo de máquina influencia a tarifação.

Operações:

- Criar
- Listar
- Consultar
- Atualizar
- Ativar/desativar

Não existe preço fixo por computador.

---

# 📦 Produtos e Vendas

Estrutura prevista:

### Produto

```text
Id
Nome
Descrição
PreçoVenda
EstoqueAtual
EstoqueMinimo
Ativo
```

### Venda

```text
Id
Cliente
Data
ValorTotal
Status
```

### ItemVenda

```text
Id
Venda
Produto
Quantidade
PrecoUnitario
Subtotal
```

O preço unitário é copiado para o item no momento da venda, preservando o histórico.

---

# 🌐 API

Versionamento:

```text
/api/v1/
```

Principais grupos:

```text
/api/v1/clientes
/api/v1/clientes/{id}/carteira
/api/v1/computadores
/api/v1/estacoes
/api/v1/sessoes
/api/v1/tipos-maquina
/api/v1/configuracoes-tarifacao
/api/v1/faixas-horario-tarifacao
/api/v1/promocoes
```

A API é documentada e testada manualmente através do Swagger.

---

# 🧪 Testes

Atualmente:

```text
29 testes
29 passando
0 falhas
```

Os testes são concentrados em regras de negócio, segurança e fluxos críticos.

---

# 🗄️ Banco de Dados

Banco:

```text
SQL Server 2022
```

Banco de desenvolvimento:

```text
VertexDb
```

O Entity Framework Core 9 é utilizado para:

- Mapeamento
- Persistência
- Migrations
- Configuração das entidades

O histórico financeiro é preservado independentemente de alterações futuras nas configurações atuais.

---

# 🗺️ Roadmap

## ✅ Concluído

- [x] Estrutura inicial
- [x] Clean Architecture
- [x] Modular Monolith
- [x] Entidades principais
- [x] Estações
- [x] Computadores
- [x] Clientes
- [x] Sessões
- [x] Associação computador ↔ estação
- [x] Tipos de máquina
- [x] Tarifas
- [x] Faixas de horário
- [x] Promoções
- [x] Dias da semana em promoções
- [x] Faixas de horário em promoções
- [x] Motor de tarifação
- [x] Persistência de consumo tarifado
- [x] Carteira de clientes
- [x] Recarga
- [x] Extrato da carteira
- [x] Débito de sessão
- [x] Sessão pré-paga
- [x] Cálculo do limite da sessão pré-paga
- [x] Encerramento automático da sessão pré-paga
- [x] Persistência do encerramento automático
- [x] Autenticação JWT
- [x] Credenciais de computador
- [x] Heartbeat
- [x] Swagger

## 🔜 Próximos passos

- [ ] Autorização de sessão pós-paga
- [ ] Produtos
- [ ] Vendas
- [ ] Controle de estoque
- [ ] Integração de vendas com carteira
- [ ] SignalR para atualização em tempo real
- [ ] Cliente WPF
- [ ] Dashboard administrativo
- [ ] Relatórios financeiros
- [ ] Fechamento de caixa

---

# 🎯 Objetivo do MVP

O objetivo é permitir que uma LAN House consiga:

```text
Cadastrar máquinas
       ↓
Organizar estações
       ↓
Cadastrar clientes
       ↓
Controlar carteiras
       ↓
Receber recargas
       ↓
Iniciar sessões
       ↓
Calcular tarifas automaticamente
       ↓
Controlar sessões pré-pagas
       ↓
Encerrar sessões automaticamente
       ↓
Registrar movimentações financeiras
       ↓
Preservar todo o histórico
```

A evolução será feita por módulos, mantendo as regras financeiras centralizadas na API e evitando complexidade desnecessária no MVP.

---

## 👨‍💻 Autor

**Artenir Pinho**

Projeto pessoal de estudo e desenvolvimento profissional utilizando .NET 9, Clean Architecture, EF Core e SQL Server.
