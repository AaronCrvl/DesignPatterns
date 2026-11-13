public interface iProduct
{
    string orderId { get; set; }
    string payment_method_id { get; set; }
    string payment_type_id { get; set; }
    iPayer payer { get; set; }    
    void ObjectChecks();
}

public interface iPayer
{
    string email { get; set; }
    iIdentification identification { get; set; }
}

public interface iIdentification
{
    string type { get; set; }
    string number { get; set; }
}