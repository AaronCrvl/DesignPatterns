public class Prestador : IParteNota
{
    public string TX_CNPJ { get; set; }
    public string TX_INSCRICAO_MUNICIPAL { get; set; }
    public string TX_RAZAO_SOCIAL { get; set; }
    public string TX_CODIGO_CIDADE { get; set; }
    public string TX_EMAIL { get; set; } = "teste@BuilderPattern@algummail.com";
    public string TX_TELEFONE { get; set; } = "0319999999";

    public bool IN_EH_MEI { get; set; } = false;
    public bool IN_EH_ME_EPP { get; set; } = false;
    public bool IN_EH_OPTANTE_SIMPLES_NACIONAL { get; set; } = true;
}