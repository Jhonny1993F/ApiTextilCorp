using Microsoft.EntityFrameworkCore;
using ApiTextilCorp.Data;
using ApiTextilCorp.Data.Models;
namespace ApiTextilCorp.Controllers;

public static class CarritoEndpoints
{
    public static void MapCarritoEndpoints (this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/Carrito", async (TextilCorpDataContext db) =>
        {
            return await db.Carritos.ToListAsync();
        })
        .WithName("GetAllCarritos")
        .Produces<List<Carrito>>(StatusCodes.Status200OK);

        routes.MapGet("/api/Carrito/{id}", async (int CarritoId, TextilCorpDataContext db) =>
        {
            return await db.Carritos.FindAsync(CarritoId)
                is Carrito model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetCarritoById")
        .Produces<Carrito>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        routes.MapPut("/api/Carrito/{id}", async (int CarritoId, Carrito carrito, TextilCorpDataContext db) =>
        {
            var foundModel = await db.Carritos.FindAsync(CarritoId);

            if (foundModel is null)
            {
                return Results.NotFound();
            }

            db.Update(carrito);

            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("UpdateCarrito")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        routes.MapPost("/api/Carrito/", async (Carrito carrito, TextilCorpDataContext db) =>
        {
            db.Carritos.Add(carrito);
            await db.SaveChangesAsync();
            return Results.Created($"/Carritos/{carrito.CarritoId}", carrito);
        })
        .WithName("CreateCarrito")
        .Produces<Carrito>(StatusCodes.Status201Created);

        routes.MapDelete("/api/Carrito/{id}", async (int CarritoId, TextilCorpDataContext db) =>
        {
            if (await db.Carritos.FindAsync(CarritoId) is Carrito carrito)
            {
                db.Carritos.Remove(carrito);
                await db.SaveChangesAsync();
                return Results.Ok(carrito);
            }

            return Results.NotFound();
        })
        .WithName("DeleteCarrito")
        .Produces<Carrito>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
