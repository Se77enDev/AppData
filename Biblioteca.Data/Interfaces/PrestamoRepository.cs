using Biblioteca.Data.Database;
using Biblioteca.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Data.Repositories;

public class PrestamoRepository : IPrestamoRepository
{
    private readonly BibliotecaContext _context;

    public PrestamoRepository(BibliotecaContext context)
    {
        _context = context;
    }

    public async Task<Prestamo?> GetByIdAsync(int id)
    {
        return await _context.Prestamos
            .Include(p => p.Libro)
            .Include(p => p.Usuario)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Prestamo>> GetAllAsync()
    {
        return await _context.Prestamos
            .Include(p => p.Libro)
            .Include(p => p.Usuario)
            .ToListAsync();
    }

    public async Task AddAsync(Prestamo prestamo)
    {
        await _context.Prestamos.AddAsync(prestamo);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Prestamo prestamo)
    {
        _context.Prestamos.Update(prestamo);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var prestamo = await GetByIdAsync(id);
        if (prestamo != null)
        {
            _context.Prestamos.Remove(prestamo);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<int> GetActiveLoansCountAsync(int usuarioId)
    {
        return await _context.Prestamos
            .CountAsync(p => p.UsuarioId == usuarioId && !p.Devuelto);
    }

    public async Task<IEnumerable<Prestamo>> GetActiveLoansByUserAsync(int usuarioId)
    {
        return await _context.Prestamos
            .Include(p => p.Libro)
            .Include(p => p.Usuario)
            .Where(p => p.UsuarioId == usuarioId && !p.Devuelto)
            .ToListAsync();
    }

    public async Task<IEnumerable<Prestamo>> GetLoansByUserAsync(int usuarioId)
    {
        return await _context.Prestamos
            .Include(p => p.Libro)
            .Where(p => p.UsuarioId == usuarioId)
            .OrderByDescending(p => p.FechaPrestamo)
            .ToListAsync();
    }

    public async Task<IEnumerable<Prestamo>> GetLoanHistoryByBookAsync(int libroId)
    {
        return await _context.Prestamos
            .Include(p => p.Usuario)
            .Where(p => p.LibroId == libroId)
            .OrderByDescending(p => p.FechaPrestamo)
            .ToListAsync();
    }

    public async Task<bool> HasActiveLoanAsync(int usuarioId, int libroId)
    {
        return await _context.Prestamos
            .AnyAsync(p => p.UsuarioId == usuarioId &&
                          p.LibroId == libroId &&
                          !p.Devuelto);
    }

    public async Task<IEnumerable<Prestamo>> GetOverdueLoansAsync(int diasLimite)
    {
        var fechaLimite = DateTime.UtcNow.AddDays(-diasLimite);

        return await _context.Prestamos
            .Include(p => p.Libro)
            .Include(p => p.Usuario)
            .Where(p => !p.Devuelto && p.FechaPrestamo <= fechaLimite)
            .ToListAsync();
    }
}