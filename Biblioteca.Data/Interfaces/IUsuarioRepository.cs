using Biblioteca.Entities;
using Biblioteca.Entities.Models;

namespace Biblioteca.Data.Interfaces;

public interface IUsuarioRepository : IRepository<Usuario>
{
    Task<Usuario?> GetByNombreAsync(string nombre);
    Task<IEnumerable<Usuario>> GetByTipoAsync(string tipo);
    Task<bool> ExistsByNombreAsync(string nombre);
    Task<IEnumerable<Usuario>> GetUsersWithActiveLoansAsync();
    Task<IEnumerable<Usuario>> SearchUsersAsync(string searchTerm);
}