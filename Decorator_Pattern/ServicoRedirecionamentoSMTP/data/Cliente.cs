using Org.BouncyCastle.Crypto.Digests;

public class Cliente
{
    public string Nome { get; set; } = "nome Cliente Teste";
    public Email EmailCliente { get; set; }
    public Telefone TelefoneCliente { get; set; }

    public bool RecebeNotificacaoWhatsApp { get => true; }
    public bool RecebeNotificacaoSlack { get => true; }
    public bool RecebeNotificacaoSMS { get => true; }

    public Cliente()
    {
        EmailCliente = new Email();
        TelefoneCliente = new Telefone();
    }
}

