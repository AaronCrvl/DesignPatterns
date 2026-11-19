// Decoradores concretos: delegam e depois somam o seu próprio comportamento
using MimeKit;

namespace RedirecionamentoSMTP.Decorators
{
    public class SMSConcreteDecorator : NotificadorBaseDecorator
    {
        private const string HeaderTelefonesSMS = "X-SMS-Recipients";
        private readonly IConfiguration configuracoesAplicacao;

        public SMSConcreteDecorator(iNotificador _notificador, IConfiguration _config) : base(_notificador)
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
            mensagem.Headers.Add(HeaderTelefonesSMS, string.Join(",", telefones));
            mensagem.Body = new TextPart("plain")
            {
                Text = $"Olá! Você recebeu uma fatura. {Mensagem}"
            };

            var enviado = await ConectarEEnviarAsync(mensagem);
            return enviado ? "Mensagem de SMS enviada." : "Falha no envio da mensagem de SMS.";
        }

        public override async Task<bool> ConectarEEnviarAsync(MimeMessage mensagemDTO)
        {
            var messenger = new SMSMessenger(configuracoesAplicacao);
            return await Task.Run(() => messenger.EnviarMensagem(mensagemDTO));
        }
    }
}