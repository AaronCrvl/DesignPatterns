using System.Dynamic;
using System.Text.Json;
using System.Threading.Tasks;

// Documentação do ViaCEP: https://viacep.com.br/
public class Localidade
{

    public string TX_CODIGO_IBGE { get; set; }
    public string TX_CEP { get; set; }
    public string TX_ENDERECO { get; set; }
    public int NR_NUMERO { get; set; }
    public string TX_BAIRRO { get; set; }

    public Localidade(string TX_LOGRADOURO, string TX_NUMERO, string TX_CEP)
    {
        this.CarregarDadosDeLocalidade(TX_LOGRADOURO, TX_NUMERO, TX_CEP);
    }

    public async Task CarregarDadosDeLocalidade(string TX_LOGRADOURO, string TX_NUMERO, string TX_CEP)
    {
        var dadosConsulta = default(ViaCEPDTO);
        using (var client = new HttpClient())
        {
            var resp = await client.GetAsync($"viacep.com.br/ws/{TX_CEP}/json/");
            resp.EnsureSuccessStatusCode();

            var content = await resp.Content.ReadAsStringAsync();
            dadosConsulta = JsonSerializer.Deserialize<ViaCEPDTO>(content);
        }

        if (dadosConsulta != null)
        {
            this.TX_CODIGO_IBGE = dadosConsulta.ibge;
            this.TX_CEP = dadosConsulta.cep;
            this.TX_ENDERECO = dadosConsulta.logradouro;
            this.NR_NUMERO = 0; // deve buscar no banco de dados
            this.TX_BAIRRO = dadosConsulta.bairro;
        }
    }
}