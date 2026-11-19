using MimeKit;

namespace RedirecionamentoSMTP.Decorators
{
    public class WhatsAppConcreteDecorator : NotificadorBaseDecorator
    {
        private const string HeaderTelefonesWhatsApp = "X-WhatsApp-Recipients";
        private readonly IConfiguration configuracoesAplicacao;

        public WhatsAppConcreteDecorator(iNotificador _notificador, IConfiguration _config) : base(_notificador)
        {
            configuracoesAplicacao = _config;
        }

        public override async Task<string> CriarMensagemAsync(string Mensagem, string Assunto, string Destinatario, Fatura faturaParaEnviar)
        {
            var telefones = faturaParaEnviar.Cliente.TelefonesCliente
                .Select(telefone => $"{telefone.DDD}{telefone.Numero}")
                .ToArray();

            var mensagem = new MimeMessage();
            mensagem.Subject = Assunto;
            mensagem.Headers.Add(HeaderTelefonesWhatsApp, string.Join(",", telefones));
            mensagem.Body = new TextPart("plain")
            {
                Text = $"Olá! Você recebeu uma fatura. {Mensagem}"
            };
            
            var enviado = await ConectarEEnviarAsync(mensagem);
            return enviado ? "Mensagem de WhatsApp enviada." : "Falha no envio da mensagem de WhatsApp.";
        }

        public override async Task<bool> ConectarEEnviarAsync(MimeMessage mensagemDTO)
        {
            var messenger = new WhatsAppMessenger(configuracoesAplicacao);
            return await Task.Run(() => messenger.EnviarMensagem(mensagemDTO));
        }
    }
}
