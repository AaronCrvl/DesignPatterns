using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NFSe;

public class BackgroundDirector : BackgroundService
{
    private readonly IServiceScopeFactory _scopo;
    private readonly ILogger<BackgroundDirector> _logger;

    public BackgroundDirector(IServiceScopeFactory scopeFactory, ILogger<BackgroundDirector> logger)
    {
        _scopo = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var timer = new PeriodicTimer(TimeSpan.FromMinutes(5));

        do
        {
            try
            {
                using var escopo = _scopo.CreateScope();
                var bd = escopo.ServiceProvider.GetRequiredService<BDExemplo>();
                var builder = new NFSeBuilder();

                var pendentes = bd.FilaProcessamentoNotas
                    .Where(o => o.Status == "PENDENTE")                    
                    .ToList();

                foreach (var fila in pendentes)
                {
                    try
                    {
                        var xml = builder.Cabecalho() + builder.DPS(fila.nota);
                        // Disparo dos dados via integração        

                        fila.Status = "ENVIADO";
                        fila.DataEnvio = DateTime.Now;
                    }
                    catch (Exception ex)
                    {
                        fila.Status = "ERRO";
                        fila.MensagemErro = ex.Message;
                        // log...
                    }
                }                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro durante o processamento da fila de notas pendentes.");
            }
        }
        while (await timer.WaitForNextTickAsync(stoppingToken));
    }
}

#region Simulando Acesso ao Banco de Dados
internal class BDExemplo
{
    public IEnumerable<FilaProcessamentoNotas> FilaProcessamentoNotas { get; internal set; }
}

internal class FilaProcessamentoNotas
{
    public string Status { get; set; }

    public string MensagemErro { get; set; }

    public DateTime DataEnvio { get; set; }

    public NFSeNacional nota { get; set; }
}
#endregion