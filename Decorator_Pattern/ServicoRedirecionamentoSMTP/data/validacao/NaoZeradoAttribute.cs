using System.ComponentModel.DataAnnotations;

public class NaoZeradoAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if(value == null) return false;
        return Convert.ToDecimal(value) != 0;
    }
}