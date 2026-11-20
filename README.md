# DesignPatterns

Design patterns não era um dos meus pontos fortes, então decidi mudar isso criando projetos com cenários reais para praticar cada um dos sete patterns que já estudei. Cada pattern tem seu próprio projeto, onde trabalho sua arquitetura na prática.

Os projetos são inspirados em exemplos de um módulo de integração aberta e nas documentações que encontrei.

Estes projetos não são implementações ativas das integrações. Usei essas integrações como referência para criar cenários de implementação. Se eu for integrar com alguma delas, este material também pode servir como um bom How-To!

## Projetos Trabalhados

- [Builder Pattern - Processando uma NFSe Nacional](Builder_Pattern/NFSeNacional/README.md)
- [Factory Method Pattern - Criando pagamentos](FactoryMethod_Pattern/FabricaDePagamentos/README.md)
- [Decorator Pattern - Gerenciando envio de mensagens (SMTP, SMS, WhatsApp, etc..)](Decorator_Pattern/ServicoRedirecionamentoSMTP/README.MD)

## Objetivos Sundários e Relevantes

Este projeto abre espaço para aprendizado de demais tecnologias.

- O [Builder Pattern](Builder_Pattern/) trabalha com uma integração com DTOs em modelo de arquivo texto específico e consigo explorar a tratativa de criação de string no C# 11.0.

- O [Factory Method Pattern](FactoryMethod_Pattern/) trabalha com uma modularização de integrações com foco na integração do Mercado Pago, fazendo o consumo da API e do WebHook. Utilizo uma arquitetura de recebimento centralizada mas que é flexível para tratariva do produto, no caso, pagamento.

- O [Decorator Pattern](Decorator_Pattern/) trabalha diretamente com diferente integrações de mensageria, visando criar um padrão de transmissão fléxivel com base em opções pré-configuradas.