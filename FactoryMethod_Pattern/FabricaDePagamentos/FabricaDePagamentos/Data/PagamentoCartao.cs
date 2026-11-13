public class PagamentoCartao : iProduct
{
    public string orderId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public string payment_type_id
    {
        get => "credit_card";
        set
        {
            if (value != "credit_card") throw new Exception("");
        }
    }
    public string payment_method_id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public iPayer payer { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public string token { get; set; }
    public int installments { get; set; }

    public PagamentoCartao(PagamentoCartao _product)
    {
        try
        {
            orderId = _product.orderId;
            payment_method_id = _product.payment_method_id;
            payment_type_id = _product.payment_type_id;
            token = _product.token;
            installments = _product.installments;

            if (!string.IsNullOrEmpty(payer.email))
                payer = new Pagador();
            else
            {
                throw new Exception("Erro na construção do objeto de pagamento(PagamentoCartao).");
                // log...
            }
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(string.Join("Erro ao concretizar o pagamento via cartão de crédito.", ex.Message));
            // log...
        }
    }

    void EfetuarValidacoes()
    {
        // Validações específicas sobre cartão de crédito....
        // Taxas de juros conforme parcelamento
        // Validação de formato de pagamento cadastrado
        // Validações de possíveis inadimplências do cliente
    }

    void iProduct.ObjectChecks()
    {
        EfetuarValidacoes();
    }
}