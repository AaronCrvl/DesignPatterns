# Design Patterns

Este repositório reúne projetos práticos para estudar padrões de projeto por meio de cenários inspirados em integrações e aplicações do dia a dia. Cada projeto apresenta o padrão em um contexto próprio; as integrações externas servem como referência e não estão conectadas a serviços reais.

## Projetos

- [Builder Pattern — Processando uma NFSe Nacional](Builder_Pattern/NFSeNacional/README.md): construção de uma nota fiscal a partir de seus componentes.
- [Factory Method Pattern — Criando pagamentos](FactoryMethod_Pattern/FabricaDePagamentos/README.md): criação de diferentes tipos de pagamento.
- [Decorator Pattern — Gerenciando envio de mensagens](Decorator_Pattern/ServicoRedirecionamentoSMTP/README.MD): composição de canais de notificação.
- [Facade Pattern — Finalizando pedidos](Facade_Pattern/README.md): coordenação do fluxo de estoque, pagamento, envio e notificação.

## Tecnologias e conceitos explorados

Os projetos também permitem praticar recursos relacionados aos cenários, como criação de documentos em texto no Builder, consumo simulado de API e webhook no Factory Method, composição de integrações de mensageria no Decorator e injeção de dependências na Minimal API do Facade.

No projeto Facade, o endpoint depende de `IPedidoFacade`, enquanto a fachada recebe as interfaces dos serviços internos. O `Program.cs` registra essas dependências com tempos de vida adequados ao exemplo:

- **Singleton:** uma instância compartilhada durante toda a execução da aplicação. É adequado aos serviços simulados sem estado por requisição.
- **Scoped:** uma instância por requisição HTTP. A fachada é registrada dessa forma para manter seu ciclo de vida associado ao fluxo de cada pedido.

Essas escolhas dependem do estado e das dependências de cada serviço. Serviços com estado específico de uma requisição ou que dependem de componentes scoped, como um contexto de banco, também devem ter um ciclo de vida compatível.
