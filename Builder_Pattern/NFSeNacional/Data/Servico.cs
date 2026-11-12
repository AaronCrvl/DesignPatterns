public class Servico
{
    public CodigoOperacao OBJ_CODIGO_OPERACAO { get; set; }
    public string TX_CODIGO_ITEM_LISTA_SERVICO { get; set; }
    public string TX_CODIGO_TRIBUTACAO_MUNICIPIO { get; set; }
    public string TX_DISCRIMINACAO_SERVICO { get; set; }
    public string TX_CODIGO_NBS { get; set; }

    /* VALORES ============================= */
    public decimal VL_VALOR_SERVICOS { get; set; }
    public decimal VL_VALOR_DEDUCOES { get; set; }
    public decimal VL_VALOR_PERC_DEDUCOES { get; set; }
    public decimal VL_ALIQUOTA_ISS { get; set; }
    public decimal VL_VALOR_ISS { get; set; }
    public decimal VL_VALOR_PIS { get; set; }
    public decimal VL_VALOR_COFINS { get; set; }
    public decimal VL_VALOR_IRRF { get; set; }
    public decimal VL_VALOR_CSLL { get; set; }
    public decimal VL_VALOR_TOTAL_TRIB_FED { get; set; }
    public decimal VL_VALOR_TOTAL_TRIB_EST { get; set; }
    public decimal VL_VALOR_TOTAL_TRIB_MUN { get; set; }

    /* IBS/CBS ============================= */
    public string TX_SITUACAO_TRIBUTARIA_IBS_CBS { get; set; }
    public string TX_CLASSIFICACAO_TRIBUTARIA_IBS_CBS { get; set; }
    public decimal VL_BASE_CALCULO_IBS_CBS { get; set; }
    public decimal VL_ALIQUOTA_IBS_ESTADUAL { get; set; }
    public decimal VL_ALIQUOTA_IBS_MUNICIPAL { get; set; }
    public decimal VL_ALIQUOTA_CBS { get; set; }
    public decimal VL_VALOR_TOTAL_NF_IBS_CBS { get; set; }
    public decimal VL_VALOR_CBS { get; set; }

    public Servico(string TX_CODIGO_OPERACAO)
    {
        OBJ_CODIGO_OPERACAO = new CodigoOperacao(TX_CODIGO_OPERACAO);
    }
}

public class CodigoOperacao
{
    public string NM_TIPO_OPERACAO { get; set; }
    public string TX_CODIGO_OPERACAO { get; set; }

    public CodigoOperacao(string TX_CODIGO_OPERACAO)
    {
        this.TX_CODIGO_OPERACAO = TX_CODIGO_OPERACAO;
        this.CarregarPorCodigoOperacao(TX_CODIGO_OPERACAO);
    }

    public void CarregarPorCodigoOperacao(string TX_CODIGO_OPERACAO)
    {
        // Buscando dados da base de dados
    }   
}