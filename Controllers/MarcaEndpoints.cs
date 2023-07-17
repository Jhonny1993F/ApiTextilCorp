using Microsoft.EntityFrameworkCore;
using ApiTextilCorp.Data;
using ApiTextilCorp.Data.Models;
namespace ApiTextilCorp.Controllers;

public static class MarcaEndpoints
{
    public static void MapMarcaEndpoints (this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/Marca", async (TextilCorpDataContext db) =>
        {
            return await db.Marcas.ToListAsync();
        })
        .WithName("GetAllMarcas")
        .Produces<List<Marca>>(StatusCodes.Status200OK);

        routes.MapGet("/api/Marca/{id}", async (int MarcasId, TextilCorpDataContext db) =>
        {
            return await db.Marcas.FindAsync(MarcasId)
                is Marca model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetMarcaById")
        .Produces<Marca>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        routes.MapPut("/api/Marca/{id}", async (int MarcasId, Marca marca, TextilCorpDataContext db) =>
        {
            var foundModel = await db.Marcas.FindAsync(MarcasId);

            if (foundModel is null)
            {
                return Results.NotFound();
            }

            db.Update(marca);

            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("UpdateMarca")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        routes.MapPost("/api/Marca/", async (Marca marca, TextilCorpDataContext db) =>
        {
            db.Marcas.Add(marca);
            await db.SaveChangesAsync();
            return Results.Created($"/Marcas/{marca.MarcasId}", marca);
        })
        .WithName("CreateMarca")
        .Produces<Marca>(StatusCodes.Status201Created);

        routes.MapDelete("/api/Marca/{id}", async (int MarcasId, TextilCorpDataContext db) =>
        {
            if (await db.Marcas.FindAsync(MarcasId) is Marca marca)
            {
                db.Marcas.Remove(marca);
                await db.SaveChangesAsync();
                return Results.Ok(marca);
            }

            return Results.NotFound();
        })
        .WithName("DeleteMarca")
        .Produces<Marca>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
