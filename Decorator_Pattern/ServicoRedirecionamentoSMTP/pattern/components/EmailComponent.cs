// Decorador base: É um Notificador e TEM um Notificador (agregação)
// Esta classe tem a função única de executar a sua funcionalidade base, sem levar em conta a demais
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

// TODO:
//  - Modularização das validações de valores de variáveis vindas dos arquivos config - OK
//  - Incorporar um cenário real de mensagem de fatura
namespace RedirecionamentoSMTP.Componentes
{
    public class EmailComponent : iNotificador
    {
        private readonly IConfiguration configuracoesAplicacao;

        public EmailComponent(IConfiguration _config)
        {
            configuracoesAplicacao = _config;
        }

        public async Task<string> CriarMensagemAsync(string Mensagem, string Assunto, string Destinatario, Fatura faturaParaEnviar)
        {
            var remetente = configuracoesAplicacao["SMTP:UserName"];
            if (string.IsNullOrWhiteSpace(remetente))
                throw new InvalidOperationException("A configuração SMTP:UserName não foi definida.");

            var mensagem = new MimeMessage();
            mensagem.From.Add(new MailboxAddress("Remetente", remetente));
            mensagem.To.Add(new MailboxAddress("Destinatário", Destinatario));
            mensagem.Subject = Assunto;
            mensagem.Body = new TextPart("plain") { Text = Mensagem };

            var foiEnviado = await ConectarEEnviar(mensagem);
            if (foiEnviado)
                return "Mensagem enviada via e-mail";

            throw new InvalidOperationException("Não foi possível enviar a mensagem por e-mail. Problemas de configuração de mensagem.");
        }

        public async Task<bool> ConectarEEnviar(MimeMessage mensagemDTO)
        {
            try
            {
                var servidorSMTP = configuracoesAplicacao["SMTP:Host"];
                var portaTexto = configuracoesAplicacao["SMTP:Porta"];
                var utilizaSSLTexto = configuracoesAplicacao["SMTP:UtilizaSSL"];
                var usuario = configuracoesAplicacao["SMTP:UserName"];
                var senha = configuracoesAplicacao["SMTP:Password"];
                var utilizaSSL = bool.TryParse(utilizaSSLTexto, out var sslConfigurado) && sslConfigurado;
                
                if (string.IsNullOrWhiteSpace(servidorSMTP))
                    throw new InvalidOperationException("A configuração SMTP:Host não foi definida.");
                if (!int.TryParse(portaTexto, out var portaSMTP))
                    throw new InvalidOperationException("A configuração SMTP:Porta é inválida.");
                if (string.IsNullOrWhiteSpace(usuario))
                    throw new InvalidOperationException("A configuração SMTP:UserName não foi definida.");                

                using var client = new SmtpClient();
                await client.ConnectAsync(
                    servidorSMTP,
                    portaSMTP,
                    utilizaSSL ? SecureSocketOptions.StartTls : SecureSocketOptions.None);

                if (!string.IsNullOrEmpty(senha))
                    await client.AuthenticateAsync(usuario, senha);
                
                var resultadoEnvio = await client.SendAsync(new SmtpSendRequest(mensagemDTO));
                await client.DisconnectAsync(true);

                if(resultadoEnvio.AcceptedRecipients.Any() && !resultadoEnvio.RejectedRecipients.Any())
                    return true; 

                throw new InvalidOperationException("Não foi possível enviar a mensagem por e-mail. Problemas de conexão e envio.");                                   
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Falha ao enviar mensagem via SMTP.", e);
            }
        }
    }
}