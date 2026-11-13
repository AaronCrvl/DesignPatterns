public class Pagador : iPayer
{
    public string email { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public iIdentification identification { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public Pagador()
    {
        
    }
}

public class Identificacao : iIdentification
{
    public string type { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public string number { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public Identificacao()
    {
        
    }
}