using CatalogoAPI.Context;
using CatalogoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogoAPI.Endpoints;

public static class ProdutosEndpoints
{
    public static void MapProdutosEndpoints(this WebApplication app)
    {
        app.MapPost("/produtos", async (Produto produto, AppDbContext db) =>
        {
            db.Produtos.Add(produto);
            await db.SaveChangesAsync();

            return Results.Created($"/categorias/{produto.ProdutoId}", produto);
        });

        app.MapGet("/produtos", async (AppDbContext db) => await db.Produtos.ToListAsync()).WithTags("Produtos").RequireAuthorization();

        app.MapGet("/produtos{id:int}", async (int id, AppDbContext db) =>
        {
            return await db.Produtos.FindAsync(id) is Produto produto ? Results.Ok(produto) : Results.NotFound();
        });

        app.MapPut("/produtos{id:int}", async (int id, Produto produto, AppDbContext db) =>
        {
            if (produto.ProdutoId != id) return Results.BadRequest();

            var produtoDb = await db.Produtos.FindAsync(id);

            if (produtoDb is null) return Results.NotFound();

            produtoDb.Nome = produto.Nome;
            produtoDb.Descricao = produto.Descricao;
            produtoDb.Preco = produto.Preco;
            produtoDb.DataCompra = produto.DataCompra;
            produtoDb.CategoriaId = produto.CategoriaId;
            produtoDb.Estoque = produto.Estoque;

            db.Produtos.Update(produtoDb);
            await db.SaveChangesAsync();

            return Results.Ok(produtoDb);
        });

        app.MapDelete("/produtos{id:int}", async (int id, AppDbContext db) =>
        {
            var deleted = await db.Produtos.FindAsync(id);

            if (deleted is null) return Results.NotFound();

            db.Produtos.Remove(deleted);
            await db.SaveChangesAsync();

            return Results.Ok(deleted);
        });

    }
}
