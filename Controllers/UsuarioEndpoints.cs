using Microsoft.EntityFrameworkCore;
using ApiTextilCorp.Data;
using ApiTextilCorp.Data.Models;
namespace ApiTextilCorp.Controllers;

public static class UsuarioEndpoints
{
    public static void MapUsuarioEndpoints (this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/Usuario", async (TextilCorpDataContext db) =>
        {
            return await db.Usuarios.ToListAsync();
        })
        .WithName("GetAllUsuarios")
        .Produces<List<Usuario>>(StatusCodes.Status200OK);

        routes.MapGet("/api/Usuario/{id}", async (int UsuariosId, TextilCorpDataContext db) =>
        {
            return await db.Usuarios.FindAsync(UsuariosId)
                is Usuario model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetUsuarioById")
        .Produces<Usuario>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        routes.MapPut("/api/Usuario/{id}", async (int UsuariosId, Usuario usuario, TextilCorpDataContext db) =>
        {
            var foundModel = await db.Usuarios.FindAsync(UsuariosId);

            if (foundModel is null)
            {
                return Results.NotFound();
            }

            db.Update(usuario);

            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("UpdateUsuario")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        routes.MapPost("/api/Usuario/", async (Usuario usuario, TextilCorpDataContext db) =>
        {
            db.Usuarios.Add(usuario);
            await db.SaveChangesAsync();
            return Results.Created($"/Usuarios/{usuario.UsuariosId}", usuario);
        })
        .WithName("CreateUsuario")
        .Produces<Usuario>(StatusCodes.Status201Created);

        routes.MapDelete("/api/Usuario/{id}", async (int UsuariosId, TextilCorpDataContext db) =>
        {
            if (await db.Usuarios.FindAsync(UsuariosId) is Usuario usuario)
            {
                db.Usuarios.Remove(usuario);
                await db.SaveChangesAsync();
                return Results.Ok(usuario);
            }

            return Results.NotFound();
        })
        .WithName("DeleteUsuario")
        .Produces<Usuario>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
