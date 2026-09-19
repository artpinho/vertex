using Vertex.Application.Abstractions.Persistence;
using Vertex.Application.Sessions.Services;

namespace Vertex.Api.BackgroundServices
{
    public sealed class MonitorSessaoPrePaga : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<MonitorSessaoPrePaga> _logger;

        public MonitorSessaoPrePaga(
            IServiceScopeFactory scopeFactory,
            ILogger<MonitorSessaoPrePaga> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(
            CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                "Monitor de sessões pré-pagas iniciado.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await ProcessarSessoesAsync(stoppingToken);
                }
                catch (OperationCanceledException)
                    when (stoppingToken.IsCancellationRequested)
                {
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogError(
                        ex,
                        "Erro no monitor de sessões pré-pagas.");
                }

                await Task.Delay(
                    TimeSpan.FromMinutes(1),
                    stoppingToken);
            }

            _logger.LogInformation(
                "Monitor de sessões pré-pagas finalizado.");
        }

        private async Task ProcessarSessoesAsync(
            CancellationToken cancellationToken)
        {
            using var scope =
                _scopeFactory.CreateScope();

            var sessaoRepository =
                scope.ServiceProvider
                    .GetRequiredService<ISessaoRepository>();

            var processador =
                scope.ServiceProvider
                    .GetRequiredService<ProcessadorSessaoPrePaga>();

            var sessoes =
                await sessaoRepository.ListarAtivasPrePagasAsync(
                    cancellationToken);

            foreach (var sessao in sessoes)
            {
                try
                {
                    await processador.ProcessarAsync(
                        sessao,
                        cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(
                        ex,
                        "Erro ao processar sessão pré-paga {SessaoId}.",
                        sessao.Id);
                }
            }
        }
    }
}
