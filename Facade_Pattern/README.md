# Facade Pattern - Finalizando pedidos

## Referências

- [Facade Pattern - Refactoring Guru](https://refactoring.guru/design-patterns/facade)

## Diagrama de Classes do Facade

O diagrama representa o endpoint como cliente da interface `IPedidoFacade`. A implementação `PedidoFacade` coordena os serviços de estoque, pagamento, envio e notificação por meio de suas interfaces.

## Objetivo

O Facade Pattern fornece uma interface simples é unificada, para direcionar a execução de diversos serviços. Neste projeto, o endpoint de pedidos depende apenas de `IPedidoFacade`, enquanto a fachada coordena as etapas necessárias para finalizar um pedido.

## Como funciona

O endpoint `POST /v1/pedidos` recebe um pedido e chama `FinalizarPedido` na fachada. A fachada valida os dados, reserva o produto, tenta cobrar o cliente e, quando o pagamento é aprovado, agenda a entrega e envia uma notificação. Se o pagamento for recusado, a fachada libera a reserva e informa o cliente.

As implementações dos serviços são simuladas e não acessam estoque, pagamento, transportadora ou mensageria externos.

## Objetos do padrão no projeto

- **Cliente:** endpoint Minimal API em [Program.cs](APIPedidos/Program.cs), que depende apenas de `IPedidoFacade`.
- **Facade:** [PedidoFacade](APIPedidos/pattern/PedidoFacade.cs), responsável por coordenar o fluxo.
- **Interface da Facade:** [IPedidoFacade](APIPedidos/pattern/IPedidoFacade.cs), contrato consumido pelo endpoint.
- **Interfaces do subsistema:** `IEstoqueService`, `IPagamentoService`, `IEnvioService` e `INotificacaoService`.
- **Serviços concretos:** `EstoqueService`, `PagamentoService`, `EnvioService` e `NotificacaoService`.
- **Dados do fluxo:** [Pedido](APIPedidos/domain/Pedido.cs), [StatusPedido](APIPedidos/domain/StatusPedido.cs) e [PedidoResultado](APIPedidos/pattern/PedidoResultado.cs).

## Execução

Na pasta `APIPedidos`, execute:

```bash
dotnet run
```

Envie um pedido para `POST /v1/pedidos` com um corpo JSON como:

```json
{
  "id": 1,
  "cliente": "Aaron",
  "descricao": "SKU-001",
  "valor": 99.90,
  "enderecoEntrega": "BOMB B, 1:36"
}
```

## Estado atual

Este projeto é um exemplo didático do Facade Pattern. As regras dos serviços são simuladas em memória e podem ser substituídas por provedores concretos sem alterar o endpoint, desde que mantenham as interfaces do subsistema.
