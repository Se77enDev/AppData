using Biblioteca.Entities.Models;

namespace Biblioteca.Business.Interfaces;

public interface ILibroService
{
    Task<Libro> RegistrarLibroAsync(string titulo, string autor, int año, string categoria);
    Task<IEnumerable<Libro>> ObtenerTodosLibrosAsync();
    Task<bool> ConsultarDisponibilidadAsync(int libroId);
    Task<IEnumerable<Libro>> ObtenerLibrosPorCategoriaAsync(string categoria);
}