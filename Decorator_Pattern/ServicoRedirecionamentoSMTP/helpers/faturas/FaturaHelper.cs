using System.Reflection.Metadata.Ecma335;

public static class FaturaHelper
{
    public static bool DocumentoFaturaEnviado(long idFatura)
    {
        // lógica de acesso à fila de gerenciamento de envio externa            
        var aleatorio = new Random().Next(1,100)%2 == 0;
        return aleatorio;        
    }
}