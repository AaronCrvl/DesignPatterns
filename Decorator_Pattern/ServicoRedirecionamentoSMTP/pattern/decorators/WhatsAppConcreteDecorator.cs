using MimeKit;

namespace RedirecionamentoSMTP.Decorators
{
    public class WhatsAppConcreteDecorator : NotificadorBaseDecorator
    {
        IConfiguration configuracoesAplicacao;

        public WhatsAppConcreteDecorator(iNotificador _notificador, IConfiguration _config) : base(_notificador)
        {
            this.configuracoesAplicacao = _config;
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