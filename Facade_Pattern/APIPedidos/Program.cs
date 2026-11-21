using Facade_Pattern.APIPedidos.domain;
using Facade_Pattern.APIPedidos.pattern;
using Facade_Pattern.APIPedidos.services;

var builder = WebApplication.CreateBuilder(args);

// Aplicação prática ============
// Singleton faz uma instância única do serviço, compartilhada em toda a aplicação.
// Scoped faz uma instância do serviço por requisição, compartilhada apenas na requisição.
// Para cumprir o padrão Facade, é necessário que os serviços sejam Singleton, pois o PedidoFacade é Scoped e precisa de instâncias únicas dos serviços para funcionar corretamente.
builder.Services.AddSingleton<IEstoqueService, EstoqueService>();
builder.Services.AddSingleton<IPagamentoService, PagamentoService>();
builder.Services.AddSingleton<IEnvioService, EnvioService>();
builder.Services.AddSingleton<INotificacaoService, NotificacaoService>();
builder.Services.AddScoped<IPedidoFacade, PedidoFacade>();

var app = builder.Build();

// Configuração do endpoint para finalizar pedidos
app.MapPost("/v1/pedidos", (Pedido pedido, IPedidoFacade pedidoFacade) =>
{
    var resultado = pedidoFacade.FinalizarPedido(pedido);
    return resultado.Status == StatusPedido.Cancelado
        ? Results.BadRequest(resultado)
        : Results.Ok(resultado);
});
app.Run();
