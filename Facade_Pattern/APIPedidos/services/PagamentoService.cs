namespace Facade_Pattern.APIPedidos.services;

public class PagamentoService : IPagamentoService
{
    public bool Cobrar(string cliente, decimal valor)
    {
        return !string.IsNullOrWhiteSpace(cliente) && valor > 0;
    }
}
