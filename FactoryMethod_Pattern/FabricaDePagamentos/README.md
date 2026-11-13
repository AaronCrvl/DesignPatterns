# Factory Method Pattern - Criando pagamentos

## Referências:
- [Realizar compras de teste - Checkout Pro (Mercado Pago)](https://www.mercadopago.com.br/developers/pt/docs/checkout-pro-preferences/integration-test/test-purchases)
- [Configurar notificações de pagamento - Webhooks (Mercado Pago)](https://www.mercadopago.com.mx/developers/en/docs/checkout-pro-preferences/payment-notifications.md)
- [GET /v1/payments/{id} - Obter pagamento (Mercado Pago)](https://www.mercadopago.com.mx/developers/pt/reference/online-payments/checkout-api-payments/get-payment/get)
- [GET /v1/payment_methods - Obter meios de pagamento (Mercado Pago)](https://www.mercadopago.com.mx/developers/pt/reference/online-payments/checkout-api-payments/payment-methods/get)
- [Checkout Transparente - Cartões, Orders API (Mercado Pago)](https://www.mercadopago.com.mx/developers/pt/docs/checkout-api-orders/payment-integration/websites/cards)


## Diagrama de Classes do Factory Method
<img width="1307" height="610" alt="image" src="https://github.com/user-attachments/assets/8f79407a-9c79-442d-9374-9e8cbc10361b" />


## Objetivo

Neste projeto, eu uso o Factory Method para criar o tipo de pagamento adequado sem deixar essa escolha espalhada pelos pontos de entrada da aplicação. O cenário trabalha com pagamentos via Pix e cartão de crédito, representados por [PagamentoPix](FabricaDePagamentos/FabricaDePagamentos/Data/PagamentoPix.cs) e [PagamentoCartao](FabricaDePagamentos/FabricaDePagamentos/Data/PagamentoCartao.cs).

## Como funciona

A interface [iProduct](FabricaDePagamentos/FabricaDePagamentos/Data/Interfaces/iProduto.cs) define o contrato comum dos pagamentos. A [PagamentoFactory](FabricaDePagamentos/FabricaDePagamentos/factory/PagamentoFactory.cs) recebe os dados, identifica qual implementação foi enviada e cria o produto correspondente:

```csharp
if (produto is PagamentoPix pix)
	produtoConcreto = new PagamentoPix(pix);

if (produto is PagamentoCartao cartao)
	produtoConcreto = new PagamentoCartao(cartao);
```

O fluxo de entrada de pagamentos está no [ApiPagamentos](FabricaDePagamentos/FabricaDePagamentos/controllers/ApiPagamentos.cs). Para notificações da plataforma, o [WebHookPagamentos](FabricaDePagamentos/FabricaDePagamentos/controllers/WebHookPagamentos.cs) encaminha os dados à mesma fábrica, que atualiza o status do pedido no cenário simulado.

Os papéis do padrão neste exemplo são:
- **Factory/ Creator:** [iFactory](FabricaDePagamentos/FabricaDePagamentos/factory/Interfaces/iFactory.cs), que define a operação de concretização.
- **Concrete Factory/ Concrete Creator:** [PagamentoFactory](FabricaDePagamentos/FabricaDePagamentos/factory/PagamentoFactory.cs), que escolhe e cria o tipo de pagamento.
- **Product:** [iProduct](FabricaDePagamentos/FabricaDePagamentos/Data/Interfaces/iProduto.cs), contrato comum dos produtos.
- **Concrete Products:** [PagamentoPix](FabricaDePagamentos/FabricaDePagamentos/Data/PagamentoPix.cs) e [PagamentoCartao](FabricaDePagamentos/FabricaDePagamentos/Data/PagamentoCartao.cs).

## Estado atual

Eu uso o Mercado Pago como referência para o cenário. A [IntegracaoService](FabricaDePagamentos/FabricaDePagamentos/service/IntegracaoService.cs) contém um exemplo de consulta de pagamento, e a fábrica ilustra o acompanhamento de status e o tratamento de notificações.

Este projeto é um estudo de padrão, não uma integração pronta para uso. O acesso ao banco está simulado na própria fábrica, e ainda faltam etapas para conectar e executar o fluxo completo, incluindo a configuração da aplicação e a integração real com a plataforma.
