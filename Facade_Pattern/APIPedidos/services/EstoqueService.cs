namespace Facade_Pattern.APIPedidos.services;

public class EstoqueService : IEstoqueService
{
    public bool Reservar(string sku, int quantidade)
    {
        return !string.IsNullOrWhiteSpace(sku) && quantidade > 0;
    }

    public void Liberar(string sku, int quantidade)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantidade);
    }
}
