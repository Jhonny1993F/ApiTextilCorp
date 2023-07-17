using Microsoft.EntityFrameworkCore;
using ApiTextilCorp.Data;
using ApiTextilCorp.Data.Models;
namespace ApiTextilCorp.Controllers;

public static class VentaEndpoints
{
    public static void MapVentaEndpoints (this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/Venta", async (TextilCorpDataContext db) =>
        {
            return await db.Ventas.ToListAsync();
        })
        .WithName("GetAllVentas")
        .Produces<List<Venta>>(StatusCodes.Status200OK);

        routes.MapGet("/api/Venta/{id}", async (int VentasId, TextilCorpDataContext db) =>
        {
            return await db.Ventas.FindAsync(VentasId)
                is Venta model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetVentaById")
        .Produces<Venta>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        routes.MapPut("/api/Venta/{id}", async (int VentasId, Venta venta, TextilCorpDataContext db) =>
        {
            var foundModel = await db.Ventas.FindAsync(VentasId);

            if (foundModel is null)
            {
                return Results.NotFound();
            }

            db.Update(venta);

            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("UpdateVenta")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        routes.MapPost("/api/Venta/", async (Venta venta, TextilCorpDataContext db) =>
        {
            db.Ventas.Add(venta);
            await db.SaveChangesAsync();
            return Results.Created($"/Ventas/{venta.VentasId}", venta);
        })
        .WithName("CreateVenta")
        .Produces<Venta>(StatusCodes.Status201Created);

        routes.MapDelete("/api/Venta/{id}", async (int VentasId, TextilCorpDataContext db) =>
        {
            if (await db.Ventas.FindAsync(VentasId) is Venta venta)
            {
                db.Ventas.Remove(venta);
                await db.SaveChangesAsync();
                return Results.Ok(venta);
            }

            return Results.NotFound();
        })
        .WithName("DeleteVenta")
        .Produces<Venta>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
