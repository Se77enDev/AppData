using Biblioteca.Business.Services;
using Biblioteca.Presentation.DTOs;

namespace Biblioteca.Presentation.Endpoints
{
    public static class UsuariosEndpoints
    {
        public static void MapUsuariosEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/usuarios").WithTags("Usuarios");

            group.MapPost("/", async (IUsuarioService service, UsuarioRequest request) =>
            {
                try
                {
                    var usuario = await service.RegistrarUsuarioAsync(request.Nombre, request.Tipo);
                    return Results.Created($"/api/usuarios/{usuario.Id}", usuario);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(ex.Message);
                }
                catch (InvalidOperationException ex)
                {
                    return Results.Conflict(ex.Message);
                }
            });

            group.MapGet("/", async (IUsuarioService service) =>
            {
                var usuarios = await service.ObtenerTodosUsuariosAsync();
                return Results.Ok(usuarios);
            });

            group.MapGet("/{id}", async (IUsuarioService service, int id) =>
            {
                var usuario = await service.ObtenerUsuarioPorIdAsync(id);
                return usuario != null ? Results.Ok(usuario) : Results.NotFound();
            });

            group.MapGet("/tipo/{tipo}", async (IUsuarioService service, string tipo) =>
            {
                var usuarios = await service.ObtenerUsuariosPorTipoAsync(tipo);
                return Results.Ok(usuarios);
            });

            group.MapGet("/existe/{nombre}", async (IUsuarioService service, string nombre) =>
            {
                var existe = await service.ExisteUsuarioAsync(nombre);
                return Results.Ok(new { Existe = existe });
            });
        }
    }
}
