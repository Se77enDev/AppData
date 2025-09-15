using Biblioteca.Data.Interfaces;
using Biblioteca.Entities.Models;

namespace Biblioteca.Business.Services;
public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioService(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<Usuario> RegistrarUsuarioAsync(string nombre, string tipo)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre es obligatorio");

        if (tipo != "estudiante" && tipo != "profesor")
            throw new ArgumentException("El tipo debe ser 'estudiante' o 'profesor'");

        if (await _usuarioRepository.ExistsByNombreAsync(nombre))
            throw new InvalidOperationException("Ya existe un usuario con ese nombre");

        var usuario = new Usuario
        {
            Nombre = nombre,
            Tipo = tipo
        };

        await _usuarioRepository.AddAsync(usuario);
        return usuario;
    }

    public async Task<IEnumerable<Usuario>> ObtenerTodosUsuariosAsync()
    {
        return await _usuarioRepository.GetAllAsync();
    }

    public async Task<IEnumerable<Usuario>> ObtenerUsuariosPorTipoAsync(string tipo)
    {
        return await _usuarioRepository.GetByTipoAsync(tipo);
    }

    public async Task<Usuario?> ObtenerUsuarioPorIdAsync(int id)
    {
        return await _usuarioRepository.GetByIdAsync(id);
    }

    public async Task<bool> ExisteUsuarioAsync(string nombre)
    {
        return await _usuarioRepository.ExistsByNombreAsync(nombre);
    }

    public async Task<int> ObtenerCantidadUsuariosAsync()
    {
        var usuarios = await _usuarioRepository.GetAllAsync();
        return usuarios.Count();
    }
}