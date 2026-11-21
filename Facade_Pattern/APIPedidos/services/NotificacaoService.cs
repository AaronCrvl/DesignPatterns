namespace Facade_Pattern.APIPedidos.services;

public class NotificacaoService : INotificacaoService
{
    public void Enviar(string cliente, string mensagem)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(cliente);
        ArgumentException.ThrowIfNullOrWhiteSpace(mensagem);
    }
}
