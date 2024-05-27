using System.ComponentModel.DataAnnotations;

namespace Data.EFCore.Models;

public class Pessoa
{
    [Key] public int Id { get; set; }
    public string Nome { get; set; }
    public int Idade { get; set; }
}