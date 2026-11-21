namespace Facade_Pattern.APIPedidos.services;

public interface INotificacaoService
{
    void Enviar(string cliente, string mensagem);
}
