//  Decorador base: É um Notificador e TEM um Notificador (agregação)

using MimeKit;

namespace RedirecionamentoSMTP.Decorators
{
    public class NotificadorBaseDecorator : iNotificador
    {
        iNotificador notificadorBase;

        public NotificadorBaseDecorator(iNotificador _notificador)
        {
            this.notificadorBase = _notificador;
        }

        // 01 - por padrão ele apenas repassa a ação para o próximo passo.

        // 02 - quem herdar esta classe irá chamar primeiramente o método base e posteriormente
        // irá aplicar suas próprias regras.

        // 03 - Os métodos são virtuais para que a subrecrita seja feita nos
        // decorators concretos (WhatsApp, SMS e etc...)
        public virtual Task<string> CriarMensagemAsync(string Mensagem, string Assunto, string Destinatario, Fatura faturaParaEnviar)
        {
            return this.notificadorBase.CriarMensagemAsync(Mensagem, Assunto, Destinatario, faturaParaEnviar);
        }

        public virtual Task<bool> ConectarEEnviar(MimeMessage mensagemDTO)
        {
            return this.notificadorBase.ConectarEEnviar(mensagemDTO);
        }
    }
}