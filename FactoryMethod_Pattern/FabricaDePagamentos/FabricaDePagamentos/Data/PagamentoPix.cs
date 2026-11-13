public class PagamentoPix : iProduct
{
    public string orderId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public string payment_type_id
    {
        get => "pix";
        set
        {
            if (value != "pix") throw new Exception("");
        }
    }
    public string payment_method_id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public iPayer payer { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public PagamentoPix(PagamentoPix _product)
    {
        try
        {            
            orderId = _product.orderId;
            payment_method_id = _product.payment_method_id;
            payment_type_id = _product.payment_type_id;

            if (!string.IsNullOrEmpty(payer.email))
                payer = new Pagador();
             else
            {
                throw new Exception("Erro na construção do objeto de pagamento(PagamentoPix).");
                // log...
            }
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(string.Join("Erro ao concretizar o pagamento via Pix.", ex.Message));
            // log...
        }
    }

    void EfetuarValidacoes()
    {
        // Validações específicas sobre pix....
        // Possíveis descontos para o modelo de pagamento
        // Validação de formato de pagamento cadastrado
        // Validações de possíveis inadimplências do cliente
    }

    void iProduct.ObjectChecks()
    {
        EfetuarValidacoes();
    }    
}