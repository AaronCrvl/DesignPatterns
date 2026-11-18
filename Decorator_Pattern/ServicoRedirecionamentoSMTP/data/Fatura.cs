using System.ComponentModel.DataAnnotations;

public class Fatura
{
    [Key]
    public long IdFatura { get; set; }
    public DateTime DataVencimento { get; set; }
    public DateTime DataCriacao { get; set; }

    [NaoZerado(ErrorMessage = "Valor da fatura não pode ser zerado")]
    public double Valor { get; set; }
    public Cliente Cliente { get; set; }

    public bool pendenteEnvioDocumento
    {
        get
        {
            // lógica de acesso à uma fila de envios
            return FaturaHelper.DocumentoFaturaEnviado(this.IdFatura);
        }
    }

    public Fatura()
    {
        DataCriacao = DateTime.Now;
        DataVencimento = DateTime.Now.AddDays(30);
        Valor = 99.9;
        Cliente = new Cliente();
    }
}