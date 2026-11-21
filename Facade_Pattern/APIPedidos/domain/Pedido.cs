namespace Facade_Pattern.APIPedidos.domain;

public class Pedido
{
    public int Id { get; set; }
    public string Cliente { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public string EnderecoEntrega { get; set; } = string.Empty;
    public StatusPedido Status { get; private set; } = StatusPedido.Criado;

    public void ConfirmarPagamento()
    {
        if (Status != StatusPedido.Criado)
            throw new InvalidOperationException("Somente pedidos criados podem ter o pagamento confirmado.");

        Status = StatusPedido.PagamentoAprovado;
    }

    public void Cancelar()
    {
        if (Status is StatusPedido.Enviado or StatusPedido.Entregue)
            throw new InvalidOperationException("Pedidos enviados ou entregues não podem ser cancelados.");

        Status = StatusPedido.Cancelado;
    }

    public void MarcarComoEnviado()
    {
        if (Status != StatusPedido.PagamentoAprovado)
            throw new InvalidOperationException("Somente pedidos com pagamento aprovado podem ser enviados.");

        Status = StatusPedido.Enviado;
    }
}
