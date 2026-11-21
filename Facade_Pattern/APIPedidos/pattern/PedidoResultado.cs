using Facade_Pattern.APIPedidos.domain;

namespace Facade_Pattern.APIPedidos.pattern;

public class PedidoResultado
{
    public int PedidoId { get; set; }
    public StatusPedido Status { get; set; }
    public string Mensagem { get; set; } = string.Empty;
    public string? CodigoRastreio { get; set; }
}
