using Biblioteca.Business.Interfaces;
using Biblioteca.Presentation.DTOs;

namespace Biblioteca.Presentation.Endpoints
{
     public static class LibrosEndpoints
    {
        public static void MapLibrosEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/libros").WithTags("Libros");

            group.MapPost("/", async (ILibroService service, LibroRequest request) =>
            {
                try
                {
                    var libro = await service.RegistrarLibroAsync(
                        request.Titulo,
                        request.Autor,
                        request.Año,
                        request.Categoria);

                    return Results.Created($"/api/libros/{libro.Id}", libro);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            });

            group.MapGet("/", async (ILibroService service) =>
            {
                var libros = await service.ObtenerTodosLibrosAsync();
                return Results.Ok(libros);
            });

            group.MapGet("/{id}/disponibilidad", async (ILibroService service, int id) =>
            {
                var disponible = await service.ConsultarDisponibilidadAsync(id);
                return Results.Ok(new { Disponible = disponible });
            });

            group.MapGet("/categoria/{categoria}", async (ILibroService service, string categoria) =>
            {
                var libros = await service.ObtenerLibrosPorCategoriaAsync(categoria);
                return Results.Ok(libros);
            });
        }
    }

}

