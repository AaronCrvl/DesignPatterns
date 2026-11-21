namespace Facade_Pattern.APIPedidos.services;

public interface IEstoqueService
{
    bool Reservar(string sku, int quantidade);
    void Liberar(string sku, int quantidade);
}
