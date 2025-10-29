using ApiChaRifaYan.Data;
using ApiChaRifaYan.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173")
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
});

var app = builder.Build();

app.UseCors("AllowSpecificOrigins");

app.MapPost("/chaRifa/participante/{nomeParticipante}", async ([FromRoute] string nomeParticipante, AppDbContext db) =>
{
    try
    {
        Participante participante = new()
        {
            Nome = nomeParticipante
        };

        db.Participante.Add(participante);
        await db.SaveChangesAsync();

        return Results.Created($"/chaRifa/participante/{participante.Id}", participante);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.MapGet("/chaRifa/participante", async (AppDbContext db) =>
{
    return await db.Participante.ToListAsync();
});

app.MapPost("/chaRifa/sorteio", async (AppDbContext db) =>
{
    try
    {
        List<Sorteio> listaSorteio = await db.Sorteio.ToListAsync();

        if (listaSorteio.Count > 0)
            return Results.Conflict(new { response = "Os números do sorteio já foram criados." });

        List<Sorteio> sorteios = Enumerable.Range(1, 50).Select(numero => new Sorteio
        {
            Numero = numero
        }).ToList();

        await db.Sorteio.AddRangeAsync(sorteios);
        await db.SaveChangesAsync();

        return Results.Created("/chaRifa/sorteio", sorteios);
    }
    catch (Exception ex)
    {
        return Results.Problem($"Erro ao gravar sorteios: {ex.Message}");
    }
});

app.MapPut("/chaRifa/sorteio", async (SorteioParticipanteDto dto, AppDbContext db) =>
{
    try
    {
        Sorteio? sorteio = await db.Sorteio
                        .Include(s => s.Participante)
                        .FirstOrDefaultAsync(s => s.Numero == dto.Numero);

        if (sorteio is null)
        {
            return Results.NotFound($"Número {dto.Numero} não existe no sorteio.");
        }

        if (sorteio.FoiEscolhido)
            return Results.Conflict(new { response = $"Número {dto.Numero} já foi marcado pelo participante {sorteio.Participante?.Nome}" });

        sorteio.ParticipanteId = dto.ParticipanteId;
        sorteio.FoiEscolhido = true;
        await db.SaveChangesAsync();

        return Results.Ok(new { response = $"Número {dto.Numero} marcado com sucesso" });
    }
    catch (Exception ex)
    {
        return Results.Problem($"Erro ao marcar número: {ex.Message}");
    }
});

app.MapGet("/chaRifa/sorteio", async (AppDbContext db) =>
{
    return await db.Sorteio
        .Include(s => s.Participante)
        .OrderBy(s => s.Numero)
        .ToListAsync();
});

app.MapGet("/chaRifa/sorteio/numero/{numero}", async ([FromRoute] int numero, AppDbContext db) =>
{
    return await db.Sorteio
    .Include(s => s.Participante)
    .Where(s => s.Numero == numero)
    .Select(s => new { s.Participante })
    .FirstOrDefaultAsync();
});

app.Run();
