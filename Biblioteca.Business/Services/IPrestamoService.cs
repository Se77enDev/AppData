using Biblioteca.Entities.Models;

namespace Biblioteca.Business.Interfaces;
public interface IPrestamoService
{
    Task<Prestamo> RegistrarPrestamoAsync(int usuarioId, int libroId);
    Task<bool> DevolverLibroAsync(int prestamoId);
    Task<int> ObtenerPrestamosActivosAsync(int usuarioId);
}