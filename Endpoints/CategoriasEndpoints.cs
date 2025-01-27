using CatalogoAPI.Context;
using CatalogoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogoAPI.Endpoints;

public static class CategoriasEndpoints
{
    public static void MapCategoriasEndpoints(this WebApplication app)
    {

        app.MapPost("/categorias", async (Categoria categoria, AppDbContext db) =>
        {
            db.Categorias.Add(categoria);
            await db.SaveChangesAsync();

            return Results.Created($"/categorias/{categoria.CategoriaId}", categoria);
        });

        app.MapGet("/categorias", async (AppDbContext db) => await db.Categorias.ToListAsync()).WithTags("Categorias").RequireAuthorization();

        app.MapGet("/categorias{id:int}", async (int id, AppDbContext db) =>
        {
            return await db.Categorias.FindAsync(id) is Categoria categoria ? Results.Ok(categoria) : Results.NotFound();
        }
        );

        app.MapPut("/categorias{id:int}", async (int id, Categoria categoria, AppDbContext db) =>
        {
            if (categoria.CategoriaId != id) return Results.BadRequest();

            var categoriaDb = await db.Categorias.FindAsync(id);

            if (categoriaDb is null) return Results.NotFound();

            categoriaDb.Nome = categoria.Nome;
            categoriaDb.Descricao = categoria.Descricao;

            db.Categorias.Update(categoriaDb);
            await db.SaveChangesAsync();

            return Results.Ok(categoriaDb);
        });

        app.MapDelete("/categorias{id:int}", async (int id, AppDbContext db) =>
        {
            var deleted = await db.Categorias.FindAsync(id);

            if (deleted is null) return Results.NotFound();

            db.Categorias.Remove(deleted);
            await db.SaveChangesAsync();

            return Results.Ok(deleted);
        });

    }
}
