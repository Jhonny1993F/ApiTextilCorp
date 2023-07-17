using Microsoft.EntityFrameworkCore;
using ApiTextilCorp.Data;
using ApiTextilCorp.Data.Models;
namespace ApiTextilCorp.Controllers;

public static class ProductoEndpoints
{
    public static void MapProductoEndpoints (this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/Producto", async (TextilCorpDataContext db) =>
        {
            return await db.Productos.ToListAsync();
        })
        .WithName("GetAllProductos")
        .Produces<List<Producto>>(StatusCodes.Status200OK);

        routes.MapGet("/api/Producto/{id}", async (int ProductosId, TextilCorpDataContext db) =>
        {
            return await db.Productos.FindAsync(ProductosId)
                is Producto model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetProductoById")
        .Produces<Producto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        routes.MapPut("/api/Producto/{id}", async (int ProductosId, Producto producto, TextilCorpDataContext db) =>
        {
            var foundModel = await db.Productos.FindAsync(ProductosId);

            if (foundModel is null)
            {
                return Results.NotFound();
            }

            db.Update(producto);

            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("UpdateProducto")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        routes.MapPost("/api/Producto/", async (Producto producto, TextilCorpDataContext db) =>
        {
            db.Productos.Add(producto);
            await db.SaveChangesAsync();
            return Results.Created($"/Productos/{producto.ProductosId}", producto);
        })
        .WithName("CreateProducto")
        .Produces<Producto>(StatusCodes.Status201Created);

        routes.MapDelete("/api/Producto/{id}", async (int ProductosId, TextilCorpDataContext db) =>
        {
            if (await db.Productos.FindAsync(ProductosId) is Producto producto)
            {
                db.Productos.Remove(producto);
                await db.SaveChangesAsync();
                return Results.Ok(producto);
            }

            return Results.NotFound();
        })
        .WithName("DeleteProducto")
        .Produces<Producto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
