namespace NFSe
{
    public class NFSeNacional
    {
        #region Variáveis Privadas
        public DateTime DT_EMISSAO { get; set; }
        public int NR_SERIE { get; set; }
        public int NR_NUMERO_DPS { get; set; }
        public Servico OBJ_SERVICO { get; set; }
        public Localidade OBJ_LOCALIDADE { get; set; }
        public decimal VL_VALOR_LIQUIDO { get; set; }
        public long NR_CHAVE_NFSE_SUBSTITUIDA { get; set; }
        public int NR_NUMERO_NFSE { get; set; }
        public Prestador OBJ_PRESTADOR { get; set; }
        public Tomador OBJ_TOMADOR { get; set; }

        public bool IN_TEM_DEDUCOES { get; set; } = false;
        public bool IN_TEM_DOCUMENTOS_REFERENCIADOS { get; set; } = false;
        #endregion

        #region Construtor
        public NFSeNacional(string TX_CODIGO_OPERACAO, string TX_CODIGO_IBGE)
        {
            this.OBJ_SERVICO = new Servico(TX_CODIGO_OPERACAO);
            this.OBJ_LOCALIDADE = new Localidade("", "", "01001000");
        }
        #endregion
    }
}