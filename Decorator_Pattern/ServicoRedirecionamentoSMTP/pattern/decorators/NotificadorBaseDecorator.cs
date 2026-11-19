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

        // 02 - quem herdar esta classe irá chamar sobrescrever os métodos e
        // irá aplicar suas próprias regras.

        // 03 - Os métodos são marcados como virtuais para que a subrecrita seja feita nos
        // decorators concretos (WhatsApp, SMS e etc...)
        public virtual Task<string> CriarMensagemAsync(string Mensagem, string Assunto, string Destinatario, Fatura faturaParaEnviar)
        {
            return this.notificadorBase.CriarMensagemAsync(Mensagem, Assunto, Destinatario, faturaParaEnviar);
        }

        public virtual Task<bool> ConectarEEnviarAsync(MimeMessage mensagemDTO)
        {
            return this.notificadorBase.ConectarEEnviarAsync(mensagemDTO);
        }
    }
}