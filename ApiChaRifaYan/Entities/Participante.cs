using System.ComponentModel.DataAnnotations;

namespace ApiChaRifaYan.Entities;

public class Participante
{
    [Key]
    public int Id { get; set; }
    public string Nome { get; set; } = null!;
    public DateTime DataHoraCadastro { get; set; }
}
