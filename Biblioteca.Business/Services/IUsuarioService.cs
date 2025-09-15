using Biblioteca.Entities.Models;


namespace Biblioteca.Business.Services
{
    public interface IUsuarioService
    {
        Task<Usuario> RegistrarUsuarioAsync(string nombre, string tipo);
        Task<IEnumerable<Usuario>> ObtenerTodosUsuariosAsync();
        Task<IEnumerable<Usuario>> ObtenerUsuariosPorTipoAsync(string tipo);
        Task<Usuario?> ObtenerUsuarioPorIdAsync(int id);
        Task<bool> ExisteUsuarioAsync(string nombre);
        Task<int> ObtenerCantidadUsuariosAsync();
    }
}

