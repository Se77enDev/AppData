using Biblioteca.Data.Database;
using Biblioteca.Data.Interfaces;
using Biblioteca.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Data.Repositories;
public class LibroRepository : ILibroRepository
{
    private readonly BibliotecaContext _context;

    public LibroRepository(BibliotecaContext context)
    {
        _context = context;
    }

    public async Task<Libro?> GetByIdAsync(int id)
    {
        return await _context.Libros.FindAsync(id);
    }

    public async Task<IEnumerable<Libro>> GetAllAsync()
    {
        return await _context.Libros.ToListAsync();
    }

    public async Task AddAsync(Libro libro)
    {
        await _context.Libros.AddAsync(libro);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Libro libro)
    {
        _context.Libros.Update(libro);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var libro = await GetByIdAsync(id);
        if (libro != null)
        {
            _context.Libros.Remove(libro);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> IsAvailableAsync(int libroId)
    {
        var libro = await GetByIdAsync(libroId);
        return libro?.Disponible ?? false;
    }

    public async Task<IEnumerable<Libro>> GetByCategoriaAsync(string categoria)
    {
        return await _context.Libros
            .Where(l => l.Categoria == categoria)
            .ToListAsync();
    }
}