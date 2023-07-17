using Microsoft.EntityFrameworkCore;
using ApiTextilCorp.Data;
using ApiTextilCorp.Data.Models;
namespace ApiTextilCorp.Controllers;

public static class CategoriaEndpoints
{
    public static void MapCategoriaEndpoints (this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/Categoria", async (TextilCorpDataContext db) =>
        {
            return await db.Categorias.ToListAsync();
        })
        .WithName("GetAllCategorias")
        .Produces<List<Categoria>>(StatusCodes.Status200OK);

        routes.MapGet("/api/Categoria/{id}", async (int CategoriasId, TextilCorpDataContext db) =>
        {
            return await db.Categorias.FindAsync(CategoriasId)
                is Categoria model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetCategoriaById")
        .Produces<Categoria>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        routes.MapPut("/api/Categoria/{id}", async (int CategoriasId, Categoria categoria, TextilCorpDataContext db) =>
        {
            var foundModel = await db.Categorias.FindAsync(CategoriasId);

            if (foundModel is null)
            {
                return Results.NotFound();
            }

            db.Update(categoria);

            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("UpdateCategoria")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        routes.MapPost("/api/Categoria/", async (Categoria categoria, TextilCorpDataContext db) =>
        {
            db.Categorias.Add(categoria);
            await db.SaveChangesAsync();
            return Results.Created($"/Categorias/{categoria.CategoriasId}", categoria);
        })
        .WithName("CreateCategoria")
        .Produces<Categoria>(StatusCodes.Status201Created);

        routes.MapDelete("/api/Categoria/{id}", async (int CategoriasId, TextilCorpDataContext db) =>
        {
            if (await db.Categorias.FindAsync(CategoriasId) is Categoria categoria)
            {
                db.Categorias.Remove(categoria);
                await db.SaveChangesAsync();
                return Results.Ok(categoria);
            }

            return Results.NotFound();
        })
        .WithName("DeleteCategoria")
        .Produces<Categoria>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
