using Biblioteca.Business.Interfaces;
using Biblioteca.Presentation.DTOs;

namespace Biblioteca.Presentation.Endpoints;
public static class PrestamosEndpoints
{
    public static void MapPrestamosEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/prestamos").WithTags("Préstamos");

        group.MapPost("/", async (IPrestamoService service, PrestamoRequest request) =>
        {
            try
            {
                var prestamo = await service.RegistrarPrestamoAsync(request.UsuarioId, request.LibroId);
                return Results.Created($"/api/prestamos/{prestamo.Id}", prestamo);
            }
            catch (Exception ex) when (ex is KeyNotFoundException or InvalidOperationException)
            {
                return Results.BadRequest(ex.Message);
            }
        });

        group.MapPost("/{id}/devolver", async (IPrestamoService service, int id) =>
        {
            var exito = await service.DevolverLibroAsync(id);
            return exito ? Results.Ok() : Results.NotFound();
        });
    }
}