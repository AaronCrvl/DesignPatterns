using Facade_Pattern.APIPedidos.domain;

namespace Facade_Pattern.APIPedidos.pattern;

public interface IPedidoFacade
{
    PedidoResultado FinalizarPedido(Pedido pedido);
}
