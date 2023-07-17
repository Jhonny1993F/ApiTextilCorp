using Microsoft.EntityFrameworkCore;
using ApiTextilCorp.Data;
using ApiTextilCorp.Data.Models;
namespace ApiTextilCorp.Controllers;

public static class ClienteEndpoints
{
    public static void MapClienteEndpoints (this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/Cliente", async (TextilCorpDataContext db) =>
        {
            return await db.Clientes.ToListAsync();
        })
        .WithName("GetAllClientes")
        .Produces<List<Cliente>>(StatusCodes.Status200OK);

        routes.MapGet("/api/Cliente/{id}", async (int ClientesId, TextilCorpDataContext db) =>
        {
            return await db.Clientes.FindAsync(ClientesId)
                is Cliente model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetClienteById")
        .Produces<Cliente>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        routes.MapPut("/api/Cliente/{id}", async (int ClientesId, Cliente cliente, TextilCorpDataContext db) =>
        {
            var foundModel = await db.Clientes.FindAsync(ClientesId);

            if (foundModel is null)
            {
                return Results.NotFound();
            }

            db.Update(cliente);

            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("UpdateCliente")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        routes.MapPost("/api/Cliente/", async (Cliente cliente, TextilCorpDataContext db) =>
        {
            db.Clientes.Add(cliente);
            await db.SaveChangesAsync();
            return Results.Created($"/Clientes/{cliente.ClientesId}", cliente);
        })
        .WithName("CreateCliente")
        .Produces<Cliente>(StatusCodes.Status201Created);

        routes.MapDelete("/api/Cliente/{id}", async (int ClientesId, TextilCorpDataContext db) =>
        {
            if (await db.Clientes.FindAsync(ClientesId) is Cliente cliente)
            {
                db.Clientes.Remove(cliente);
                await db.SaveChangesAsync();
                return Results.Ok(cliente);
            }

            return Results.NotFound();
        })
        .WithName("DeleteCliente")
        .Produces<Cliente>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
