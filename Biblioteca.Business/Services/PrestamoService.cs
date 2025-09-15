using Biblioteca.Business.Interfaces;
using Biblioteca.Entities.Models;
using Biblioteca.Data.Interfaces;

namespace Biblioteca.Business.Services
{
    public class PrestamoService : IPrestamoService
    {
        private readonly IPrestamoRepository _prestamoRepository;
        private readonly ILibroRepository _libroRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public PrestamoService(
            IPrestamoRepository prestamoRepository,
            ILibroRepository libroRepository,
            IUsuarioRepository usuarioRepository)
        {
            _prestamoRepository = prestamoRepository;
            _libroRepository = libroRepository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Prestamo> RegistrarPrestamoAsync(int usuarioId, int libroId)
        {
            // Validar libro
            var libro = await _libroRepository.GetByIdAsync(libroId);
            if (libro == null)
                throw new KeyNotFoundException("Libro no encontrado");

            if (!libro.Disponible)
                throw new InvalidOperationException("Libro no disponible");

            // Validar usuario
            var usuario = await _usuarioRepository.GetByIdAsync(usuarioId);
            if (usuario == null)
                throw new KeyNotFoundException("Usuario no encontrado");

            // Validar límite de préstamos
            var prestamosActivos = await _prestamoRepository.GetActiveLoansCountAsync(usuarioId);
            if (prestamosActivos >= 3)
                throw new InvalidOperationException("Límite de préstamos alcanzado (máximo 3)");

            // Crear préstamo
            var prestamo = new Prestamo
            {
                UsuarioId = usuarioId,
                LibroId = libroId,
                FechaPrestamo = DateTime.UtcNow
            };

            await _prestamoRepository.AddAsync(prestamo);

            // Actualizar disponibilidad del libro
            libro.Disponible = false;
            await _libroRepository.UpdateAsync(libro);

            return prestamo;
        }

        public async Task<bool> DevolverLibroAsync(int prestamoId)
        {
            var prestamo = await _prestamoRepository.GetByIdAsync(prestamoId);
            if (prestamo == null || prestamo.Devuelto)
                return false;

            prestamo.FechaDevolucion = DateTime.UtcNow;
            await _prestamoRepository.UpdateAsync(prestamo);

            // Liberar libro
            var libro = await _libroRepository.GetByIdAsync(prestamo.LibroId);
            if (libro != null)
            {
                libro.Disponible = true;
                await _libroRepository.UpdateAsync(libro);
            }

            return true;
        }

        public async Task<int> ObtenerPrestamosActivosAsync(int usuarioId)
        {
            return await _prestamoRepository.GetActiveLoansCountAsync(usuarioId);
        }
    }
}
