using ApiChaRifaYan.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiChaRifaYan.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Participante> Participante => Set<Participante>();
    public DbSet<Sorteio> Sorteio => Set<Sorteio>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ParticipanteContext());
        modelBuilder.ApplyConfiguration(new SorteioContext());
    }
}
