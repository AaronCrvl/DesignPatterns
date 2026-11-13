using System.Net.Http.Headers;

public class PagamentoFactory : iFactory
{
    private readonly IServiceScopeFactory _escopo;
    private IntegracaoService _integracao;

    public PagamentoFactory(IServiceScopeFactory escopo)
    {
        _escopo = escopo;
    }

    public int ConcretizarPagamento(object objPagamento)
    {
        var produtoConcreto = default(iProduct);
        if (objPagamento is iProduct produto)
        {
            using var escopo = _escopo.CreateScope();
            using var bd = escopo.ServiceProvider.GetRequiredService<BDExemplo>();
            var pedido = bd.Pedidos.FirstOrDefault(pedido => pedido.ID_PEDIDO == produto.orderId);

            // chamada via API -> concretizar produto -> enviar para integração de gerenciamento de pagamento
            if (pedido != null && pedido.ID_PEDIDO.Contains("PED"))
            {
                var retorno = VerificarStatusPedido(pedido);
                if (retorno.HasValue)
                    return retorno.Value;

                if (pedido.ID_STATUS_PEDIDO == EStatusPedido.EnviadoParaGerenciamento)
                {
                    dynamic pagamento = _integracao.BuscarPagamento(pedido.NR_PEDIDO).GetAwaiter().GetResult();
                    if (pagamento.Status == "approved")
                    {
                        pedido.ID_STATUS_PEDIDO = EStatusPedido.Pago;
                        pedido.SalvarPedido();
                        return 200;
                    }

                    // ainda em análise ou aguardando pagamento (ex.: Pix): não cria outro
                    if (pagamento.Status == "pending" || pagamento.Status == "in_process" || pagamento.Status == "authorized")
                        return 200;

                    pedido.ID_STATUS_PEDIDO = EStatusPedido.Pendente;
                }

                if (pedido.ID_STATUS_PEDIDO == EStatusPedido.Pendente)
                {
                    if (produto is PagamentoPix pix)
                        produtoConcreto = new PagamentoPix(pix);

                    if (produto is PagamentoCartao cartao)
                        produtoConcreto = new PagamentoCartao(cartao);

                    produto.ObjectChecks();
                }

                // lógica de chamada de API externa....
                // o produto concreto, no caso o pagamento, será gerenciado externamente pelo MercadoPago
                // posteriormente o webhook será notificado pelo MercadoPago sobre as alterações no status do pedido

                pedido.ID_STATUS_PEDIDO = EStatusPedido.EnviadoParaGerenciamento;
                pedido.SalvarPedido();
                return 200;
            }
            else
                throw new InvalidDataException("o objeto de dados enviado está fora do padrão proposto.");

        }

        // chamada via WebHook -> atualizar pedido e produto concretixado
        if (objPagamento is iNotification notificacao)
        {
            using var escopo = _escopo.CreateScope();
            using var bd = escopo.ServiceProvider.GetRequiredService<BDExemplo>();
            var pedido = bd.Pedidos.FirstOrDefault(pedido => pedido.NR_PEDIDO == notificacao.id);

            if (pedido != null)
            {
                var retorno = VerificarStatusPedido(pedido);
                if (retorno.HasValue)
                    return retorno.Value;

                pedido.ID_STATUS_PEDIDO = EStatusPedido.Pago;
                pedido.SalvarPedido();
                return 200;
            }
            else
                throw new InvalidDataException("o objeto de dados enviado está fora do padrão proposto.");
        }

        return 505; // Falha Genérica
    }

    private static int? VerificarStatusPedido(Pedido pedido)
    {
        return pedido.ID_STATUS_PEDIDO switch
        {
            EStatusPedido.Pago => 202,
            EStatusPedido.Cancelado => 500,
            _ => null
        };
    }
}

#region Simulando Acesso ao Banco de Dados
internal class BDExemplo : IDisposable
{
    public required IEnumerable<Pedido> Pedidos { get; internal set; }
    public void Dispose()
    {
    }
}

internal class Pedido
{
    public required string ID_PEDIDO { get; set; }
    public required long NR_PEDIDO { get; set; }
    public EStatusPedido ID_STATUS_PEDIDO { get; set; }
    public void SalvarPedido()
    {
    }
}

internal enum EStatusPedido
{
    Pendente = 0,
    Pago = 1,
    Cancelado = 2,
    EnviadoParaGerenciamento = 3
}
#endregion