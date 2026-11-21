namespace Facade_Pattern.APIPedidos.services;

public interface IPagamentoService
{
    bool Cobrar(string cliente, decimal valor);
}
