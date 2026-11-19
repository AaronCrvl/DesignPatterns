using MimeKit;

public interface IMessenger
{
    bool EnviarMensagem(MimeMessage mensagem);
}