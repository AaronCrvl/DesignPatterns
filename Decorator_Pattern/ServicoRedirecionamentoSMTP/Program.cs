using ServicoRedirecionamentoSMTP;

var builder = Host.CreateDefaultBuilder(args)
    .UseWindowsService(options =>
    {
        options.ServiceName = "ServicoRedirecionamentoSMTP";
    })
    .ConfigureServices((hostContext, services) =>
    {
        services.AddSingleton<BDExemplo>();
        services.AddHostedService<Worker>();
    });

builder.Build().Run();