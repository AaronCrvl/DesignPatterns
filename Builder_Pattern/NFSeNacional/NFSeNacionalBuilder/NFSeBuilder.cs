using System.Globalization;
using NFSe;

public class NFSeBuilder : iNFSeBuilder
{
    public string Cabecalho() => """
        formato=tx2
        padrao=TecnoNFSeNacional
        Versao=v1.01
    """;

    public string DPS(NFSeNacional nota) => $"""
        INCLUIRDPS

        {/* DPS ============================= */      string.Empty}
        DataEmissaoDPS={nota.DT_EMISSAO.ToString("yyyy-MM-dd")}
        SerieDps={nota.NR_SERIE}
        NumeroDps={nota.NR_NUMERO_DPS}
        Competencia={nota.DT_EMISSAO.ToString("yyyy-MM-dd")}
        FinalidadeNFSe=0
        OperacaoPessoal=0
        CodigoOperacao={nota.OBJ_SERVICO.OBJ_CODIGO_OPERACAO.TX_CODIGO_OPERACAO}
        TipoOperacao=2
        TipoEnteGovernamental=2
        TipoEmitente=1
        CodigoCidadeEmitente={nota.OBJ_LOCALIDADE.TX_CODIGO_IBGE}
        NumeroNfse={nota.NR_NUMERO_NFSE}        
        ValorLiquidoNfse={nota.VL_VALOR_LIQUIDO}
        CodMotivoNfseSubstituida={(nota.NR_CHAVE_NFSE_SUBSTITUIDA > 0 ? nota.NR_CHAVE_NFSE_SUBSTITUIDA : string.Empty)}
        
        {Prestador(nota)}

        {Tomador(nota)}

        {/* IMÓVEL (não obrigatório) ============================= */      string.Empty}
        {/* DESTINATARIO (não obrigatório) ============================= */      string.Empty}

        {Servico(nota)}

        {/* OBRA (não obrigatório) ============================= */      string.Empty}
        {/* EVENTO (não obrigatório) ============================= */      string.Empty}

        {IBSCBS(nota)}

        {(nota.IN_TEM_DEDUCOES ? Deducao() : string.Empty)}
        {(nota.IN_TEM_DOCUMENTOS_REFERENCIADOS ? DocumentosReferenciados() : string.Empty)}

        SALVARDPS
    """;

    public string Prestador(NFSeNacional nota) => $"""
        {/* PRESTADOR ============================= */      string.Empty}
        CpfCnpjPrestador={nota.OBJ_PRESTADOR.TX_CNPJ}
        InscricaoMunicipalPrestador={nota.OBJ_PRESTADOR.TX_INSCRICAO_MUNICIPAL}
        RazaoSocialPrestador={nota.OBJ_PRESTADOR.TX_RAZAO_SOCIAL}
        CodigoCidadePrestador={nota.OBJ_PRESTADOR.TX_CODIGO_CIDADE}
        CepPrestador={nota.OBJ_LOCALIDADE.TX_CEP}
        EnderecoPrestador={nota.OBJ_LOCALIDADE.TX_ENDERECO}
        NumeroPrestador={nota.OBJ_LOCALIDADE.NR_NUMERO}
        ComplementoPrestador={string.Empty}
        BairroPrestador={nota.OBJ_LOCALIDADE.TX_BAIRRO}
        TelefonePrestador={nota.OBJ_PRESTADOR.TX_TELEFONE}
        EmailPrestador={nota.OBJ_PRESTADOR.TX_EMAIL}
        OptanteSimplesNacional={(nota.OBJ_PRESTADOR.IN_EH_OPTANTE_SIMPLES_NACIONAL ? (nota.OBJ_PRESTADOR.IN_EH_ME_EPP ? 3 : (nota.OBJ_PRESTADOR.IN_EH_MEI ? 2 : 0)) : 1)}
    """;

    public string Tomador(NFSeNacional nota) => $"""
        {/* TOMADOR ============================= */      string.Empty}
        CpfCnpjTomador={nota.OBJ_TOMADOR.TX_CNPJ}
        InscricaoMunicipalTomador={nota.OBJ_TOMADOR.TX_INSCRICAO_MUNICIPAL}
        RazaoSocialTomador={nota.OBJ_TOMADOR.TX_RAZAO_SOCIAL}
        CodigoCidadeTomador={nota.OBJ_TOMADOR.TX_CODIGO_CIDADE}
        CepTomador={nota.OBJ_TOMADOR.OBJ_LOCALIDADE.TX_CEP}
        EnderecoTomador={nota.OBJ_TOMADOR.OBJ_LOCALIDADE.TX_ENDERECO}
        NumeroTomador={nota.OBJ_TOMADOR.OBJ_LOCALIDADE.NR_NUMERO}
        ComplementoTomador={string.Empty}
        BairroTomador={nota.OBJ_TOMADOR.OBJ_LOCALIDADE.TX_BAIRRO}
        TelefoneTomador={nota.OBJ_TOMADOR.TX_TELEFONE}
        EmailTomador={nota.OBJ_TOMADOR.TX_EMAIL}        
    """;

    public string Servico(NFSeNacional nota) => $"""
        {/* SERVICO ============================= */      string.Empty}
        CodigoItemListaServico={nota.OBJ_SERVICO.TX_CODIGO_ITEM_LISTA_SERVICO}
        CodigoTributacaoMunicipio={nota.OBJ_SERVICO.TX_CODIGO_TRIBUTACAO_MUNICIPIO}
        DiscriminacaoServico={nota.OBJ_SERVICO.TX_DISCRIMINACAO_SERVICO}
        CodigoNbs={nota.OBJ_SERVICO.TX_CODIGO_NBS}

        {/* VALORES ============================= */      string.Empty}
        ValorServicos={nota.OBJ_SERVICO.VL_VALOR_SERVICOS.ToString("F2", CultureInfo.InvariantCulture)}
        ValorDeducoes={nota.OBJ_SERVICO.VL_VALOR_DEDUCOES.ToString("F2", CultureInfo.InvariantCulture)}
        ValorPercDeducoes={nota.OBJ_SERVICO.VL_VALOR_PERC_DEDUCOES.ToString("F2", CultureInfo.InvariantCulture)}
        AliquotaISS={nota.OBJ_SERVICO.VL_ALIQUOTA_ISS.ToString("F2", CultureInfo.InvariantCulture)}
        ValorIss={nota.OBJ_SERVICO.VL_VALOR_ISS.ToString("F2", CultureInfo.InvariantCulture)}
        ValorPis={nota.OBJ_SERVICO.VL_VALOR_PIS.ToString("F2", CultureInfo.InvariantCulture)}
        ValorCofins={nota.OBJ_SERVICO.VL_VALOR_COFINS.ToString("F2", CultureInfo.InvariantCulture)}
        ValorIRRF={nota.OBJ_SERVICO.VL_VALOR_IRRF.ToString("F2", CultureInfo.InvariantCulture)}
        ValorCSLL={nota.OBJ_SERVICO.VL_VALOR_CSLL.ToString("F2", CultureInfo.InvariantCulture)}
        ValorTotalTribFed={nota.OBJ_SERVICO.VL_VALOR_TOTAL_TRIB_FED.ToString("F2", CultureInfo.InvariantCulture)}
        ValorTotalTribEst={nota.OBJ_SERVICO.VL_VALOR_TOTAL_TRIB_EST.ToString("F2", CultureInfo.InvariantCulture)}
        ValorTotalTribMun={nota.OBJ_SERVICO.VL_VALOR_TOTAL_TRIB_MUN.ToString("F2", CultureInfo.InvariantCulture)}
    """;

    public string IBSCBS(NFSeNacional nota) => $"""
        {/* IBS/CBS ============================= */      string.Empty}
        SituacaoTributariaIbsCbs={nota.OBJ_SERVICO.TX_SITUACAO_TRIBUTARIA_IBS_CBS}
        ClassificacaoTributariaIbsCbs={nota.OBJ_SERVICO.TX_CLASSIFICACAO_TRIBUTARIA_IBS_CBS}
        BaseCalculoIbsCbs={nota.OBJ_SERVICO.VL_BASE_CALCULO_IBS_CBS.ToString("F2", CultureInfo.InvariantCulture)}
        AliquotaIbsEstadual={nota.OBJ_SERVICO.VL_ALIQUOTA_IBS_ESTADUAL.ToString("F2", CultureInfo.InvariantCulture)}
        AliquotaIbsMunicipal={nota.OBJ_SERVICO.VL_ALIQUOTA_IBS_MUNICIPAL.ToString("F2", CultureInfo.InvariantCulture)}
        AliquotaCbs={nota.OBJ_SERVICO.VL_ALIQUOTA_CBS.ToString("F2", CultureInfo.InvariantCulture)}
        ValorTotalNfIbsCbs={nota.OBJ_SERVICO.VL_VALOR_TOTAL_NF_IBS_CBS.ToString("F2", CultureInfo.InvariantCulture)}
        ValorCbs={nota.OBJ_SERVICO.VL_VALOR_CBS.ToString("F2", CultureInfo.InvariantCulture)}
    """;

    string Deducao()
    {
        throw new NotImplementedException();
    }

    string iNFSeBuilder.Deducao()
    {
        return Deducao();
    }

    string DocumentosReferenciados()
    {
        throw new NotImplementedException();
    }

    string iNFSeBuilder.DocumentosReferenciados()
    {
        return DocumentosReferenciados();
    }
}