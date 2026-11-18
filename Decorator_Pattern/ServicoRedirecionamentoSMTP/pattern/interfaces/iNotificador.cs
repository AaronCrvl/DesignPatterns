//Interface comum: cliente e decoradores falam a mesma língua
using MimeKit;

public interface iNotificador
{
    Task<string> CriarMensagemAsync(string Mensagem, string Assunto, string Destinatario, Fatura faturaParaEnviar);
    Task<bool> ConectarEEnviar(MimeMessage mensagemDTO);
}