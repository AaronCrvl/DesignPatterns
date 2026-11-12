public class Tomador : IParteNota
{
    public string TX_CNPJ { get; set; }
    public string TX_INSCRICAO_MUNICIPAL { get; set; }
    public string TX_RAZAO_SOCIAL { get; set; }
    public string TX_CODIGO_CIDADE { get; set; }
    public string TX_EMAIL { get; set; } = "teste@BuilderPattern@algummail.com";
    public string TX_TELEFONE { get; set; } = "0319999999";
    public Localidade OBJ_LOCALIDADE { get; set; }
}