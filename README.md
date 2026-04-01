🚀 Vertex LAN Manager

Sistema de gerenciamento de LAN House focado em simplicidade, controle de sessões e operação eficiente.

🎯 Este repositório contém o MVP (Minimum Viable Product) do sistema.

🧠 Objetivo do Projeto

O Vertex LAN Manager foi desenvolvido com o objetivo de oferecer uma solução simples e funcional para controle de uso de computadores em LAN Houses.

Este MVP permite:

✅ Controlar uso das máquinas
✅ Identificar clientes
✅ Medir tempo de uso
✅ Calcular valor automaticamente
✅ Bloquear e liberar estações

⚡ Foco total em simplicidade, estabilidade e funcionamento.

📦 Escopo do MVP
👤 Clientes (Básico)
Nome
CPF (opcional)
Saldo (R$ ou horas)

🔸 Login simplificado:

Nome ou ID
Sem autenticação complexa
💻 Estações
Nome da máquina (ex: PC-01)
Status:
🟢 Livre
🔴 Em uso
⛔ Bloqueada
⏱️ Sessão (Core do Sistema)
Cliente
Estação
Data de início
Data de fim
Tempo total (minutos)
Valor calculado
💰 Regra de Preço

Modelo simples:

valor = (tempo_em_minutos / 60) * preco_hora

Exemplo:

30 minutos → R$ 2,50 (considerando R$5/h)
🖥️ Aplicação da Estação (Cliente)

Funcionalidades:

Tela bloqueada ao iniciar
Login por nome ou ID
Botão "Iniciar Sessão"
Contador de tempo
Botão "Encerrar Sessão"
Comunicação com API
🧑‍💼 Painel Administrativo
Lista de estações
Status em tempo real
Ações:
Liberar estação
Encerrar sessão
Cadastrar cliente
🔄 Comunicação
API REST (obrigatório)
SignalR (opcional para versões futuras)
❌ Fora do MVP

🚫 NÃO implementar nesta fase

Produtos / vendas
Promoções
Agenda de preços
Relatórios avançados
Controle de estoque
Perfis de usuário
Integração com pagamento

👉 Estes itens fazem parte da versão 2+

🧱 Modelagem (EF Core)
Cliente
Cliente
- Id
- Nome
- CPF (nullable)
- Saldo
Estacao
Estacao
- Id
- Nome
- Status
Sessao
Sessao
- Id
- ClienteId
- EstacaoId
- DataInicio
- DataFim
- TempoMinutos
- ValorCobrado
🔄 Fluxo Principal
🟢 Iniciar Sessão
Cliente acessa a estação
Realiza login
App chama API: IniciarSessao

Servidor:

Verifica disponibilidade
Cria sessão
Atualiza estação → Em uso
🔴 Durante Uso
App envia heartbeat (ex: a cada 10s)
ou
Controle local com sincronização
🔴 Encerrar Sessão
Cliente ou admin encerra
API:
Calcula tempo
Calcula valor
Atualiza sessão
Libera estação
🧩 Estrutura da Solution (.NET)
VertexLanManager.sln

- VertexLanManager.API
- VertexLanManager.Domain
- VertexLanManager.Infrastructure
- VertexLanManager.Application
- VertexLanManager.Client (WPF)
- VertexLanManager.Admin (Web)
🗺️ Roadmap do MVP
🥇 Fase 1 — Backend
Criar solution
Criar entidades
Configurar DbContext
Criar migrations
Criar endpoints:
Cliente
Estação
Sessão
🥈 Fase 2 — Regras de Negócio
Iniciar sessão
Encerrar sessão
Calcular valor
🥉 Fase 3 — App da Estação
Tela bloqueada
Login
Contador
Integração com API
🏁 Fase 4 — Painel Administrativo
Monitoramento das estações
Controle de sessões
💡 Filosofia do Projeto

⚠️ Não tente fazer tudo perfeito.

Prioridades:

✔ Funcionar
✔ Ser estável
✔ Ser simples
👨‍💻 Autor

Artenir Pinho
Desenvolvedor .NET
