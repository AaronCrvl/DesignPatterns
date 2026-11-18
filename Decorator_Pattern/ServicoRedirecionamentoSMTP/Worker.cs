using System.Collections.Concurrent;
using RedirecionamentoSMTP.Componentes;

namespace ServicoRedirecionamentoSMTP;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IServiceScopeFactory _escopo;
    private readonly IConfiguration _configuracoes;

    public Worker(ILogger<Worker> logger, IServiceScopeFactory escopo, IConfiguration configuracoes)
    {
        _logger = logger;
        _escopo = escopo;
        _configuracoes = configuracoes;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // intervalo simples de 10 minutos para o background service
        using var timerPeriodo = new PeriodicTimer(new TimeSpan(0, 10, 0));
        do
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }

            var escopo = _escopo.CreateScope();
            var bd = escopo.ServiceProvider.GetRequiredService<BDExemplo>();
            var faturasPendentesParaEnvio = bd.faturas.Where(f => f.pendenteEnvioDocumento);
            var resultados = new ConcurrentBag<string>();

            var options = new ParallelOptions
            {
                MaxDegreeOfParallelism = 5,// supondo um caso de 5 faturas por vez
                CancellationToken = stoppingToken
            };

            await Parallel.ForEachAsync(faturasPendentesParaEnvio, options, async (fatura, cancellationToken) =>
            {
                var componente = new EmailComponent(_configuracoes);
                var res = await componente.CriarMensagemAsync(
                        $"Olá! Segue em anexo os documentos da fatura relativa à {fatura.DataCriacao:yyyy/MM}",
                        $"Fatura - {fatura.DataCriacao:yyyy/MM}",
                        fatura.Cliente.EmailCliente.Endereco,
                        fatura
                    );
                resultados.Add(res);
            });
        }
        while (await timerPeriodo.WaitForNextTickAsync(stoppingToken));
    }
}

#region Simulação BD

internal class BDExemplo
{
    public IEnumerable<Fatura> faturas { get; set; } = [];
}
#endregion