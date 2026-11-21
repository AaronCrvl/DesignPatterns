namespace Facade_Pattern.APIPedidos.services;

public interface IEnvioService
{
    string AgendarEntrega(string cliente, string endereco);
}
