using ApiChaRifaYan.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiChaRifaYan.Data;

public class ParticipanteContext : IEntityTypeConfiguration<Participante>
{
    public void Configure(EntityTypeBuilder<Participante> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Nome)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.DataHoraCadastro)
            .HasDefaultValueSql("NOW()");
    }
}
