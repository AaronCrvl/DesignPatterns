using Org.BouncyCastle.Crypto.Digests;

public class Cliente
{
    public string Nome { get; set; } = "nome Cliente Teste";
    public List<Email> EmailsCliente { get; set; }
    public List<Telefone> TelefonesCliente { get; set; }

    public bool RecebeNotificacaoWhatsApp { get => true; }
    public bool RecebeNotificacaoSlack { get => true; }
    public bool RecebeNotificacaoSMS { get => true; }

    public Cliente()
    {
        EmailsCliente = new List<Email>(){};
        TelefonesCliente = new List<Telefone>(){};
    }
}

