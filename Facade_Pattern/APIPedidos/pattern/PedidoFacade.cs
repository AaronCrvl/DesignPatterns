using Facade_Pattern.APIPedidos.domain;
using Facade_Pattern.APIPedidos.services;

namespace Facade_Pattern.APIPedidos.pattern;

public class PedidoFacade : IPedidoFacade
{
    private readonly IEstoqueService _estoque;
    private readonly IPagamentoService _pagamento;
    private readonly IEnvioService _envio;
    private readonly INotificacaoService _notificacao;

    public PedidoFacade(
        IEstoqueService estoque,
        IPagamentoService pagamento,
        IEnvioService envio,
        INotificacaoService notificacao)
    {
        _estoque = estoque;
        _pagamento = pagamento;
        _envio = envio;
        _notificacao = notificacao;
    }

    public PedidoResultado FinalizarPedido(Pedido pedido)
    {
        if (pedido.Valor <= 0 || string.IsNullOrWhiteSpace(pedido.Cliente) ||
            string.IsNullOrWhiteSpace(pedido.Descricao) || string.IsNullOrWhiteSpace(pedido.EnderecoEntrega))
        {
            return CriarResultado(pedido, StatusPedido.Cancelado, "Os dados do pedido são inválidos.");
        }

        if (!_estoque.Reservar(pedido.Descricao, 1))
        {
            pedido.Cancelar();
            const string mensagem = "Produto indisponível em estoque.";
            _notificacao.Enviar(pedido.Cliente, mensagem);
            return CriarResultado(pedido, StatusPedido.Cancelado, mensagem);
        }

        try
        {
            if (!_pagamento.Cobrar(pedido.Cliente, pedido.Valor))
            {
                _estoque.Liberar(pedido.Descricao, 1);
                pedido.Cancelar();
                const string mensagem = "Pagamento recusado. A reserva de estoque foi liberada.";
                _notificacao.Enviar(pedido.Cliente, mensagem);
                return CriarResultado(pedido, StatusPedido.Cancelado, mensagem);
            }

            pedido.ConfirmarPagamento();
            var codigoRastreio = _envio.AgendarEntrega(pedido.Cliente, pedido.EnderecoEntrega);
            pedido.MarcarComoEnviado();
            const string sucesso = "Pedido aprovado e enviado.";
            _notificacao.Enviar(pedido.Cliente, sucesso);

            return new PedidoResultado
            {
                PedidoId = pedido.Id,
                Status = pedido.Status,
                Mensagem = sucesso,
                CodigoRastreio = codigoRastreio
            };
        }
        catch
        {
            _estoque.Liberar(pedido.Descricao, 1);
            throw;
        }
    }

    private static PedidoResultado CriarResultado(Pedido pedido, StatusPedido status, string mensagem) => new()
    {
        PedidoId = pedido.Id,
        Status = status,
        Mensagem = mensagem
    };
}
