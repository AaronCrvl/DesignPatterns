using System.Text.Json;
using System.Threading.Tasks;

public class IntegracaoService
{
    string BASE_URL { get; set; }
    string ACCESS_TOKEN { get; set; }
    public IntegracaoService()
    {
        BASE_URL = "https://api.mercadopago.com";
        ACCESS_TOKEN = Environment.GetEnvironmentVariable("ACCESS_TOKEN");
    }

    public async Task<object?> BuscarPagamento(long idPagamento)
    {
        // GET /v1/payments/{id}
        using var clienteHttp = new HttpClient();
        clienteHttp.DefaultRequestHeaders.Add("Authorization", $"Bearer {ACCESS_TOKEN}");

        var res = await clienteHttp.GetAsync($"{BASE_URL}/v1/payments/{idPagamento}");
        res.EnsureSuccessStatusCode();
        var conteudo = await res.Content.ReadAsStringAsync();

        using var json = JsonDocument.Parse(conteudo);
        var raiz = json.RootElement;

        return new
        {
            Id = raiz.GetProperty("id").GetInt64(),
            TipoPagamento = raiz.GetProperty("payment_type_id").GetString(),
            MeioPagamento = raiz.GetProperty("payment_method_id").GetString(),
            Status = raiz.GetProperty("status").GetString(),
            DetalheStatus = raiz.GetProperty("status_detail").GetString(),
            Parcelas = raiz.GetProperty("installments").GetInt32(),
            ValorTotalPago = raiz.GetProperty("transaction_details").GetProperty("total_paid_amount").GetDecimal()
        };
    }
}