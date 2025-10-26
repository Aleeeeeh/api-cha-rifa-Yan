using ApiChaRifaYan.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiChaRifaYan.Data;

public class SorteioContext : IEntityTypeConfiguration<Sorteio>
{
    public void Configure(EntityTypeBuilder<Sorteio> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Numero)
            .IsRequired();

        builder.Property(s => s.DataHora)
            .HasDefaultValue(DateTime.UtcNow);

        builder.Property(s => s.FoiEscolhido)
            .HasDefaultValue(false)
            .IsRequired();

        builder.HasOne(s => s.Participante)
            .WithMany()
            .HasForeignKey(s => s.ParticipanteId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
