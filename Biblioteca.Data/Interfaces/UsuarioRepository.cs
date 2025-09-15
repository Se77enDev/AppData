using Biblioteca.Data.Database;
using Biblioteca.Data.Interfaces;
using Biblioteca.Entities;
using Biblioteca.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Data.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly BibliotecaContext _context;

    public UsuarioRepository(BibliotecaContext context)
    {
        _context = context;
    }

    public async Task<Usuario?> GetByIdAsync(int id)
    {
        return await _context.Usuarios.FindAsync(id);
    }

    public async Task<IEnumerable<Usuario>> GetAllAsync()
    {
        return await _context.Usuarios.ToListAsync();
    }

    public async Task AddAsync(Usuario usuario)
    {
        await _context.Usuarios.AddAsync(usuario);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Usuario usuario)
    {
        _context.Usuarios.Update(usuario);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var usuario = await GetByIdAsync(id);
        if (usuario != null)
        {
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<Usuario?> GetByNombreAsync(string nombre)
    {
        return await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Nombre == nombre);
    }
    public async Task<IEnumerable<Usuario>> GetByTipoAsync(string tipo)
    {
        return await _context.Usuarios
            .Where(u => u.Tipo == tipo)
            .ToListAsync();
    }
    public async Task<bool> ExistsByNombreAsync(string nombre)
    {
        return await _context.Usuarios
            .AnyAsync(u => u.Nombre == nombre);
    }
    public async Task<IEnumerable<Usuario>> GetUsersWithActiveLoansAsync()
    {
        return await _context.Usuarios
            .Where(u => _context.Prestamos.Any(p => p.UsuarioId == u.Id && !p.Devuelto))
            .ToListAsync();
    }
    public async Task<IEnumerable<Usuario>> SearchUsersAsync(string searchTerm)
    {
        return await _context.Usuarios
            .Where(u => u.Nombre.Contains(searchTerm))
            .ToListAsync();
    }
}