// Decoradores concretos: delegam e depois somam o seu próprio comportamento
using MimeKit;

namespace RedirecionamentoSMTP.Decorators
{
    public class SlackConcreteDecorator : NotificadorBaseDecorator
    {
        public SlackConcreteDecorator(iNotificador _notificador) : base(_notificador)
        {
        }

        public override async Task<string> CriarMensagemAsync(string Mensagem, string Assunto, string Destinatario, Fatura faturaParaEnviar)
        {
            return "";
        }

        public override async Task<bool> ConectarEEnviar(MimeMessage mensagemDTO)
        {
            try
            {
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}