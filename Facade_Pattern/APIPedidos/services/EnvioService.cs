namespace Facade_Pattern.APIPedidos.services;

public class EnvioService : IEnvioService
{
    public string AgendarEntrega(string cliente, string endereco)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(cliente);
        ArgumentException.ThrowIfNullOrWhiteSpace(endereco);
        return $"BR{Guid.NewGuid():N}"[..14].ToUpperInvariant();
    }
}
