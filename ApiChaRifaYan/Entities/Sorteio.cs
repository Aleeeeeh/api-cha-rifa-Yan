using System.ComponentModel.DataAnnotations;

namespace ApiChaRifaYan.Entities;

public class Sorteio
{
    [Key]
    public int Id { get; set; }
    public int Numero { get; set; }
    public int? ParticipanteId { get; set; }
    public Participante? Participante { get; set; }
    public DateTime DataHora { get; set; }
    public bool FoiEscolhido { get; set; }
}
