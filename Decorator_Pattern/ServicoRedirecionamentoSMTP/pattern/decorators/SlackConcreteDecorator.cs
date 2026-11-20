// Decoradores concretos: delegam e depois somam o seu próprio comportamento
using MimeKit;
using SlackBotMessages;
using SlackBotMessages.Models;

namespace RedirecionamentoSMTP.Decorators
{
    public class SlackConcreteDecorator : NotificadorBaseDecorator
    {
        private IConfigurationManager _config;   
             
        public SlackConcreteDecorator(iNotificador notificador, IConfigurationManager config) : base(notificador)
        {
            _config = config;
        }

        public override async Task<string> CriarMensagemAsync(string Mensagem, string Assunto, string Destinatario, Fatura faturaParaEnviar)
        {
            var mensagem = new MimeMessage
            {
                Subject = Assunto,
                Body = new TextPart("plain")
                {
                    Text = "Olá! Você recebeu uma fatura. "
                }
            };

            var enviado = await this.ConectarEEnviarAsync(mensagem);
            if (enviado)
                return $"Mensagem de Slack enviada.";

            throw new Exception("Erro ao criar DTO da mensagem.");
        }

        public override async Task<bool> ConectarEEnviarAsync(MimeMessage mensagemDTO)
        {
            try
            {
                var slackURL = this._config["Integrações:Slack:SlackUrl"]?.ToString() ?? "";
                if (string.IsNullOrEmpty(slackURL))
                    throw new InvalidDataException("Configure a URL do seu canal do Slack nos arquivos.");

                var textoMensagem = (mensagemDTO.Body as TextPart)?.Text ?? "";
                var client = new SbmClient(slackURL);
                var slackMessage = new Message
                {
                    Text = textoMensagem,
                    Channel = mensagemDTO.Subject,

                };

                var resultado = await client.SendAsync(slackMessage);
                if (resultado.Contains("success"))
                    return true;

                throw new Exception("Erro ao se conectar e enviar mensagem via Slack.");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}