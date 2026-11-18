using System.ComponentModel.DataAnnotations;
using System.Data.Common;

public class Telefone
{
    [MaxLength(3)]
    public string DDD { get; set; } = "099";

    [MaxLength(9)]
    public string Numero { get; set; } = "984621573";
}