using MimeKit;
using Twilio;
using Twilio.Types;
using Twilio.Rest.Api.V2010.Account;

public class SMSMessenger : IMessenger
{
    private const string HeaderTelefonesSMS = "X-SMS-Recipients";
    private readonly string idConta;
    private readonly string token;
    private readonly string numeroOrigem;

    public SMSMessenger(IConfiguration conf)
    {
        idConta = conf["Integrações:WhatsApp:IdConta"] ?? "";
        token = conf["Integrações:WhatsApp:Token"] ?? "";
        numeroOrigem = conf["Integrações:WhatsApp:NumeroOrigem"] ?? "";
    }
    
    public bool EnviarMensagem(MimeMessage mensagem)
    {
        var telefones = mensagem.Headers[HeaderTelefonesSMS]
            ?.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            ?? [];
        var corpo = (mensagem.Body as TextPart)?.Text ?? mensagem.TextBody ?? "";

        if (telefones.Length == 0 || string.IsNullOrWhiteSpace(corpo))
            return false;

        try
        {
            TwilioClient.Init(idConta, token);
            var todosEnviados = true;

            foreach (var telefone in telefones)
            {
                try
                {
                    var destino = telefone.StartsWith("+:", StringComparison.OrdinalIgnoreCase)
                        ? telefone
                        : $"+{telefone.TrimStart('+')}";
                    var options = new CreateMessageOptions(new PhoneNumber(destino))
                    {
                        From = new PhoneNumber(numeroOrigem),
                        Body = corpo
                    };

                    MessageResource.Create(options);
                }
                catch
                {
                    todosEnviados = false;
                }
            }

            return todosEnviados;
        }
        catch
        {
            return false;
        }
    }
}
