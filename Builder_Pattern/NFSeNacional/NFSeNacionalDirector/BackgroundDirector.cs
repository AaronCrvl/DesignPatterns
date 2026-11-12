using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NFSe;

public class BackgroundDirector : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<BackgroundDirector> _logger;

    public BackgroundDirector(IServiceScopeFactory scopeFactory, ILogger<BackgroundDirector> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var timer = new PeriodicTimer(TimeSpan.FromMinutes(5));

        do
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<MyDbContext>();
                var builder = new NFSeBuilder();

                List<NFSeNacional> pending = await db.FilaProcessamentoNotas
                    .Where(o => o.Status == "PENDENTE")
                    .ToListAsync<NFSeNacional>(stoppingToken);

                foreach (var nota in pending)
                {
                    var tx2 = "";
                    tx2 += builder.Cabecalho();
                    tx2 += builder.DPS(nota);
                }

                // Disparo dos dados via integração
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Polling failed");
            }
        }
        while (await timer.WaitForNextTickAsync(stoppingToken));
    }
}